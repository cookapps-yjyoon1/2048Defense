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

    Color color = new Color(1f, 1f, 1f, 1f);

    public void MoveOrderInLayer()
    {
        foreach (SpriteRenderer renderer in spriteArr)
        {
            int newSortingOrder = -Mathf.RoundToInt(transform.position.y * 1000f) + Mathf.RoundToInt(transform.position.x*10);
            renderer.sortingOrder = newSortingOrder;
        }
    }

    public void Init()
    {
        spriteArr[0].sprite = hairArr[Random.Range(0, hairArr.Length)];
        spriteArr[1].sprite = faceArr[Random.Range(0, faceArr.Length)];

        foreach (SpriteRenderer renderer in spriteArr)
        {
            renderer.color = color;
        }
    }
}
