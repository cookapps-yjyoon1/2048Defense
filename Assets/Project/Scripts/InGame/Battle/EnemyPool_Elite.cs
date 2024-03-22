using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool_Elite : ObjectPool
{
    [SerializeField] WallController target;
    [SerializeField] float randomXvalue;

    int mobType;

    public void GameStart(int _stage)
    {
        mobType = GameManager.Instance.stageData.stage[_stage].mob_elite.Count;

        for (int i = 0; i < mobType; i++)
        {
            prefab[i] = GameManager.Instance.stageData.stage[_stage].mob_elite[i];
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

    public void Spawn(float _correction, int _curWave)
    {
        int repeat = (_curWave / 10) + 1;

        for (int i = 0; i < repeat; i++)
        {
            float randomX = Random.Range(-randomXvalue, randomXvalue);
            Vector3 spawnPos = new Vector3(randomX, transform.position.y, transform.position.z);
            EnemyController unit = Get(Random.Range(0, mobType), spawnPos).GetComponent<EnemyController>();
            unit.InitData(target, _correction, 1);
            GameManager.Instance.unitList.enumyList.Add(unit);
        }
    }

    public IEnumerator CoWaveSpawn(int _repeat, float _correction)
    {
        int delayTime = 15 / _repeat;

        for (int i = 0; i < _repeat; i++)
        {
            Spawn(_correction, 1);
            yield return new WaitForSeconds(delayTime);
        }
    }

    public IEnumerator CoWaveSpawn_3wave(int _repeat, float _correction)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < _repeat; j++)
            {
                Spawn(_correction, 1);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(3f);
        }
    }
}
