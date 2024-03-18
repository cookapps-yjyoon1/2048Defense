using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool
{
    [SerializeField] WallController target;
    [SerializeField] float randomXvalue;

    int mobType;

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

        mobType = GameManager.instance.stageData.stage[_stage].mob.Count;

        for (int i = 0; i < mobType; i++)
        {
            prefab[i] = GameManager.instance.stageData.stage[_stage].mob[i];
        }

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
        correction = _correction;

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

    IEnumerator CoSpawn()
    {
        while (true)
        {
            for (int i = 0; i < repeat; i++)
            {
                Spawn(Random.Range(0, mobType), correction);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
