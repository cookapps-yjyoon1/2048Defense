using UnityEngine;

public static class DragAndDropHandler
{
    public static int Index;
    public static int Level;
    public static bool IsDragging = false;
    public static GameObject Block;

    public static void Grap(GameObject go,int index, int level)
    {
        Index = index;
        Level = level;
        InGameManager.Instance.DragAndDrop.OnDrag();
        IsDragging = true;
        Block = go;
        Debug.Log($"{Index} , {Level} Grap");
    }
    
    public static void Drop()
    {
        IsDragging = false;
        InGameManager.Instance.DragAndDrop.OnEndDrag();
        Debug.Log($"{Index} , {Level} Drop");
    }

    public static void Clear()
    {
        Index = -1;
        Level = -1;
    }
}