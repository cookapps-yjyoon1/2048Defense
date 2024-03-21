using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnMouseDown()
    {
        // 코인 획득 이펙트를 생성합니다.
        GameManager.instance.vfxPool.Spawn(2, 0, transform.position);
        // 코인을 획득한 것으로 처리하고 게임 오브젝트를 비활성화합니다.
        HandleCoinPickup();
    }

    private void HandleCoinPickup()
    {
        // 코인을 획득한 것으로 처리하는 코드를 작성합니다.
        // 여기에는 획득한 코인을 플레이어의 점수에 추가하는 등의 작업을 수행할 수 있습니다.
        GameManager.instance.MoveEnergyFull(1);


        // 코인 게임 오브젝트를 비활성화합니다.
        gameObject.SetActive(false);
    }
}
