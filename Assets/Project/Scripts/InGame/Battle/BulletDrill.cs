using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class BulletDrill : Bullet
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
        Dmg = _dmg * GameManager.Instance.DrillCrrection * 1.2f;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, _dir);

        StartCoroutine(CoShot(_dir));
    }

    IEnumerator CoShot(Vector3 _dir)
    {
        float timer = 0;

        while (timer < 1) 
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
            GameManager.Instance.vfxPool.Spawn((int)vfxType, Dmg, transform.position);
            GameManager.Instance.obPool.SpawnTxt(2, transform.position, Dmg.ToString("F0"));
            other.GetComponent<EnemyController>().Hit(Dmg);
        }
    }
}
