using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Transform = UnityEngine.Transform;

public class GameManager : SingletonBehaviour<GameManager>
{
    public WaveInfo waveInfo;
    public StageData stageData;
    public EnemyPool enemyPool;
    public EnemyPool_Elite enemyPool_Elite;
    public EnemyPool_Boss enemyPool_Boss;
    public AdsMobCount adsMobCount;
    public List<TowerController> tower;

    public VFXPool vfxPool;
    public BulletPool commonBulletPool;
    public ObjectSpawnPool obPool;
    public GameObject gameMgrCanvas;
    public Sprite[] WeaponSprite;
    public UI_Battle _UIBattle;
    public UnitList unitList;

    //public Slider eliteGauge;
    //public Slider energyGauge;
    public Text txtMobCount;
    public Text txtEnergyCount;

    public Text txtBlockProbSmall;
    public Text txtBlockProbBig;
    public Text txtMaxBlock;

    public int curStage => PlayerDataManager.ETCData.CurStage;
    [HideInInspector] public int ExplosionCrrection = 1;
    [HideInInspector] public int MultCrrection = 1;
    [HideInInspector] public int DrillCrrection = 1;
    [HideInInspector] public int MoveCount = 10;

    public int mobCount;

    private int EenergyCount = 6;
    private int curEnergy = 30;
    public bool isStart = false;
    public bool energyAutoAds = false;

    public int CurEnergy { get => curEnergy; set => curEnergy = value; }

    private int maxNumber = 4;
    private int maxNumber_tmp = 4;

    public int _towerGunCount = 0;
    bool spawnLastBoss = false;

    public int TowerGunCount
    {
        get
        {
            return _towerGunCount;
        }

        set
        {
            if (_towerGunCount == 15)
            {
                if (!spawnLastBoss)
                {
                    SpawnLastBossMonster();
                }
            }

            _towerGunCount = value;
        }
    }


    public int Numberic
    {
        get
        {
            if (maxNumber <= 32)
            {
                return Random.Range(0, 100) < 90 ? 2 : 4;
            }
            else if (maxNumber <= 64)
            {
                return Random.Range(0, 100) < 50 ? 2 : 4;
            }
            else if (maxNumber <= 128)
            {
                return Random.Range(0, 100) < 10 ? 2 : 4;
            }
            else if (maxNumber <= 256)
            {
                return Random.Range(0, 100) < 90 ? 4 : 8;
            }
            else if (maxNumber <= 512)
            {
                return Random.Range(0, 100) < 50 ? 4 : 8;
            }
            else if (maxNumber <= 1024)
            {
                return Random.Range(0, 100) < 10 ? 4 : 8;
            }
            else
            {
                if (maxNumber == 2048)
                {
                    if (!spawnLastBoss)
                    {
                        SpawnLastBossMonster();
                    }
                }

                return 8;
            }
        }
        set
        {
            maxNumber = value;
        }
    }

