using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsEnergyAuto : MonoBehaviour
{
    [SerializeField] private GameObject _goAds;

    private void Start()
    {
        if (GameManager.Instance.curStage == 0)
        {
            _goAds.SetActive(false);
        }
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
                GameManager.Instance.energyAutoAds = true;
                _goAds.SetActive(false);
            }
        });
    }
}
