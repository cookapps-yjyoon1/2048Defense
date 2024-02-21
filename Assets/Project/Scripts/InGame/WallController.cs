using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallController : MonoBehaviour
{
    [SerializeField] Slider hpSlider;
    public float maxHp;
    public float curHp;

    public void Hit(float _dmg)
    {
        curHp -= _dmg;
        if (curHp <= 0)
        {
            //StopCoroutine(CoAtk());
            gameObject.SetActive(false);
        }
    }
}
