using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TowerController : Tower
{
    WaitForSeconds wfsAtk = new WaitForSeconds(1);
    WaitForSeconds wfsReload = new WaitForSeconds(0.5f);
    WaitForFixedUpdate wffUpdate = new WaitForFixedUpdate();

    [SerializeField] BulletPool bulletPool;
    
    [SerializeField] Transform target;
    [SerializeField] Transform gun;
    List<Transform> targetGroup = new List<Transform>();

    private void Start()
    {
        StartCoroutine(CoFindTarget());
        StartCoroutine(CoAtk());
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
                yield return Shot(target);
            }

            yield return new WaitForSeconds(attackSpeed);
        }
    }

    YieldInstruction Shot(Transform _target)
    {
        if (curArrowIndexMax > 0)
        {
            Vector3 shotDir = (_target.position - bulletPool.transform.position).normalized;
            //bool isCri = Random.value < criChance;
            gun.rotation = Quaternion.LookRotation(Vector3.forward, shotDir);
            bulletPool.Shot(shotDir, curArrow.Index, curArrow.Level);

            _curIndex++;

            Refresh();
            
            if (_curIndex == curArrowIndexMax)
            {
                _curIndex = 0;
                _towerUI.Reload();
                return wfsReload;
            }
        }

        return null;
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
