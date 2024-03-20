using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using Random = UnityEngine.Random;

public class EnemyPool : ObjectPool
{
    [SerializeField] WallController target;
    [SerializeField] float randomXvalue;
    [SerializeField] EnemyPool_Boss bossPool;

    int mobType;

    float correction;
    int repeat;

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

    public void Spawn(int _unitIndex, float _correction)
    {
        float randomX = Random.Range(-randomXvalue, randomXvalue);
        Vector3 spawnPos = new Vector3(randomX, transform.position.y, transform.position.z);
        EnemyController unit = Get(_unitIndex, spawnPos).GetComponent<EnemyController>();
        unit.InitData(target, _correction, 0);
        UnitList.enumyList.Add(unit);
    }

    public void ChangeWave(int _curWave, float _correction)
    {
        correction = _correction;
        //repeat = 1 + (int)_curWave / 10;

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
                bossPool.Spawn(_correction * 5f);
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
                bossPool.Spawn(_correction * 5f);
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
