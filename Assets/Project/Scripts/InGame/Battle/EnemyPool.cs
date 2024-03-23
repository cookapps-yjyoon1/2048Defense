
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyPool : ObjectPool
{
    public WallController target;
    [SerializeField] float randomXvalue;
    [SerializeField] EnemyPool_Elite elitePool;
    [SerializeField] EnemyPool_Boss bossPool;

    int mobType;

    public float correction;
    float increCorrection;
    int repeat;

    public void GameStart(int _stage)
    {
        increCorrection = GameManager.Instance.stageData.stage[_stage].correction;
        correction = increCorrection;
        repeat = 1;


        mobType = GameManager.Instance.stageData.stage[_stage].mob.Count;

        prefab = new GameObject[mobType];

        for (int i = 0; i < mobType; i++)
        {
            prefab[i] = GameManager.Instance.stageData.stage[_stage].mob[i];
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
        GameManager.Instance.unitList.enumyList.Add(unit);
    }

    public void WaveStart(int _curWave)
    {
        ChangeWave(_curWave);
    }

    public void WaveStop()
    {
        StopAllCoroutines();
    }

    public void ChangeWave(int _curWave)
    {
        StopAllCoroutines();

        switch (_curWave % 5)
        {
            case 0:
                StartCoroutine(CoWaveSpawn(10, correction));
                StartCoroutine(CoWaveSpawn(10 + repeat, correction));
                StartCoroutine(CoWaveSpawn_3wave(10, correction));
                break;
            case 1:
                StartCoroutine(CoWaveSpawn(10, correction));
                StartCoroutine(CoWaveSpawn_3wave(4 + repeat, correction));
                StartCoroutine(elitePool.CoWaveSpawn(repeat, correction));
                break;
            case 2:
                correction += increCorrection;
                StartCoroutine(CoWaveSpawn(10 + repeat, correction));
                StartCoroutine(CoWaveSpawn_3wave(4, correction));
                StartCoroutine(elitePool.CoWaveSpawn_3wave(4 + repeat, correction));
                break;
            case 3:
                StartCoroutine(CoWaveSpawn(10, correction));
                StartCoroutine(CoWaveSpawn_3wave(4 + repeat, correction));
                StartCoroutine(elitePool.CoWaveSpawn(repeat, correction));
                StartCoroutine(elitePool.CoWaveSpawn_3wave(4 + repeat, correction));
                break;
            case 4:
                StartCoroutine(bossPool.CoWaveSpawn(repeat, correction * 10f, 2));
                correction += increCorrection;
                repeat++;
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
