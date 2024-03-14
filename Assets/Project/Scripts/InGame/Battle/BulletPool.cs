using UnityEngine;

public class BulletPool : ObjectPool
{
    public void Shot(Vector2 _dir, int _bulType, float _dmg)
    {
        Bullet bul = Get(_bulType, transform.position).GetComponent<Bullet>();
        bul.InitData(_dir, _dmg);
        SoundManager.Instance.PlayRandomIndex("Shoot",0.7f,1f);
    }

    public void Shot(Vector2 _dir, int _bulType, float _dmg, Transform _trans)
    {
        Bullet bul = Get(_bulType, _trans.position).GetComponent<Bullet>();
        bul.InitData(_dir, _dmg);
        SoundManager.Instance.PlayRandomIndex("Shoot", 0.7f, 1f);
    }
}