using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectTxt : MonoBehaviour
{
    float moveDuration = 1f;
    [SerializeField] float fadeDuration = 0.6f;
    Ease easeType = Ease.OutQuad; // 이동의 감속 유형
    [SerializeField] TextMeshPro txtEnergy;

    public void InitData(string _str)
    {
        txtEnergy.text = _str;
        transform.localScale = Vector3.one;

        txtEnergy.DOFade(1f, 0f).OnComplete(() =>
        {
            txtEnergy.DOFade(0f, fadeDuration).SetEase(easeType).OnComplete(() => gameObject.SetActive(false));
        });

        transform.DOMoveY(transform.position.y + 0.3f, moveDuration).SetEase(easeType);
        transform.DOScale(1.2f, moveDuration).SetEase(Ease.OutQuad);
    }
}
