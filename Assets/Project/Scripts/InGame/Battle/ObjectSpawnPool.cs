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

    public void Spawn(int _Index, Vector3 _pos, int _repeat)
    {
        for (int i = 0; i < _repeat; i++)
        {
            ObjectController obj = Get(_Index, _pos).GetComponent<ObjectController>();

            obj.ObjMove();

        }
    }
}
