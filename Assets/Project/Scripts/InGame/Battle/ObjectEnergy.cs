using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.PlayerLoop.PostLateUpdate;
using UnityEngine.U2D;

public class ObjectEnergy : MonoBehaviour
{

    float moveDuration = 0.4f; // 이동하는 데 걸리는 시간
    Ease easeType = Ease.OutQuad; // 이동의 감속 유형
    Vector3 targetPos;
    int energy;
    Vector3 wallPos;

    public void InitData(int _energy)
    {
        wallPos = GameManager.instance.enemyPool.target.transform.position;

        energy = _energy;
        targetPos = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), transform.position.z + Random.Range(-0.5f, 0.5f));
        transform.DOMove(targetPos, moveDuration).SetEase(easeType);

        StartCoroutine(CoMoveDown());
    }

    private void OnMouseDown()
    {
        GameManager.instance.vfxPool.Spawn(3, 0, transform.position);
        GameManager.instance.obPool.SpawnTxt(1, transform.position, energy.ToString());
        HandleCoinPickup();
    }

    private void HandleCoinPickup()
    {
        GameManager.instance.MoveEnergyFull(energy);
        gameObject.SetActive(false);
    }

    IEnumerator CoMoveDown()
    {
        while (true)
        {
            if (Mathf.Abs(transform.position.y - wallPos.y) > 0.2f)
            {
                //print();
                transform.Translate(Vector3.down * 0.4f * Time.deltaTime);
            }
            else
            {
                //OnMouseDown();
                yield break;
            }

            yield return null;
        }
    }
}
