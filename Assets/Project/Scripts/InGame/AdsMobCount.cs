using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsMobCount : MonoBehaviour
{
    public GameObject _goAds;
    int count;
    public bool off;

    private void Start()
    {
        count = 0;
        off = false;
        _goAds.SetActive(false);
    }

    public void ShowAds()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        CookApps.Admob.CAppAdmob.Rewarded.Show((isSuccess) =>
        {
            if (isSuccess)
            {
                GameManager.Instance.mobCount += 500;
                GameManager.Instance.txtMobCount.text = GameManager.Instance.mobCount.ToString();
                _goAds.SetActive(false);
                count++;
                
                if(count == 2)
                {
                    off = true;
                }
            }
        });
    }
}
