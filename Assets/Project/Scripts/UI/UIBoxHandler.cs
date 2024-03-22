using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIBoxHandler : SingletonBehaviour<UIBoxHandler>
{
    [SerializeField] private List<UI_Box> _uiBoxes;
    private BoxData _boxData => PlayerDataManager.BoxData;

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < _uiBoxes.Count; i++)
        {
            _uiBoxes[i].Init(i);
        }
    }

    private void OnEnable()
    {
        TimeManager.Instance.AddOnTickCallback(RefreshBox);
    }

    public void RefreshBox()
    {
        for (int i = 0; i < _uiBoxes.Count; i++)
        {
            _uiBoxes[i].Refresh();
        }
    }

    public int GetActiveBoxCount()
    {
        return _uiBoxes.Count(x => x.gameObject.activeSelf);
    }
}