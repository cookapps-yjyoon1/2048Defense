using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController_Dmg : VFXController
{
    [SerializeField] Collider2D col;
    float dmg;

    public override void StartVFX(float _dmg)
    {
        StartCoroutine(CoVFX(_dmg));
    }

    IEnumerator CoVFX(float _dmg)
    {
        SoundManager.Instance.Play(Enum_Sound.Effect, "Boom",0,0.2f);
        vfx.Play();
        dmg = _dmg;
        col.enabled = true;
        yield return new WaitForSeconds(0.1f);
        col.enabled = false;
        yield return new WaitForSeconds(endTime);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().Hit(dmg);
        }
    }
}
