using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletMult : Bullet
{
    public float speed = 10f;
    [SerializeField] Rigidbody2D rb;

    override public void InitData(Vector3 _dir, float _dmg)
    {
        Dmg = _dmg;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, _dir);

        StartCoroutine(CoShot(_dir));

        for (int i = 0; i < 5; i++)
        {
            _dir = new Vector3(_dir.x + Random.Range(-1.0f, 1f), _dir.y + Random.Range(-1.0f, 1f), 0);
            GameManager.instance.commonBulletPool.Shot(_dir, 0, Dmg);
        }
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
