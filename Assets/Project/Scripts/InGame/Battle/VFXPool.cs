using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class VFXPool : ObjectPool
{
    public void Spawn(int _Index, float _dmg, Vector3 _pos)
    {
        VFXController vfx = Get(_Index, _pos).GetComponent<VFXController>();

        vfx.StartVFX(_dmg);
    }
}
