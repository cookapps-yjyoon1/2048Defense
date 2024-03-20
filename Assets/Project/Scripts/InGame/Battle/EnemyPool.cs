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

        switch (_curWave % 5)
        {
            case 0:
                break;
            case 1:
                StartCoroutine(CoSpawn02(9));
                break;
            case 2:
                break;
            case 3:
                StartCoroutine(CoSpawn02(9));
                break;
            case 4:
                break;
            case 5:
                bossPool.Spawn(correction * 5f);
                break;
        }
    }

    IEnumerator CoSpawn()
    {
        while (true)
        {
            Spawn(Random.Range(0, mobType), correction);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator CoSpawn02(int _repeat)
    {
        for (int i = 0; i < _repeat; i++)
        {
            Spawn(Random.Range(0, mobType), correction);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(7.5f);

        for (int i = 0; i < _repeat; i++)
        {
            Spawn(Random.Range(0, mobType), correction);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
