using System.Collections.Generic;
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
        
        RefreshBox();
    }

    public void RefreshBox()
    {
        for (int i = 0; i < _uiBoxes.Count; i++)
        {
            if (i < _boxData.Boxes.Count)
            {
                _uiBoxes[i].gameObject.SetActive(true);
                continue;
            }

            _uiBoxes[i].gameObject.SetActive(false);
        }
    }
}