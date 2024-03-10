using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public EnemyPool enemyPool;
    public WaveInfo waveInfo;
    public GameObject gameMgrCanvas;
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
}
