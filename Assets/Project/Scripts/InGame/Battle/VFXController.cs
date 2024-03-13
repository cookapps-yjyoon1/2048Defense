using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    [SerializeField] protected ParticleSystem vfx;
    [SerializeField] protected float endTime;

    public virtual void StartVFX(float _dmg) 
    {
        StartCoroutine(CoVFX(_dmg));
    }

    IEnumerator CoVFX(float _dmg)
    {
        vfx.Play();
        yield return new WaitForSeconds(endTime);
        gameObject.SetActive(false);
    }
}