    private void Start()
    {
        // if (PlayerDataManager.ETCData.IsFirstPlay)
        // {
        //     // 팝업 띄우고 endAction으로 StartEvent 태우기
        //     
        //     return;
        // }

        StartEvent();

        void StartEvent()
        {
            OnClickBtnGameStart();

            _originBoardPos = _trBoard.position;
            _imgBoardRed.DOFade(35f / 255f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
            _imgBoardRed.gameObject.SetActive(false);
        }

        energyAutoAds = false;
    }

    public void GameStart()
    {
        gameMgrCanvas.SetActive(false);
        waveInfo.GameStart(curStage);
        enemyPool.GameStart(curStage);
        enemyPool_Elite.GameStart(curStage);
        enemyPool_Boss.GameStart(curStage);

        energyAutoAds = false;
        mobCount = 3000;

        txtMobCount.text = mobCount.ToString();

        txtEnergyCount.text = CurEnergy.ToString();
    }

    public void WaveStart()
    {
        waveInfo.WaveStart();
    }

    public void GameClear()
    {
        PlayerDataManager.ETCData.SetClearStage(curStage);
        _UIBattle.FinishGame(true);
    }

    public void GameOver()
    {
        waveInfo.GameOver();
        enemyPool.GameOver();
        enemyPool_Elite.GameOver();
        enemyPool_Boss.GameOver();
        _UIBattle.FinishGame(false);
        tower[0].GameOver();
        tower[1].GameOver();
        tower[2].GameOver();
    }


    public void OnClickBtnGameStart()
    {
        SoundManager.Instance.Play(Enum_Sound.Bgm, "InGame", 0, 0.5f);
        GameStart();
    }

    public void MoveNode()
    {
        SoundManager.Instance.Play(Enum_Sound.Effect, "2048Move");
        //if (!isStart) return;

        //MoveCount--;

        //if (MoveCount == 0)
        //{
        //    SpawnBossMonster();
        //    MoveCount = 10;
        //}
        //eliteGauge.value = MoveCount;

        //txtCount.text = MoveCount.ToString();
    }

    public void MobCount()
    {
        if (spawnLastBoss) return;

        mobCount--;

        if (!adsMobCount._goAds.activeSelf && !adsMobCount.off && mobCount <= 500)
        {
            adsMobCount._goAds.SetActive(true);
        }

        if (mobCount <= 0)
        {
            GameOver();
            txtMobCount.text = "Game\nOver";
        }
        else
        {
            txtMobCount.text = mobCount.ToString();
        }
    }

    public void MobCountNum(int _num)
    {
        if (spawnLastBoss) return;

        mobCount -= _num;

        if (mobCount <= 0)
        {
            GameOver();
            txtMobCount.text = "Game\nOver";
        }
        else
        {
            txtMobCount.text = mobCount.ToString();
        }
    }

    public void MoveEnergy(Vector3 _tr)
    {
        EenergyCount--;

        if (EenergyCount == 0)
        {
            if (Random.value < 0.7)
            {
                obPool.SpawnEnergy(_tr, 1);
            }
            else if (Random.value < 0.9)
            {
                obPool.SpawnEnergy(_tr, 2);
            }
            else if (Random.value <= 1)
            {
                obPool.SpawnEnergy(_tr, 4);
            }

            EenergyCount = 6;
        }
    }

    public void MoveEnergyFull(int _energy)
    {
        curEnergy += _energy;
        txtEnergyCount.text = CurEnergy.ToString();
    }

    public void UseEnergy()
    {
        curEnergy--;
        txtEnergyCount.text = CurEnergy.ToString();

        if (CurEnergy == 0 && !isStart)
        {
            isStart = true;
            WaveStart();
        }
    }

    public void SpawnBossMonster()
    {
        //enemyPool_Elite.Spawn(waveInfo.correction * 2f, waveInfo.curWave);

        for (int i = 0; i < (waveInfo.curWave / 10) + 1; i++)
        {
            enemyPool_Boss.Spawn(enemyPool.correction * 8f, 1);
        }
    }

    public void SpawnLastBossMonster()
    {
        spawnLastBoss = true;
        enemyPool_Boss.Spawn(enemyPool.correction * 60f, 3);
        waveInfo.WaveStop();
        enemyPool.WaveStop();
    }

    public void SetMaxBlock(int num)
    {
        maxNumber = Mathf.Max(maxNumber, num);

        if (maxNumber != maxNumber_tmp)
        {
            maxNumber_tmp = maxNumber;

            txtMaxBlock.text = "Max : " + maxNumber_tmp;


            if (maxNumber <= 32)
            {
                txtBlockProbSmall.text = "2\n90%";
                txtBlockProbBig.text = "4\n10%";
            }
            else if (maxNumber <= 64)
            {
                txtBlockProbSmall.text = "2\n50%";
                txtBlockProbBig.text = "4\n50%";
            }
            else if (maxNumber <= 128)
            {
                txtBlockProbSmall.text = "2\n10%";
                txtBlockProbBig.text = "4\n90%";
            }
            else if (maxNumber <= 256)
            {
                txtBlockProbSmall.text = "4\n90%";
                txtBlockProbBig.text = "8\n10%";
            }
            else if (maxNumber <= 512)
            {
                txtBlockProbSmall.text = "4\n50%";
                txtBlockProbBig.text = "8\n50%";
            }
            else if (maxNumber <= 1024)
            {
                txtBlockProbSmall.text = "4\n10%";
                txtBlockProbBig.text = "8\n90%";
            }
        }
    }

    [SerializeField] private Image _imgBoardRed;
    [SerializeField] private Transform _trBoard;
    private Vector3 _originBoardPos;
    public void HitTowerEvent()
    {
        _imgBoardRed.gameObject.SetActive(true);
        _trBoard.DOShakePosition(0.1f, 0.1f);
    }

    public void HitTowerEventEnd()
    {
        _imgBoardRed.gameObject.SetActive(false);
        _trBoard.DOKill();
        _trBoard.position = _originBoardPos;
    }
}
