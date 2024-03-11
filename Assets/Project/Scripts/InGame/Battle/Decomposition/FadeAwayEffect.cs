using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FadeAwayEffect : MonoBehaviour
{
    public int _index = -1; 
    public float fadeDuration = 2f;
    public float scaleMultiplier = 2f;
    public float riseSpeed = 1f;
    public float rotateSpeed = 100f;

    [SerializeField]private SpriteRenderer spriteRenderer;
    private float currentTime = 0;

    public void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime); 
    }

    public void OnEffect(int index)
    {
        _index = index;
        StartCoroutine(CoEffect());
    }

    IEnumerator CoEffect()
    {
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, currentTime / fadeDuration);
            spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;
            transform.DORotate(180f);
            yield return null;
        }
        
        currentTime = 0;
        _index = -1;
        DecompositionPool.Instance.ReturnObjectToPool(_index,gameObject);
    }
}