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
    [SerializeField] float criChance;
    [SerializeField] float criFactor;

    Transform target;

    private void Start()
    {
        StartCoroutine(CoFindTarget());
        StartCoroutine(CoAtk());

        // 나중에 시트 연동
        dmg = 1;
        atkSpeed = 1;
        criChance = 0.5f;
        criFactor = 2;
    }

    void FirstDistTarget(List<Transform> _targetList)
    {
        float firstDist = 10000;
        float curDist = 0;
        int firstDistIndex = 0;

        for (int i = 0; i < _targetList.Count; i++)
        {
            curDist = Vector3.Distance(transform.position, _targetList[i].transform.position);

            if (firstDist > curDist)
            {
                firstDistIndex = i;
                firstDist = curDist;
            }
        }
        firstDist = 10000;
        target = _targetList[firstDistIndex];
    }

    IEnumerator CoFindTarget()
    {
        while (true)
        {
            if (UnitList.targetList.Count != 0)
            {
                FirstDistTarget(UnitList.targetList);
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
            if (target != null)
            {
                Shot(target);

                yield return new WaitForSeconds(atkSpeed);
            }
            yield return null;
        }
    }

    void Shot(Transform _target)
    {
        Vector2 shotDir = (_target.position - transform.position).normalized;
        bool isCri = Random.value < criChance;
        bulletPool.Shot(shotDir, 0, DmgCalculation(dmg, isCri));
    }

    int DmgCalculation(float _dmg, bool _isCri)
    {
        if (_isCri)
        {
            _dmg = criFactor * _dmg;
        }

        return Mathf.CeilToInt(_dmg);
    }
}
