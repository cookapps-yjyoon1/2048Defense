using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;
using Random = UnityEngine.Random;

public class EnemyPool : ObjectPool
{
    [SerializeField] WallController target;
    [SerializeField] float randomXvalue;
    [SerializeField] EnemyPool_Elite elitePool;
    [SerializeField] EnemyPool_Boss bossPool;

    int mobType;

    public float correction;
    float increCorrection;
    int repeat;

    public void GameStart(int _stage)
    {
        increCorrection = GameManager.instance.stageData.stage[_stage].correction;
        correction = increCorrection;
        repeat = 1;

        mobType = GameManager.instance.stageData.stage[_stage].mob.Count;

        for (int i = 0; i < mobType; i++)
        {
            prefab[i] = GameManager.instance.stageData.stage[_stage].mob[i];
        }

        PreCreatePoolObject();
        //StartCoroutine(CoSpawn());
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

    public void WaveStart(int _curWave)
    {
        ChangeWave(_curWave);
    }

    public void ChangeWave(int _curWave)
    {
        StopAllCoroutines();

        switch (_curWave % 5)
        {
            case 0:
                StartCoroutine(CoWaveSpawn(10 + repeat, correction));
                StartCoroutine(CoWaveSpawn(10 + repeat, correction));
                StartCoroutine(CoWaveSpawn_3wave(10 + repeat, correction));
                break;
            case 1:
                StartCoroutine(CoWaveSpawn(10 + repeat, correction));
                StartCoroutine(CoWaveSpawn_3wave(5 + repeat, correction));
                StartCoroutine(elitePool.CoWaveSpawn(repeat, correction));
                break;
            case 2:
                correction += increCorrection;
                StartCoroutine(CoWaveSpawn(10 + repeat, correction));
                StartCoroutine(CoWaveSpawn_3wave(5 + repeat, correction));
                StartCoroutine(elitePool.CoWaveSpawn_3wave(5 + repeat, correction));
                break;
            case 3:
                StartCoroutine(CoWaveSpawn(10 + repeat, correction));
                StartCoroutine(CoWaveSpawn_3wave(5 + repeat, correction));
                StartCoroutine(elitePool.CoWaveSpawn(repeat, correction));
                StartCoroutine(elitePool.CoWaveSpawn_3wave(5 + repeat, correction));
                break;
            case 4:
                for (int i = 0; i < repeat; i++)
                {
                    bossPool.Spawn(correction * 10f, 2);
                }
                correction += increCorrection;
                break;
        }
    }

    IEnumerator CoWaveSpawn(int _repeat, float _correction)
    {
        int delayTime = 15 / _repeat;

        for (int i = 0; i < _repeat; i++)
        {
            Spawn(Random.Range(0, mobType), _correction);
            yield return new WaitForSeconds(delayTime);
        }
    }

    IEnumerator CoWaveSpawn_3wave(int _repeat, float _correction)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < _repeat; j++)
            {
                Spawn(Random.Range(0, mobType), _correction);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(3f);
        }
    }
}
