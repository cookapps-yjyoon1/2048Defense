using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool
{
    [SerializeField] WallController target;
    [SerializeField] float randomXvalue;

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

    public void GameStart()
    {
        StartCoroutine(TestSpawn());
    }

    public void Spawn(int _unitIndex, float _hp, float _def)
    {
        float randomX = Random.Range(-randomXvalue, randomXvalue);
        Vector3 spawnPos = new Vector3(randomX, transform.position.y, transform.position.z);
        EnemyController unit = Get(_unitIndex, spawnPos).GetComponent<EnemyController>();
        unit.InitData(target, _hp, _def);
        UnitList.enumyList.Add(unit);
    }

    IEnumerator TestSpawn()
    {
        while (true)
        {
            Spawn(0, 1, 1);
            yield return new WaitForSeconds(0.5f);
        }
    }


    IEnumerator CoSpawn00()
    {
        while (true)
        {
            Spawn(0, 1, 1);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator CoSpawn01()
    {
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                Spawn(0, 1, 1);
            }
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator CoSpawn02()
    {
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                Spawn(0, 1, 1);
            }
            yield return new WaitForSeconds(3f);
        }
    }

}
