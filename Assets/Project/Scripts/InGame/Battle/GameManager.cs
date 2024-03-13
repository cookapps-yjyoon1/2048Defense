using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public EnemyPool enemyPool;
    public WaveInfo waveInfo;
    public VFXPool vfxPool;
    public BulletPool commonBulletPool;
    public GameObject gameMgrCanvas;

    private int maxNumber = 4;
    public int Numberic
    {
        get
        {
            var num = maxNumber / 64;
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
        enemyPool.GameStart();        
        waveInfo.GameStart();
    }

    public void GameOver()
    {
        
    }


    public void OnClickBtnGameStart()
    {
        SoundManager.Instance.Play(Enum_Sound.Bgm, "InGame", 0,0.5f);
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
    }

    public void SpawnBossMonster()
    {
        enemyPool.Spawn(1,waveInfo.correction * 2f);
    }
    
    public void SpawnLastBossMonster()
    {
        enemyPool.Spawn(1,waveInfo.correction * 2f);
    }

    public void SetMaxBlock(int num)
    {
        maxNumber = Mathf.Max(maxNumber, num);
    }
}
