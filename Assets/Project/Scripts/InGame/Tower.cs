using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour , IDropHandler
{
    [SerializeField] private TextMeshProUGUI _txtIndex;
    [SerializeField] private TextMeshProUGUI _txtLevel;
    [SerializeField] private TextMeshProUGUI _txtCount;
    
    public int Index;
    public int Level;
    public int Count;

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
        var index = DragAndDropHandler.Index;
        var level = DragAndDropHandler.Level;

        Debug.Log("Equip");
        
        if (Index == index && Level == level)
        {
            Index = index;
            Level = level;
            Count++;
            ClearDrop();
            return;
        }

        if (Level < level)
        {
            Index = index;
            Level = level;
            Count = 1;
            ClearDrop();
            return;
        }
    }

    private void Refresh()
    {
        var isOff = Index < 0 || Level < 0 || Count <= 0;
        
        _txtIndex.gameObject.SetActive(!isOff);
        _txtLevel.gameObject.SetActive(!isOff);
        _txtCount.gameObject.SetActive(!isOff);

        _txtIndex.text = Index.ToString();
        _txtLevel.text = $"Lv.{Level}";
        _txtCount.text = $"Equip {Count}";
    }

    private void ClearDrop()
    {
        DragAndDropHandler.Clear();
        Destroy(DragAndDropHandler.Block);
        Refresh();
    }
}
