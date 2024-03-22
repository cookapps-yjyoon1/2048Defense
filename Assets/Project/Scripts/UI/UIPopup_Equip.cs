using System;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup_Equip : MonoBehaviour
{
    [SerializeField] private UIPopup_Equip_Slot[] slots;
    
    public void OnClickButton()
    {
        gameObject.SetActive(!gameObject.activeSelf);

        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0; i < 3; i++)
        {
            slots[i].Refresh(i);
        }
    }
}