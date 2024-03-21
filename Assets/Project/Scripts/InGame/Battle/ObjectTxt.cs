using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTxt : MonoBehaviour
{
    float moveDuration = 0.4f; // 이동하는 데 걸리는 시간
    Ease easeType = Ease.OutQuad; // 이동의 감속 유형
    Vector3 targetPos;
    [SerializeField] Text txtEnergy;

    public void InitData(string _str)
    {
        txtEnergy.text = _str;
        targetPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        transform.DOMove(targetPos, moveDuration).SetEase(easeType).OnComplete(() => gameObject.SetActive(false));
    }
}
