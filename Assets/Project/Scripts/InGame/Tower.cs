using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour , IDropHandler
{
    List<Arrow> arrows = new List<Arrow>();

    protected float dmg;
    protected float attackSpeed => 1f / (float)Mathf.Clamp(curArrowIndexMax,1,curArrowIndexMax);
    protected int curArrowIndexMax => arrows.Count;
    protected Arrow curArrow => arrows[_curIndex];
    protected int _curIndex = 0;
    
    [SerializeField] protected TowerUI _towerUI;

    private void Start()
    {
        Refresh();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragAndDropHandler.Index < 0 || DragAndDropHandler.Level < 0)
        {
            return;
        }
        
        Equip();
    }

    public void Equip()
    {
        if (arrows.Count > 10)
        {
            return;
        }
        
        var index = DragAndDropHandler.Index;
        var level = DragAndDropHandler.Level;
        
        arrows.Add(new Arrow(index,level));
        
        ClearDrop();
        Refresh();
    }

    protected void Refresh()
    {
        _towerUI.UpdateUI(curArrowIndexMax,_curIndex);
    }

    private void ClearDrop()
    {
        DragAndDropHandler.Clear();
        Destroy(DragAndDropHandler.Block);
        Refresh();
    }
}
