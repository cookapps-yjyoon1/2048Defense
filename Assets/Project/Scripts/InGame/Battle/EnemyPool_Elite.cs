using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool_Elite : ObjectPool
{
    [SerializeField] WallController target;
    [SerializeField] float randomXvalue;

    int mobType;

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
        mobType = GameManager.instance.stageData.stage[_stage].mob_elite.Count;

        for (int i = 0; i < mobType; i++)
        {
            prefab[i] = GameManager.instance.stageData.stage[_stage].mob_elite[i];
        }

        PreCreatePoolObject();
    }

    public void Spawn(int _unitIndex, float _correction, int _curWave)
    {
        int repeat = (_curWave / 10) + 1;

        for (int i = 0; i < repeat; i++)
        {
            float randomX = Random.Range(-randomXvalue, randomXvalue);
            Vector3 spawnPos = new Vector3(randomX, transform.position.y, transform.position.z);
            EnemyController unit = Get(_unitIndex, spawnPos).GetComponent<EnemyController>();
            unit.InitData(target, _correction);
            UnitList.enumyList.Add(unit);
        }
    }
}
