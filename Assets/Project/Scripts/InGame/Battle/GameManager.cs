using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Random = UnityEngine.Random;
using Transform = UnityEngine.Transform;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public WaveInfo waveInfo;
    public StageData stageData;
    public EnemyPool enemyPool;
    public EnemyPool_Elite enemyPool_Elite;
    public EnemyPool_Boss enemyPool_Boss;
    public List<TowerController> tower;

    public VFXPool vfxPool;
    public BulletPool commonBulletPool;
    public ObjectSpawnPool obPool;
    public GameObject gameMgrCanvas;
    public Sprite[] WeaponSprite;
    public UI_Battle _UIBattle;
    public UnitList unitList;

    public Slider eliteGauge;
    //public Slider energyGauge;
    public Text txtEnergyCount;

    public Text txtBlockProbSmall;
    public Text txtBlockProbBig;
    public Text txtMaxBlock;

    public int curStage = 0;
    [HideInInspector] public int ExplosionCrrection = 1;
    [HideInInspector] public int MultCrrection = 1;
    [HideInInspector] public int DrillCrrection = 1;
    [HideInInspector] public int MoveCount = 10;

    private int EenergyCount = 10;
    private int curEnergy = 5;
    public bool isStart = false;

    public int CurEnergy { get => curEnergy; set => curEnergy = value; }

    private int maxNumber = 4;
    private int maxNumber_tmp = 4;

    public int Numberic
    {
        get
        {
            if (maxNumber <= 32)
            {
                return Random.Range(0, 100) < 80 ? 2 : 4;
            }
            else if (maxNumber <= 64)
            {
                return Random.Range(0, 100) < 50 ? 2 : 4;
            }
            else if (maxNumber <= 128)
            {
                return Random.Range(0, 100) < 20 ? 2 : 4;
            }
            else if (maxNumber <= 256)
            {
                return Random.Range(0, 100) < 90 ? 4 : 8;
            }
            else if (maxNumber <= 512)
            {
                return Random.Range(0, 100) < 70 ? 4 : 8;
            }
            else if (maxNumber <= 1024)
            {
                return Random.Range(0, 100) < 50 ? 4 : 8;
            }
            else
            {
                if (maxNumber == 2048)
                {
                    SpawnLastBossMonster();
                }
                return 8;
            }
        }
        set
        {
            maxNumber = value;
        }
    }
    //public UnitList unitList;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //gameMgrCanvas.gameObject.SetActive(true);
    }

    private void Start()
    {
        OnClickBtnGameStart();

        _originBoardPos = _trBoard.position;
        _imgBoardRed.DOFade(35f / 255f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        _imgBoardRed.gameObject.SetActive(false);
    }

    public void GameStart()
    {
        gameMgrCanvas.SetActive(false);
        waveInfo.GameStart(curStage);
        enemyPool.GameStart(curStage);
        enemyPool_Elite.GameStart(curStage);
        enemyPool_Boss.GameStart(curStage);

        //curEnergy = 70;

        eliteGauge.maxValue = MoveCount;
        eliteGauge.value = MoveCount;

        //energyGauge.maxValue = EenergyCount;
        //energyGauge.value = EenergyCount;
        txtEnergyCount.text = CurEnergy.ToString();
    }

    public void WaveStart()
    {
        waveInfo.WaveStart();
        //enemyPool.WaveStart(waveInfo.curWave, waveInfo.correction);
    }

    public void GameClear()
    {
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
        if (!isStart) return;

        MoveCount--;

        SoundManager.Instance.Play(Enum_Sound.Effect, "2048Move");
        if (MoveCount == 0)
        {
            SpawnBossMonster();
            MoveCount = 10;
        }
        eliteGauge.value = MoveCount;

        //txtCount.text = MoveCount.ToString();
    }

    public void MoveEnergy(Vector3 _tr)
    {
        EenergyCount--;

        if (EenergyCount == 0)
        {
            if (Random.value < 0.8)
            {
                obPool.SpawnEnergy(_tr, 1);
            }
            else if (Random.value < 0.9)
            {
                obPool.SpawnEnergy(_tr, 2);
            }
            else
            {
                obPool.SpawnEnergy(_tr, 3);
            }
            //MoveEnergyFull(1);
            EenergyCount = 10;
        }
        //energyGauge.value = EenergyCount;
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
            enemyPool_Boss.Spawn(enemyPool.correction * 7f, 1);
        }
    }

    public void SpawnLastBossMonster()
    {
        enemyPool_Boss.Spawn(enemyPool.correction * 20f, 3);
        waveInfo.WaveStop();
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
                txtBlockProbSmall.text = "2\n80%";
                txtBlockProbBig.text = "4\n20%";
            }
            else if (maxNumber <= 64)
            {
                txtBlockProbSmall.text = "2\n50%";
                txtBlockProbBig.text = "4\n50%";
            }
            else if (maxNumber <= 128)
            {
                txtBlockProbSmall.text = "2\n20%";
                txtBlockProbBig.text = "4\n80%";
            }
            else if (maxNumber <= 256)
            {
                txtBlockProbSmall.text = "4\n90%";
                txtBlockProbBig.text = "8\n10%";
            }
            else if (maxNumber <= 512)
            {
                txtBlockProbSmall.text = "4\n70%";
                txtBlockProbBig.text = "8\n30%";
            }
            else if (maxNumber <= 1024)
            {
                txtBlockProbSmall.text = "4\n50%";
                txtBlockProbBig.text = "8\n50%";
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

    private void OnDestroy()
    {
        instance = null;
    }
}
