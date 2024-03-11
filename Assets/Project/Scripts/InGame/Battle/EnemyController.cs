using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float maxHp;
    [SerializeField] float nowHp;
    [SerializeField] float atkDmg;
    [SerializeField] float atkDist;
    [SerializeField] float moveSpeed;
    [SerializeField] DecompositionController _decompositionController;
    [SerializeField] WallController target;

    // ĳ�̺���
    WaitForSeconds wfsAtk = new WaitForSeconds(1);
    WaitForFixedUpdate wffUpdate = new WaitForFixedUpdate();
    Vector3 orgTransScale = new Vector3(1, 1, 1);
    Vector3 flipTransScale = new Vector3(-1, 1, 1);
    
    bool isDecomposition1 = false;
    bool isDecomposition2 = false;

    public void InitData(WallController _target, float _correction)
    {
        isDecomposition1 = false;
        _decompositionController.Reset();
        target = _target;
        nowHp = maxHp * _correction;
        StartCoroutine(CoMove());

        if (Random.Range(0, 2) == 0)
        {
            transform.localScale = orgTransScale;
        }
        else
        {
            transform.localScale = flipTransScale;
        }
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

        if (!isDecomposition1 && nowHp / maxHp < 0.7f)
        {
            isDecomposition1 = true;
            _decompositionController.ActiveRandom();
        }
        
        if (nowHp <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        CameraManager.Instance.Shake(0.05f,0.04f);
        StopAllCoroutines();
        UnitList.enumyList.Remove(this);
        gameObject.SetActive(false);
    }
}