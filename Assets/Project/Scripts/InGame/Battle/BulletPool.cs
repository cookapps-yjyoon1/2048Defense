using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletPool : ObjectPool
{
    public void Shot(Vector2 _dir, int _bulType, float _dmg)
    {
        Bullet bul = Get(_bulType, transform.position).GetComponent<Bullet>();
        bul.InitData(_dir, _dmg);
    }
}