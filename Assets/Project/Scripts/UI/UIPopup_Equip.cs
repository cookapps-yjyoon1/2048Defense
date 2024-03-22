using System;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup_Equip : MonoBehaviour
{
    public void Awake()
    {
        //gameObject.SetActive(false);
    }

    public void OnClickButton()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (gameObject.activeSelf)
        {
            PlayerDataManager.BoxData.TryAddBox();
        }

    }
}