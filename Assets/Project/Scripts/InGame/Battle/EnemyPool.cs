using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool
{
    [SerializeField] WallController target;
    [SerializeField] float randomXvalue;

    //public void GameOver()
    //{
    //    foreach (var item1 in pool)
    //    {
    //        foreach (GameObject item2 in item1)
    //        {
    //            if (item2.activeSelf)
    //            {
    //                item2.GetComponent<EnemyController>().GameOver();
    //            }
    //        }
    //    }
    //}

    public void GameStart()
    {
        StartCoroutine(CoSpawn_1s_1m(0, 1, 1));
    }

    public void Spawn(int _unitIndex, float _correction)
    {
        float randomX = Random.Range(-randomXvalue, randomXvalue);
        Vector3 spawnPos = new Vector3(randomX, transform.position.y, transform.position.z);
        EnemyController unit = Get(_unitIndex, spawnPos).GetComponent<EnemyController>();
        unit.InitData(target, _correction);
        UnitList.enumyList.Add(unit);
    }

    public void ChangeWave(int _curWave, float _correction)
    {
        StopAllCoroutines();
        int repeat = (_curWave / 10) + 1;

        switch (_curWave % 10)
        {
            case 0:
                StartCoroutine(CoSpawn_1s_1m(0, _correction, repeat));
                break;
            case 1:
                StartCoroutine(CoSpawn_1s_1m(0, _correction, repeat));
                StartCoroutine(CoSpawn_3s_1m(1, _correction, repeat));
                break;
            case 2:
                StartCoroutine(CoSpawn_1s_1m(0, _correction, repeat));
                StartCoroutine(CoSpawn_3s_1m(2, _correction, repeat));
                break;
            case 3:
                StartCoroutine(CoSpawn_1s_1m(0, _correction, repeat));
                StartCoroutine(CoSpawn_3s_1m(1, _correction, repeat));
                break;
            case 4:
                StartCoroutine(CoSpawn_1s_1m(0, _correction, repeat));
                StartCoroutine(CoSpawn_3s_1m(2, _correction, repeat));
                break;
            case 5:
                StartCoroutine(CoSpawn_1s_1m(0, _correction, repeat));
                StartCoroutine(CoSpawn_3s_1m(1, _correction, repeat));
                break;
            case 6:
                StartCoroutine(CoSpawn_1s_1m(0, _correction, repeat));
                StartCoroutine(CoSpawn_3s_1m(2, _correction, repeat));
                break;
            case 7:
                StartCoroutine(CoSpawn_1s_1m(0, _correction, repeat));
                StartCoroutine(CoSpawn_3s_1m(1, _correction, repeat));
                break;
            case 8:
                StartCoroutine(CoSpawn_1s_1m(0, _correction, repeat));
                StartCoroutine(CoSpawn_3s_1m(2, _correction, repeat));
                break;
            case 9:
                StartCoroutine(CoSpawn_1s_1m(0, _correction, repeat));
                StartCoroutine(CoSpawn_1s_1m(1, _correction, repeat));
                StartCoroutine(CoSpawn_1s_1m(2, _correction, repeat));
                break;
        }
    }

    #region CoSpawnType

    IEnumerator CoSpawn_1s_1m(int _unitIndex, float _increaseHp, int _repeat)
    {
        while (true)
        {
            for (int i = 0; i < _repeat; i++)
            {
                Spawn(_unitIndex, _increaseHp);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator CoSpawn_3s_1m(int _unitIndex, float _increaseHp, int _repeat)
    {
        while (true)
        {
            for (int i = 0; i < _repeat; i++)
            {
                Spawn(_unitIndex, _increaseHp);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(3f);
        }
    }

    void SpawnBoss(int _unitIndex, float _increaseHp)
    {
        Spawn(_unitIndex, _increaseHp);

    }
    #endregion
}
