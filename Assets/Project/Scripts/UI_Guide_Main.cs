using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UI_Guide_Main : MonoBehaviour
{
    [SerializeField] private List<CanvasGroup> _gameObjects;
    [SerializeField] private CanvasGroup _main;

    private int _currentIndex = 0;
    
    public void Open()
    {
        _main.alpha = 1;
        _gameObjects.ForEach(x=>x.DOFade(0,0f));
        _gameObjects[0].DOFade(1, 0.5f);
        gameObject.SetActive(true);
    }

    public void Close()
    {
        PlayerDataManager.ETCData.IsFirstPlay = false;
        PlayerDataManager.Instance.SaveLocalData();
        gameObject.SetActive(false);
    }

    public void OnClick()
    {
        if (_currentIndex >= _gameObjects.Count-1)
        {
            _main.DOFade(0, 0.5f);
            gameObject.SetActive(false);
            return;
        }
        
        _gameObjects[_currentIndex].DOFade(0, 0.5f).OnComplete(() =>
        {
            _gameObjects[_currentIndex+1].DOFade(1, 0.5f);
        });
    }
    
    private void Start()
    {
        if (!PlayerDataManager.ETCData.IsFirstPlay)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
}
