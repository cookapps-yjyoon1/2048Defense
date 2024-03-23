using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform cha;
    [SerializeField] float maxHp;
    [SerializeField] float nowHp;
    [SerializeField] float atkDmg;
    [SerializeField] float atkDist;
    [SerializeField] float moveSpeed;
    [SerializeField] DecompositionController _decompositionController;
    [SerializeField] WallController target;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRendererController spriteCon;
    [SerializeField] Collider2D col;
    int enemyType;

    WaitForSeconds wfsAtk = new WaitForSeconds(1);
    WaitForSeconds wfsDeath = new WaitForSeconds(1f);
    WaitForFixedUpdate wffUpdate = new WaitForFixedUpdate();
    Vector3 orgTransScale = new Vector3(1, 1, 1);
    Vector3 flipTransScale = new Vector3(-1, 1, 1);

    bool isDecomposition1 = false;

    public void InitData(WallController _target, float _correction, int _enemyType)
    {
        col.enabled = true;
        target = _target;
        nowHp = maxHp * _correction;
        isDecomposition1 = false;
        _decompositionController.BodyReset();
        enemyType = _enemyType;

        spriteCon.Init();

        if (Random.Range(0, 2) == 0)
        {
            transform.localScale = orgTransScale;
        }
        else
        {
            transform.localScale = flipTransScale;
        }
        
        if (_enemyType == 1)
        {
            cha.localScale = Vector3.one * 0.45f;
        }
        else if (_enemyType == 2)
        {
            cha.localScale = Vector3.one * 0.55f;
        }
        else if (_enemyType == 3)
        {
            transform.position = new Vector3(0, GameManager.Instance.enemyPool.transform.position.y, GameManager.Instance.enemyPool.transform.position.z);
            cha.localScale = Vector3.one * 0.85f;
        }

        StartCoroutine(CoMove());
    }

    public void GameOver()
    {
        StopAllCoroutines();
    }

    IEnumerator CoMove()
    {
        if (Random.Range(0, 2) == 0)
        {
            animator.Play("walk1_1");
        }
        else
        {
            animator.Play("walk1_2");
        }

        //transCha.transform.localScale = (transform.position.x > Target.transform.position.x) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        while (true)
        {
            if (Mathf.Abs(transform.position.y - target.transform.position.y) > atkDist)
            {
                transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
                spriteCon.MoveOrderInLayer();
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
        animator.Play("atk");

        while (true)
        {
            if (Mathf.Abs(transform.position.y - target.transform.position.y) <= atkDist)
            {
                target.Hit(atkDmg);
                SoundManager.Instance.Play(Enum_Sound.Effect, "TowerHit", 0, 0.5f);
            }
            else
            {
                StartCoroutine(CoMove());
                yield break;
            }
            yield return wfsAtk;
        }
    }

    IEnumerator CoDeath()
    {
        yield return wfsDeath;
        gameObject.SetActive(false);
    }

    public void Hit(float _dmg)
    {
        SoundManager.Instance.Play(Enum_Sound.Effect, "Hit",0,0.3f);
        nowHp -= _dmg;

        // if (!isDecomposition1)
        // {
        //     isDecomposition1 = true;
        //     _decompositionController.ActiveRandom();
        // }

        if (nowHp <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        col.enabled = false;
        animator.Play("die");
        //CameraManager.Instance.Shake(0.05f, 0.04f);
        GameManager.Instance.MobCount();

        switch (enemyType)
        {
            case 0:
                GameManager.Instance.MoveEnergy(transform.position);
                break;
            case 1:
                GameManager.Instance.MoveEnergy(transform.position);
                break;
            case 2:
                GameManager.Instance.MoveEnergy(transform.position);
                break;
            case 3:
                GameManager.Instance.GameClear();
                break;
        }

        GameManager.Instance.unitList.enumyList.Remove(this);

        StopAllCoroutines();
        StartCoroutine(CoDeath());
    }
}