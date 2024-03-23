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

    public void SpawnEnergy(Vector3 _pos, int _energy)
    {
        ObjectEnergy obj = Get(0, _pos).GetComponent<ObjectEnergy>();
        obj.InitData(_energy);
    }

    public void SpawnTxt(int _index, Vector3 _pos, string _str)
    {
        ObjectTxt obj = Get(_index, _pos).GetComponent<ObjectTxt>();
        obj.InitData(_str);
    }

    public void SpawnIcon(int _index, Vector3 _pos, string _str)
    {
        ObjectIcon obj = Get(_index, _pos).GetComponent<ObjectIcon>();
        obj.InitData(_str);
    }
}
