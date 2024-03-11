using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRendererController : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] spriteArr = new SpriteRenderer[8]; // 0 => hair, 1 => face
    [SerializeField] Sprite[] hairArr;
    [SerializeField] Sprite[] faceArr;

    //[SerializeField] SpriteRenderer body;
    //[SerializeField] SpriteRenderer head;
    //[SerializeField] SpriteRenderer face;
    //[SerializeField] SpriteRenderer face_decoration;
    //[SerializeField] SpriteRenderer hair;
    //[SerializeField] SpriteRenderer back_hair;
    //[SerializeField] SpriteRenderer back_arm;
    //[SerializeField] SpriteRenderer front_arm;



    public void MoveOrderInLayer()
    {
        foreach (SpriteRenderer renderer in spriteArr)
        {
            int newSortingOrder = -Mathf.RoundToInt(transform.position.y * 10f);
            renderer.sortingOrder = newSortingOrder;
        }
    }

    public void AtkOrderInLayer()
    {
        foreach (SpriteRenderer renderer in spriteArr)
        {
            int newSortingOrder = -Mathf.RoundToInt(transform.position.y * 10f);
            renderer.sortingOrder = newSortingOrder;
        }
    }

    public void Init()
    {
        spriteArr[0].sprite = hairArr[Random.Range(0, hairArr.Length)];
        spriteArr[1].sprite = faceArr[Random.Range(0, faceArr.Length)];

        foreach (SpriteRenderer renderer in spriteArr)
        {
            Color color = renderer.color;
            color.a = 1f;
            renderer.color = color;
        }
    }
}
