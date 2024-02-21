using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNormal : Bullet
{
    public float speed = 10f;
    [SerializeField] Rigidbody2D rb;

    override public void InitData(Vector2 _dir, float _dmg)
    {
        Dmg = _dmg;
        StartCoroutine(CoShoot(_dir));
    }

    IEnumerator CoShoot(Vector2 _dir)
    {
        float timer = 0;

        while (timer < 2) 
        {
            rb.velocity = _dir * speed;
            timer += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            //other.GetComponent<EnemyController>().Hit(Dmg);
            gameObject.SetActive(false);
        }
    }
}
