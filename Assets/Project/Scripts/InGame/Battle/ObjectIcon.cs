using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectIcon : MonoBehaviour
{
    [SerializeField] float duration = 0.5f;
    Ease easeType = Ease.OutQuad; // 이동의 감속 유형
    [SerializeField] TextMeshPro txtEnergy;
    [SerializeField] SpriteRenderer img;

    public void InitData(string _str)
    {
        txtEnergy.text = _str;
        transform.localScale = Vector3.one;

        //transform.DOMoveY(transform.position.y + 0.3f, duration).SetEase(easeType).OnComplete(() => gameObject.SetActive(false));
        transform.DOScale(1.2f, duration).SetEase(Ease.OutQuad).OnComplete(() => gameObject.SetActive(false));
    }
}
