using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool
{
    public GameObject[] prefabBoss;

    [SerializeField] WallController target;
    [SerializeField] float randomXvalue;

    int mobType;
    int eliteMobType;

    float correction;
    int repeat;

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

    public void GameStart(int _stage)
    {
        correction = 1;
        repeat = 1;

        mobType;
        eliteMobType = GameManager.instance.stageData.mob

        prefab[0] = GameManager.instance.stageData.GetMobType(_stage, 0);
        prefab[1] = GameManager.instance.stageData.GetMobType(_stage, 1);
        prefab[2] = GameManager.instance.stageData.GetMobType(_stage, 2);

        prefabBoss[0] = GameManager.instance.stageData.GetMobType(_stage, 0);
    
        PreCreatePoolObject();
        StartCoroutine(CoSpawn());

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
        //StopAllCoroutines();
        correction = _correction;
        //StartCoroutine(CoSpawn_1s_1m(0, _correction, repeat));

        switch (_curWave % 10)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                
                break;
            case 4:
                repeat++;
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                repeat++;
                break;
        }
    }

    #region CoSpawnType

    // 스테이지 메인이 되는 기본 유닛
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

    IEnumerator CoSpawn()
    {
        while (true)
        {
            for (int i = 0; i < repeat; i++)
            {
                Spawn(Random.Range(0, 3), correction);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    #endregion
}
