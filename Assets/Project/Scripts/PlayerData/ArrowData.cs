
using System.Collections.Generic;
using Newtonsoft.Json;

public class ArrowData : GameData
{
    protected override eDataType DataType => eDataType.Arrow;
    public List<int> Arrows = new List<int>();
    public List<int> OwnArrowPiece = new List<int>();
    
    
    //Spec
    [JsonIgnore] public const int MaxLevel = 100;
    [JsonIgnore] public const int UpgradeCost = 100;
    [JsonIgnore] public const float IncreaseDamage = 1.1f;
    
    
    public override void Initialize()
    {
        for (int i = Arrows.Count; i < 3; i++)
        {
            Arrows.Add(0);
        }
        
        for (int i = OwnArrowPiece.Count; i < 3; i++)
        {
            OwnArrowPiece.Add(0);
        }
    }

    public override void LateInitialize()
    {
        
    }

    public bool IsEnoughArrow(int index)
    {
        if (OwnArrowPiece[index] < UpgradeCost)
        {
            return false;
        }

        if (Arrows[index] >= MaxLevel)
        {
            return false;
        }

        return true;
    }

    public bool TryUpgradeArrow(int index)
    {
        if (!IsEnoughArrow(index))
        {
            return false;
        }
        
        Arrows[index]++;
        OwnArrowPiece[index] -= UpgradeCost;

        PlayerDataManager.Instance.SaveLocalData();
        
        return true;
    }

    public void AddArrowPiece(int index, int count)
    {
        OwnArrowPiece[index] += count;
    }
}
