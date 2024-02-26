using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    WaitForSeconds wfsAtk = new WaitForSeconds(1);
    WaitForFixedUpdate wffUpdate = new WaitForFixedUpdate();

    [SerializeField] float initHp;
    [SerializeField] float hp;
    [SerializeField] float initAtkDmg;
    [SerializeField] float atkDmg;
    [SerializeField] float atkDist;
    [SerializeField] float moveSpeed;
    [SerializeField] WallController target;
    
    public void InitData(float _waveBuff, WallController _target)
    {
        target = _target;
        hp = initHp * _waveBuff;
        atkDmg = initAtkDmg * _waveBuff;
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
            print(transform.position.y - target.transform.position.y);

            if (transform.position.y - target.transform.position.y > atkDist)
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
            if (transform.position.y - target.transform.position.y <= atkDist)
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
        hp -= _dmg;
        if (hp <= 0)
        {
            StopCoroutine(CoAtk());
            gameObject.SetActive(false);
        }
    }
}