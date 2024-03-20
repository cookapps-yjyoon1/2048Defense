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
    [SerializeField] EnemyPool_Elite elitePool;
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

    public void WaveStart(int _curWave, float _correction)
    {
        ChangeWave(_curWave, _correction);
    }
    public void WaveStop()
    {
        StopAllCoroutines();
        GameManager.instance.MoveEnergyFull(25);
    }

    public void ChangeWave(int _curWave, float _correction)
    {
        correction = _correction;
        
        

        switch (_curWave % 5)
        {
            case 0:
                StartCoroutine(CoWaveSpawn(15, correction));
                StartCoroutine(CoWaveSpawn_3wave(10, correction));
                StartCoroutine(elitePool.CoWaveSpawn(repeat, correction));
                break;
            case 1:
                StartCoroutine(CoWaveSpawn(15, correction));
                StartCoroutine(CoWaveSpawn_3wave(10, correction));
                StartCoroutine(elitePool.CoWaveSpawn_3wave(5 + repeat, correction));
                break;
            case 2:
                StartCoroutine(CoWaveSpawn(15, correction));
                StartCoroutine(CoWaveSpawn_3wave(10, correction));
                StartCoroutine(elitePool.CoWaveSpawn(repeat, correction));
                break;
            case 3:
                StartCoroutine(CoWaveSpawn(15, correction));
                StartCoroutine(CoWaveSpawn_3wave(10, correction));
                StartCoroutine(elitePool.CoWaveSpawn_3wave(5 + repeat, correction));
                break;
            case 4:
                for (int i = 0; i < repeat; i++)
                {
                    bossPool.Spawn(correction * 6f);
                }
                repeat++;
                break;
        }

        //switch (_curWave % 10)
        //{
        //    case 0:
        //        repeat++;
        //        break;
        //    case 1:
        //        break;
        //    case 2:
        //        StartCoroutine(CoSpawn02(10, correction * 2));
        //        break;
        //    case 3:
        //        break;
        //    case 4:
        //        bossPool.Spawn(correction * 6f);
        //        break;
        //    case 5:
        //        repeat++;
        //        break;
        //    case 6:
        //        break;
        //    case 7:
        //        StartCoroutine(CoSpawn02(10, correction * 2));
        //        break;
        //    case 8:
        //        break;
        //    case 9:
        //        bossPool.Spawn(correction * 6f);
        //        break;
        //}
    }

    IEnumerator CoSpawn()
    {
        while (true)
        {
            for (int i = 0; i < repeat; i++)
            {
                Spawn(Random.Range(0, mobType), correction);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator CoWaveSpawn(int _repeat, float _correction)
    {
        for (int i = 0; i < _repeat; i++)
        {
            Spawn(Random.Range(0, mobType), _correction);
            yield return new WaitForSeconds(0.5f);
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


    IEnumerator CoSpawn02(int _repeat, float _correction)
    {
        for (int i = 0; i < _repeat; i++)
        {
            Spawn(Random.Range(0, mobType), _correction);
            yield return new WaitForSeconds(0.1f);
        }
        
        yield return new WaitForSeconds(8f);

        for (int i = 0; i < _repeat; i++)
        {
            Spawn(Random.Range(0, mobType), _correction);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
