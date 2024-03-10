using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNormal : Bullet
{
    public float speed = 10f;
    [SerializeField] Rigidbody2D rb;

    override public void InitData(Vector3 _dir, float _dmg)
    {
        Dmg = _dmg;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, _dir);

        StartCoroutine(CoShot(_dir));
    }

    IEnumerator CoShot(Vector3 _dir)
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
            other.GetComponent<EnemyController>().Hit(Dmg);
            
            gameObject.SetActive(false);
        }
    }
}
