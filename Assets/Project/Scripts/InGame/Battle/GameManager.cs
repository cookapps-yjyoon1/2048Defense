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

    public VFXPool vfxPool;
    public BulletPool commonBulletPool;
    public GameObject gameMgrCanvas;
    public Sprite[] WeaponSprite;
    public Text txtCount;

    public int curStage = 0; // �κ񿡼� ������ �����;� ��

    private int maxNumber = 4;
    public int Numberic
    {
        get
        {
            var num = maxNumber / 512;
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

    public int MoveCount = 10;

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
    }

    public void GameOver()
    {

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

        txtCount.text = MoveCount.ToString();
    }

    public void SpawnBossMonster()
    {
        enemyPool_Elite.Spawn(waveInfo.correction * 2f, waveInfo.curWave);
    }

    public void SpawnLastBossMonster()
    {
        enemyPool_Boss.Spawn(waveInfo.correction * 2f);
    }

    public void SetMaxBlock(int num)
    {
        maxNumber = Mathf.Max(maxNumber, num);
    }
}
