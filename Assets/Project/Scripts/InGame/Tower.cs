using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.Rendering.DebugUI;

public class Tower : MonoBehaviour, IDropHandler
{
    List<Arrow> arrows = new List<Arrow>();

    protected float dmg;
    protected float attackSpeed => 1f / (float)Mathf.Clamp(curArrowIndexMax, 1, curArrowIndexMax);
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
        if (arrows.Count >= 5)
        {
            arrows.RemoveAt(0);
            GameManager.Instance.MobCountNum(100);
            GameManager.Instance.obPool.SpawnIcon(3, _towerUI.transform.position, "-100");

            for (int i = 0; i < 4; i++)
            {
                _towerUI._goBullet[i].color = _towerUI._goBullet[i + 1].color;
            }
        }
        else
        {
            GameManager.Instance.obPool.SpawnIcon(4, _towerUI.transform.position, "+" + (1 + arrows.Count).ToString());
            GameManager.Instance.MoveEnergyFull(1 + arrows.Count);
            _towerUI.txtEquip.text = (arrows.Count + 1).ToString();
        }

        var index = DragAndDropHandler.Index;
        var level = DragAndDropHandler.Level;

        _towerUI._goBullet[arrows.Count].color = _towerUI.blockColors[(int)Mathf.Log(level, 2) - 1];

        arrows.Add(new Arrow(index, level));
        ClearDrop();
        Refresh();
    }

    protected void Refresh()
    {
        _towerUI.UpdateUI(curArrowIndexMax, _curIndex);
    }

    private void ClearDrop()
    {
        DragAndDropHandler.Clear();
        Destroy(DragAndDropHandler.Block);
        Refresh();
    }
}
