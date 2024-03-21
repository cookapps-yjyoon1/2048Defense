using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectController : MonoBehaviour
{

    float moveDuration = 0.4f; // 이동하는 데 걸리는 시간
    Ease easeType = Ease.OutQuad; // 이동의 감속 유형
    Vector3 targetPos;

    public void ObjMove()
    {
        targetPos = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), transform.position.z + Random.Range(-0.5f, 0.5f));
        transform.DOMove(targetPos, moveDuration).SetEase(easeType);
    }

    private void OnMouseDown()
    {
        GameManager.instance.vfxPool.Spawn(2, 0, transform.position);
        HandleCoinPickup();
    }

    private void HandleCoinPickup()
    {
        GameManager.instance.MoveEnergyFull(1);
        gameObject.SetActive(false);
    }
}
