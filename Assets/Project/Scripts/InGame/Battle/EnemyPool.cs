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

    private void Start()
    {
        StartCoroutine(TestSpawn());
    }

    public void Spawn(float _buff, int _unitIndex)
    {
        //Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomX = Random.Range(-randomXvalue, randomXvalue);
        Vector2 spawnPos = new Vector3(randomX, transform.position.y, 0f);

        EnemyController unit = Get(_unitIndex, spawnPos).GetComponent<EnemyController>();


        unit.InitData(_buff, target);
    }

    IEnumerator TestSpawn()
    {
        while (true)
        {
            Spawn(1, 0);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
