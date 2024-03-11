using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallController : MonoBehaviour
{
    [SerializeField] Slider hpSlider;
    [SerializeField] float maxHp;
    [SerializeField] float curHp;

    private void Start()
    {
        curHp = maxHp;
        hpSlider.maxValue = maxHp;
        hpSlider.value = maxHp;

    }

    public void Hit(float _dmg)
    {
        curHp -= _dmg;
        hpSlider.value = curHp;

        if (curHp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
