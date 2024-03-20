using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool_Boss : ObjectPool
{
    [SerializeField] WallController target;
    [SerializeField] float randomXvalue;

    int mobType;

    public void GameStart(int _stage)
    {
        mobType = GameManager.instance.stageData.stage[_stage].mob_boss.Count;

        for (int i = 0; i < mobType; i++)
        {
            prefab[i] = GameManager.instance.stageData.stage[_stage].mob_boss[i];
        }

        PreCreatePoolObject();
    }

    public void GameOver()
    {

        foreach (var pair in pool)
        {
            int key = pair.Key;
            List<GameObject> gameObjectList = pair.Value;

            foreach (GameObject gameObject in gameObjectList)
            {
                gameObject.GetComponent<EnemyController>().GameOver();
            }
        }
    }

    public void Spawn(float _correction)
    {
        float randomX = Random.Range(-randomXvalue, randomXvalue);
        Vector3 spawnPos = new Vector3(randomX, transform.position.y, transform.position.z);
        EnemyController unit = Get(0, spawnPos).GetComponent<EnemyController>();
        unit.InitData(target, _correction, 2);
        UnitList.enumyList.Add(unit);
    }
}
