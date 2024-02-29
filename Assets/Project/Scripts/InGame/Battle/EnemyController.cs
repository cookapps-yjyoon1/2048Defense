using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    WaitForSeconds wfsAtk = new WaitForSeconds(1);
    WaitForFixedUpdate wffUpdate = new WaitForFixedUpdate();

    [SerializeField] float maxHp;
    [SerializeField] float nowHp;
    [SerializeField] float atkDmg;
    [SerializeField] float atkDist;
    [SerializeField] float moveSpeed;
    [SerializeField] WallController target;
    
    public void InitData(float _waveBuff, WallController _target)
    {
        target = _target;
        nowHp = maxHp * _waveBuff;
        //atkDmg *= _waveBuff;
        StartCoroutine(CoMove());
    }
    
    public void GameOver()
    {
        StopAllCoroutines();
    }

    IEnumerator CoMove()
    {
        //transCha.transform.localScale = (transform.position.x > Target.transform.position.x) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        while (true)
        {
            if (Mathf.Abs(transform.position.y - target.transform.position.y) > atkDist)
            {
                transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
                //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                StartCoroutine(CoAtk());
                yield break;
            }
            yield return wffUpdate;
        }
    }

    IEnumerator CoAtk()
    {
        while (true)
        {
            if (Mathf.Abs(transform.position.y - target.transform.position.y) <= atkDist)
            {
                //target.Hit(atkDmg);
            }
            else
            {
                StartCoroutine(CoMove());
                yield break;
            }
            yield return wfsAtk;
        }
    }

    public void Hit(float _dmg)
    {
        nowHp -= _dmg;
        if (nowHp <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        StopAllCoroutines();
        UnitList.enumyList.Remove(this);
        gameObject.SetActive(false);
    }
}