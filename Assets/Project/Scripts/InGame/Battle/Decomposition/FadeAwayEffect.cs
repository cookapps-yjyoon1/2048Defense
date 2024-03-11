using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FadeAwayEffect : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _renderer;
    public float dropHeight = -5f; // 바닥 위치
    public float dropDuration = 1f; // 떨어지는데 걸리는 시간
    public float fadeDuration = 1f; // 페이드 아웃 시간
    
    public readonly Color whiteColor = new Color(1f, 1f, 1f, 1f);

    public void OnEffect(int index)
    {
        for (int i = 0; i < _renderer.Length; i++)
        {
            _renderer[i].color = whiteColor;
        }

        // DoTween 초기화
        DOTween.Init();

        // 떨어지는 애니메이션
        transform.DOMoveY(transform.position.y + dropHeight, dropDuration).OnComplete(() =>
        {
            // 1초 대기 후 페이드 아웃
            DOVirtual.DelayedCall(1f, () =>
            {
                _renderer[0].DOFade(0f, fadeDuration).OnComplete(() =>
                {
                    DecompositionPool.Instance.ReturnObjectToPool(index, gameObject);
                });
                
                for (int i = 1; i < _renderer.Length; i++)
                {
                    _renderer[i].DOFade(0f, fadeDuration);
                }
            });
        });
    }
}