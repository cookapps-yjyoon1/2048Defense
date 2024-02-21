using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    WaitForSeconds wfsSpawnDeley = new WaitForSeconds(0.1f);

    //[SerializeField] PlayerController playerCtr;
    [SerializeField] EnemyPool enemies;
    //[SerializeField] WaveInfo waveInfo;

    public void GameStart()
    {
        //waveInfo.GameStart();
        //playerCtr.GameStart();
        //StartCoroutine(CoSpawn());
    }

    public void GameOver()
    {
        //waveInfo.GameOver();
        //enemies.GameOver();
        //playerCtr.GameOver();
        StopAllCoroutines();
    }

    //IEnumerator CoSpawn()
    //{
    //    while (true)
    //    {
    //        for (int i = 0; i < waveInfo.CurWaveData.spawnCount; i++)
    //        {
    //            enemies.Spawn(waveInfo.CurWaveData.buff, RandomUnitIndex(
    //                                                        waveInfo.CurWaveData.prob00,
    //                                                        waveInfo.CurWaveData.prob01,
    //                                                        waveInfo.CurWaveData.prob02
    //                                                     )
    //            );
    //            yield return wfsSpawnDeley;
    //        }
    //        yield return new WaitForSeconds(waveInfo.CurWaveData.spawnTime);
    //    }
    //}

    int RandomUnitIndex(float _prob00, float _prob01, float _prob02)
    {
        float randomNum = Random.Range(0, 1f);

        if (randomNum < _prob00)
        {
            return 0;
        }
        else if (randomNum < _prob00 + _prob01)
        {
            return 1;
        }
        else if (randomNum < _prob00 + _prob01 + _prob02)
        {
            return 2;
        }
        else
        {
            print($"{randomNum} ??");
            return 0;
        }
    }
}
