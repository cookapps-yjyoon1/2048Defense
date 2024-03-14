using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletMult : Bullet
{
    enum Enum_VFX_Type
    {
        Bullet00,
        Bullet01,
        Bullet02,
    }

    public float speed = 10f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Enum_VFX_Type vfxType;

    override public void InitData(Vector3 _dir, float _dmg)
    {
        Dmg = _dmg;
        
        transform.rotation = Quaternion.LookRotation(Vector3.forward, _dir);

        for (int i = 0; i < 5; i++)
        {
            _dir = new Vector3(_dir.x + Random.Range(-0.2f, 0.2f), _dir.y + Random.Range(-0.2f, 0.2f), 0);
            GameManager.instance.commonBulletPool.Shot(_dir, 0, Dmg, transform);
        }

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
            GameManager.instance.vfxPool.Spawn((int)vfxType, 0, transform.position);
            gameObject.SetActive(false);
        }
    }
}
