using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ArrowSlot : MonoBehaviour
{
    [SerializeField] private Image _imgIcon;
    [SerializeField] private Text _txtAmount;

    [SerializeField] private Sprite[] sprites;
    public void Init(int index, int amount)
    {
        if (amount <= 0)
        {
            gameObject.SetActive(false);
            return;
        }
        
        _imgIcon.sprite = sprites[index];
        _txtAmount.text = amount.ToString();
        gameObject.SetActive(true);
    }
}
