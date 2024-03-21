using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallController : MonoBehaviour
{
    [SerializeField] Slider hpSlider;
    [SerializeField] float maxHp;
    [SerializeField] float curHp;
    
    private float time = 0;

    private void Start()
    {
        curHp = maxHp;
        hpSlider.maxValue = maxHp;
        hpSlider.value = maxHp;
        StartCoroutine(CheckHitState());
    }

    public void Hit(float _dmg)
    {
        curHp -= _dmg;
        hpSlider.value = curHp;
        
        GameManager.instance.HitTowerEvent();
        time = 2;

        if (curHp <= 0)
        {
            gameObject.SetActive(false);
            GameManager.instance.GameOver();
        }
    }

    IEnumerator CheckHitState()
    {
        while (true)
        {
            while (time > 0)
            {
                time -= Time.deltaTime;

                if (time < 0)
                {
                    GameManager.instance.HitTowerEventEnd();
                }

                yield return null;
            }

            yield return null;
        }
    }
}
