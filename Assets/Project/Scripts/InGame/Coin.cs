using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnMouseDown()
    {
        // ���� ȹ�� ����Ʈ�� �����մϴ�.
        GameManager.instance.vfxPool.Spawn(2, 0, transform.position);
        // ������ ȹ���� ������ ó���ϰ� ���� ������Ʈ�� ��Ȱ��ȭ�մϴ�.
        HandleCoinPickup();
    }

    private void HandleCoinPickup()
    {
        // ������ ȹ���� ������ ó���ϴ� �ڵ带 �ۼ��մϴ�.
        // ���⿡�� ȹ���� ������ �÷��̾��� ������ �߰��ϴ� ���� �۾��� ������ �� �ֽ��ϴ�.
        GameManager.instance.MoveEnergyFull(1);


        // ���� ���� ������Ʈ�� ��Ȱ��ȭ�մϴ�.
        gameObject.SetActive(false);
    }
}
