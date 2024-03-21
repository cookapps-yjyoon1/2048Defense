using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnPool : ObjectPool
{
    protected override void Awake()
    {
        base.Awake();
        PreCreatePoolObject();
    }

    public void Spawn(int _Index, Vector3 _pos)
    {
        GameObject obj = Get(_Index, _pos);
    }
}
