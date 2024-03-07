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
        Wave00(0, 1, 1, 1);
    }

    public void Spawn(int _unitIndex, float _increaseHp, float _increaseDef)
    {
        float randomX = Random.Range(-randomXvalue, randomXvalue);
        Vector3 spawnPos = new Vector3(randomX, transform.position.y, transform.position.z);
        EnemyController unit = Get(_unitIndex, spawnPos).GetComponent<EnemyController>();
        unit.InitData(target, _increaseHp, _increaseDef);
        UnitList.enumyList.Add(unit);
    }

    public void ChangeWave(int _curWave, float _correction)
    {
        StopAllCoroutines();
        switch (_curWave%10)
        {
            case 0:
                Wave00(0, 1 * _correction, 1 * _correction, 1);
                break;
            case 1:
                Wave01(0, 1 * _correction, 1 * _correction, 1);
                break;
            case 2:
                Wave02(0, 1 * _correction, 1 * _correction, 1);
                break;
            case 3:
                Wave03(0, 1 * _correction, 1 * _correction, 1);
                break;
            case 4:
                Wave04(0, 1 * _correction, 1 * _correction, 1);
                break;
            case 5:
                Wave05(0, 1 * _correction, 1 * _correction, 1);
                break;
            case 6:
                Wave06(0, 1 * _correction, 1 * _correction, 1);
                break;
            case 7:
                Wave07(0, 1 * _correction, 1 * _correction, 1);
                break;
            case 8:
                Wave08(0, 1 * _correction, 1 * _correction, 1);
                break;
            case 9:
                Wave09(0, 1 * _correction, 1 * _correction, 1);
                break;
        }
    }

    #region Wave
    public void Wave00(int _unitIndex, float _increaseHp, float _increaseDef, int _repeat)
    {
        
        StartCoroutine(CoSpawnTypeA(_unitIndex, _increaseHp, _increaseDef, _repeat));
    }

    public void Wave01(int _unitIndex, float _increaseHp, float _increaseDef, int _repeat)
    {
        StartCoroutine(CoSpawnTypeA(_unitIndex, _increaseHp, _increaseDef, _repeat));
        StartCoroutine(CoSpawnTypeB(_unitIndex, _increaseHp, _increaseDef, _repeat));
    }

    public void Wave02(int _unitIndex, float _increaseHp, float _increaseDef, int _repeat)
    {
        StartCoroutine(CoSpawnTypeA(_unitIndex, _increaseHp, _increaseDef, _repeat));
    }


    public void Wave03(int _unitIndex, float _increaseHp, float _increaseDef, int _repeat)
    {
        StartCoroutine(CoSpawnTypeA(_unitIndex, _increaseHp, _increaseDef, _repeat));
        StartCoroutine(CoSpawnTypeB(_unitIndex, _increaseHp, _increaseDef, _repeat));
    }


    public void Wave04(int _unitIndex, float _increaseHp, float _increaseDef, int _repeat)
    {
        StartCoroutine(CoSpawnTypeA(_unitIndex, _increaseHp, _increaseDef, _repeat));
        StartCoroutine(CoSpawnTypeB(_unitIndex, _increaseHp, _increaseDef, _repeat));
    }


    public void Wave05(int _unitIndex, float _increaseHp, float _increaseDef, int _repeat)
    {
        StartCoroutine(CoSpawnTypeA(_unitIndex, _increaseHp, _increaseDef, _repeat));
        StartCoroutine(CoSpawnTypeB(_unitIndex, _increaseHp, _increaseDef, _repeat));
        StartCoroutine(CoSpawnTypeC(_unitIndex, _increaseHp, _increaseDef, _repeat));
    }

    public void Wave06(int _unitIndex, float _increaseHp, float _increaseDef, int _repeat)
    {
        StartCoroutine(CoSpawnTypeA(_unitIndex, _increaseHp, _increaseDef, _repeat));
        StartCoroutine(CoSpawnTypeB(_unitIndex, _increaseHp, _increaseDef, _repeat));
        StartCoroutine(CoSpawnTypeC(_unitIndex, _increaseHp, _increaseDef, _repeat));
    }


    public void Wave07(int _unitIndex, float _increaseHp, float _increaseDef, int _repeat)
    {
        StartCoroutine(CoSpawnTypeA(_unitIndex, _increaseHp, _increaseDef, _repeat));
        StartCoroutine(CoSpawnTypeB(_unitIndex, _increaseHp, _increaseDef, _repeat));
        StartCoroutine(CoSpawnTypeC(_unitIndex, _increaseHp, _increaseDef, _repeat));
    }


    public void Wave08(int _unitIndex, float _increaseHp, float _increaseDef, int _repeat)
    {
        StartCoroutine(CoSpawnTypeA(_unitIndex, _increaseHp, _increaseDef, _repeat));
        StartCoroutine(CoSpawnTypeB(_unitIndex, _increaseHp, _increaseDef, _repeat));
        StartCoroutine(CoSpawnTypeC(_unitIndex, _increaseHp, _increaseDef, _repeat));
    }


    public void Wave09(int _unitIndex, float _increaseHp, float _increaseDef, int _repeat)
    {
        SpawnBoss(_unitIndex, _increaseHp, _increaseDef);
    }
    #endregion

    #region CoSpawnType
    IEnumerator CoSpawnTypeA(int _unitIndex, float _increaseHp, float _increaseDef, int _repeat)
    {
        while (true)
        {
            for (int i = 0; i < _repeat; i++)
            {
                Spawn(_unitIndex, _increaseHp, _increaseDef);
            }
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator CoSpawnTypeB(int _unitIndex, float _increaseHp, float _increaseDef, int _repeat)
    {
        while (true)
        {
            for (int i = 0; i < _repeat; i++)
            {
                Spawn(_unitIndex, _increaseHp, _increaseDef);
                yield return new WaitForSeconds(0.5f);
                Spawn(_unitIndex, _increaseHp, _increaseDef);
                yield return new WaitForSeconds(0.5f);
                Spawn(_unitIndex, _increaseHp, _increaseDef);
            }
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator CoSpawnTypeC(int _unitIndex, float _increaseHp, float _increaseDef, int _repeat)
    {
        while (true)
        {
            for (int i = 0; i < _repeat; i++)
            {
                Spawn(_unitIndex, _increaseHp, _increaseDef);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void SpawnBoss(int _unitIndex, float _increaseHp, float _increaseDef)
    {
        Spawn(_unitIndex, _increaseHp, _increaseDef);
        
    }
    #endregion
}
