using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Slider eliteGauge;
    public Slider energyGauge;
    public Text txtEnergyCount;

    public int curStage = 0;
    [HideInInspector] public int ExplosionCrrection = 1;
    [HideInInspector] public int MultCrrection = 1;
    [HideInInspector] public int DrillCrrection = 1;
    [HideInInspector] public int MoveCount = 10;
    [HideInInspector] public int EenergyCount = 10;
    [HideInInspector] public int CurEnergy = 150;

    private int maxNumber = 4;

    public int Numberic
    {
        get
        {
            var num = maxNumber / 32;
            num = Mathf.Clamp(num, 2, num);

            if (maxNumber == 2048)
            {
                SpawnLastBossMonster();
            }

            return num;
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

        gameMgrCanvas.gameObject.SetActive(true);
    }

    public void GameStart()
    {
        gameMgrCanvas.SetActive(false);
        waveInfo.GameStart(curStage);
        enemyPool.GameStart(curStage);
        enemyPool_Elite.GameStart(curStage);
        enemyPool_Boss.GameStart(curStage);


        eliteGauge.maxValue = MoveCount;
        eliteGauge.value = MoveCount;

        energyGauge.maxValue = EenergyCount;
        energyGauge.value = EenergyCount;
        txtEnergyCount.text = CurEnergy.ToString();
    }

    public void GameOver()
    {
        waveInfo.GameOver();
        enemyPool.GameOver();
        enemyPool_Elite.GameOver();
        enemyPool_Boss.GameOver();
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
            MoveEnergyFull(4);
            EenergyCount = 10;
        }
        energyGauge.value = EenergyCount;
    }

    public void MoveEnergyFull(int _energy)
    {
        CurEnergy += _energy;
        txtEnergyCount.text = CurEnergy.ToString();
    }

    public void UseEnergy()
    {
        CurEnergy--;
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
