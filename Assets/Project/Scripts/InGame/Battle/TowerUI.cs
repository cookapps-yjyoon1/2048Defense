using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> _goBulletSlot;
    [SerializeField] private List<GameObject> _goBullet;
    [SerializeField] private GameObject _goReloadText;

    private int _count = 0;
    
    public void UpdateUI(int count,int currentIndex)
    {
        _count = count;
        for (int i = 0; i < _goBulletSlot.Count; i++)
        {
            _goBulletSlot[i].SetActive(i < count);
        }

        for (int i = 0; i < _goBullet.Count; i++)
        {
            _goBullet[i].SetActive(currentIndex-1 < i);
        }
    }

    public void Reload()
    {
        StartCoroutine(CoReload());
    }

    IEnumerator CoReload()
    {
        WaitForSeconds wfs = new WaitForSeconds(0.5f / _count);
        
        _goReloadText.SetActive(true);
        
        for (int i = _count; i >= 0; i--)
        {
            _goBullet[i].SetActive(true);
            yield return wfs;
        }
        
        _goReloadText.SetActive(false);
    }
}
