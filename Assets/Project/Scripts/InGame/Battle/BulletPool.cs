using UnityEngine;

public class BulletPool : ObjectPool
{
    public void Shot(Vector2 _dir, int _bulType, float _dmg)
    {
        Bullet bul = Get(_bulType, transform.position).GetComponent<Bullet>();
        bul.InitData(_dir, _dmg);
        SoundManager.Instance.PlayRandomIndex("Shoot",0.1f,1f);
    }
}