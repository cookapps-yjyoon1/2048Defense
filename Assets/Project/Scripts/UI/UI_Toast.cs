using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UI_Toast : SingletonBehaviour<UI_Toast>
{
    [SerializeField] private UI_ArrowSlot[] _arrowSlots;
    [SerializeField] private CanvasGroup _canvasGroup;

    protected override void Awake()
    {
        base.Awake();
        
gameObject.SetActive(false);
    }

    public void Open(List<int> arrow)
    {
        SoundManager.Instance.Play(Enum_Sound.Effect, "BoxOpen");
        int i = 0;
        for (; i < arrow.Count; i++)
        {
            _arrowSlots[i].Init(i,arrow[i]);
        }

        _canvasGroup.alpha = 0;
        
        gameObject.SetActive(true);

        _canvasGroup.DOFade(1f, 0.2f);
        
        Invoke(nameof(Close),1f);
    }

    public void Close()
    {
        _canvasGroup.DOFade(0f, 0.2f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
