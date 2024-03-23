
using System.Collections;
using UnityEngine;

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
        Dmg = _dmg * 0.7f * GameManager.Instance.MultCrrection;
        
        transform.rotation = Quaternion.LookRotation(Vector3.forward, _dir);

        for (int i = 0; i < 5; i++)
        {
            _dir = new Vector3(_dir.x + Random.Range(-0.15f, 0.15f), _dir.y + Random.Range(-0.15f, 0.15f), 0);
            GameManager.Instance.commonBulletPool.Shot(_dir, 0, Dmg, transform);
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
            GameManager.Instance.vfxPool.Spawn((int)vfxType, 0, transform.position);
            GameManager.Instance.obPool.SpawnTxt(2, transform.position, Dmg.ToString("F0"));
            gameObject.SetActive(false);
        }
    }
}
