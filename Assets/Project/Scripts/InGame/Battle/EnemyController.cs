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
    [SerializeField] float def;
    [SerializeField] WallController target;
    
    public void InitData(WallController _target, float _increaseHp , float _increaseDef)
    {
        target = _target;
        maxHp += _increaseHp;
        nowHp = maxHp;
        def += _increaseDef;
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
        SoundManager.Instance.Play(Enum_Sound.Effect,"Hit");
        nowHp -= _dmg;
        if (nowHp <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        CameraManager.Instance.Shake(0.1f,0.1f);
        StopAllCoroutines();
        UnitList.enumyList.Remove(this);
        gameObject.SetActive(false);
    }
}