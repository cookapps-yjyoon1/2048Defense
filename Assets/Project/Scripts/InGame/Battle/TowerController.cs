using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TowerController : Tower
{
    WaitForSeconds wfsAtk = new WaitForSeconds(1);
    WaitForFixedUpdate wffUpdate = new WaitForFixedUpdate();

    [SerializeField] BulletPool bulletPool;

    [SerializeField] float dmg;
    [SerializeField] float atkSpeed;

    [SerializeField] Transform target;
    List<Transform> targetGroup = new List<Transform>();

    int curArrowIndexMax;
    int curArrowIndex;

    private void Start()
    {
        StartCoroutine(CoFindTarget());
        StartCoroutine(CoAtk());

        // 나중에 시트 연동
        dmg = 1;
        atkSpeed = 1;

        // 화살 추가 할 때마다 리스트에 추가해야함
        curArrowIndexMax = bulletPool.prefab.Length;
        atkSpeed /= curArrowIndexMax;
    }

    void FirstDistTarget()
    {
        float firstDist = 10000;
        float curDist = 0;
        int firstDistIndex = 0;

        for (int i = 0; i < UnitList.enumyList.Count; i++)
        {
            curDist = Vector3.Distance(transform.position, UnitList.enumyList[i].transform.position); //GameManager.instance.unitList.enumyList[i].transform.position.y;

            if (firstDist > curDist)
            {
                firstDistIndex = i;
                firstDist = curDist;
            }
        }
        target = UnitList.enumyList[firstDistIndex].transform;
        
    }
    IEnumerator CoFindTarget()
    {
        while (true)
        {
            if (UnitList.enumyList.Count != 0)
            {
                FirstDistTarget();
            }
            else
            {
                target = null;
            }
            yield return null;
        }
    }

    IEnumerator CoAtk()
    {
        while (true)
        {
            if (target != null && target.transform.position.y < 2)
            {
                Shot(target);
            }

            yield return new WaitForSeconds(atkSpeed);
        }
    }

    void Shot(Transform _target)
    {
        Vector3 shotDir = (_target.position - bulletPool.transform.position).normalized;
        //bool isCri = Random.value < criChance;
        bulletPool.Shot(shotDir, curArrowIndex, dmg);

        curArrowIndex++;
        
        if(curArrowIndex == curArrowIndexMax)
        {
            curArrowIndex = 0;
        }
    }

    //int DmgCalculation(float _dmg, bool _isCri)
    //{
    //    if (_isCri)
    //    {
    //        _dmg = criFactor * _dmg;
    //    }

    //    return Mathf.CeilToInt(_dmg);
    //}
}
