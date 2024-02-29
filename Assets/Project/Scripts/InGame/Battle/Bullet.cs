using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float Dmg { protected get; set; }

    virtual public void InitData(Vector3 _dir, float _dmg) { }
}