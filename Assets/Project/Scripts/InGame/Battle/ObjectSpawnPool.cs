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

    public void SpawnEnergy(int _Index, Vector3 _pos, int _energy)
    {
        ObjectEnergy obj = Get(_Index, _pos).GetComponent<ObjectEnergy>();
        obj.InitData(_energy);
    }

    public void SpawnTxt(int _Index, Vector3 _pos, string _str)
    {
        ObjectTxt obj = Get(_Index, _pos).GetComponent<ObjectTxt>();
        //obj.InitData(_str, _pos);
    }
}
