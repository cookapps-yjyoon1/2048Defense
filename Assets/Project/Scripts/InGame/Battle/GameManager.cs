using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using static UnityEngine.Rendering.DebugUI;

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
    public GameObject gameMgrCanvas;
    public Sprite[] WeaponSprite;
    public UI_Battle _UIBattle;

    public Slider eliteGauge;
    public Slider energyGauge;
    public Text txtEnergyCount;

    public int curStage = 0;
    [HideInInspector] public int ExplosionCrrection = 1;
    [HideInInspector] public int MultCrrection = 1;
    [HideInInspector] public int DrillCrrection = 1;
    [HideInInspector] public int MoveCount = 10;
    [HideInInspector] public int EenergyCount = 10;
    [HideInInspector] private int curEnergy;

    public int CurEnergy { get => curEnergy; }

    private int maxNumber = 4;

    public int Numberic
    {
        get
        {
            if (maxNumber <= 64)
            {
                return Random.Range(0, 100) < 80 ? 2 : 4;
            }
            else if (maxNumber <= 128)
            {
                return Random.Range(0, 100) < 50 ? 2 : 4;
            }
            else if (maxNumber <= 256)
            {
                return Random.Range(0, 100) < 20 ? 2 : 4;
            }
            else if (maxNumber <= 512)
            {
                return Random.Range(0, 100) < 80 ? 4 : 8;
            }
            else if (maxNumber <= 1024)
            {
                return Random.Range(0, 100) < 50 ? 4 : 8;
            }
            else
            {
                SpawnLastBossMonster();
                return 8;
            }

            //var num = maxNumber / 64;
            //num = Mathf.Clamp(num, 2, num);

            //if (maxNumber == 2048)
            //{
            //    SpawnLastBossMonster();
            //}

            //return num;
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
    }

    public void GameStart()
    {
        gameMgrCanvas.SetActive(false);
        waveInfo.GameStart(curStage);
        enemyPool.GameStart(curStage);
        enemyPool_Elite.GameStart(curStage);
        enemyPool_Boss.GameStart(curStage);

        curEnergy = 100;

        eliteGauge.maxValue = MoveCount;
        eliteGauge.value = MoveCount;

        energyGauge.maxValue = EenergyCount;
        energyGauge.value = EenergyCount;
        txtEnergyCount.text = CurEnergy.ToString();
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
        MoveCount--;

        if (MoveCount == 0)
        {
            SpawnBossMonster();
            MoveCount = 10;
        }
        eliteGauge.value = MoveCount;

        //txtCount.text = MoveCount.ToString();
    }
    
    public void MoveEnergy()
    {
        EenergyCount--;

        if (EenergyCount == 0)
        {
            MoveEnergyFull(3);
            EenergyCount = 10;
        }
        energyGauge.value = EenergyCount;
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
    }

    public void SpawnBossMonster()
    {
        enemyPool_Elite.Spawn(waveInfo.correction * 2f, waveInfo.curWave);
    }

    public void SpawnLastBossMonster()
    {
        enemyPool_Boss.Spawn(waveInfo.correction * 10f);
    }

    public void SetMaxBlock(int num)
    {
        maxNumber = Mathf.Max(maxNumber, num);
    }
}
