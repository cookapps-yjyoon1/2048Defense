using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

using Random = UnityEngine.Random;

[Serializable]
public class Box
{
    public int Level;
    public bool IsProgress;
    public bool IsShowAds;

    public void Init(int level)
    {
        Level = level;
    }

    public void Clear()
    {
        Level = -1;
        IsProgress = false;
        IsShowAds = false;
    }
}

[Serializable]
public class BoxData : GameData
{
    [JsonIgnore] public const int MaxBoxCount = 4;

    [JsonIgnore] public readonly float[] BoxPercents = new float[] { BoxPercent1, BoxPercent2, BoxPrecent3 };
    [JsonIgnore] public readonly long[] BoxTimes = new long[] { BoxTime1, BoxTime2, BoxTime3 };
    [JsonIgnore] public readonly int[] BoxMinPieceAmount = new int[] { MinPieceAmount1, MinPieceAmount2, MinPieceAmount3 };
    [JsonIgnore] public readonly int[] BoxMaxPieceAmount = new int[] { MaxPieceAmount1, MaxPieceAmount2, MaxPieceAmount3 };
    
    
    [JsonIgnore] public const float BoxPercent1 = 70;
    //[JsonIgnore] public const long BoxTime1 = 2;
    [JsonIgnore] public const long BoxTime1 = 900;

    [JsonIgnore] public const int MinPieceAmount1 = 8;
    [JsonIgnore] public const int MaxPieceAmount1 = 12;

    [JsonIgnore] public const float BoxPercent2 = 20;
    //[JsonIgnore] public const long BoxTime2 = 4;
    [JsonIgnore] public const long BoxTime2 = 1800;

    [JsonIgnore] public const int MinPieceAmount2 = 20;
    [JsonIgnore] public const int MaxPieceAmount2 = 26;

    [JsonIgnore] public const float BoxPrecent3 = 10;
    //[JsonIgnore] public const long BoxTime3 = 6;
    [JsonIgnore] public const long BoxTime3 = 3600;
    
    [JsonIgnore] public const int MinPieceAmount3 = 50;
    [JsonIgnore] public const int MaxPieceAmount3 = 56;

    [JsonProperty] public long StartBoxTime;
    [JsonProperty] public long FinishBoxTime;

    [JsonProperty] public List<Box> Boxes = new List<Box>();

    
    protected override eDataType DataType => eDataType.Box;
    public override void Initialize()
    {
        for (int i = Boxes.Count; i < 3; i++)
        {
            Boxes.Add(new Box());
            Boxes[i].Init(1);
        }
    }

    public override void LateInitialize()
    {
    }


    public bool TryAddBox()
    {
        var box = Boxes.Find(x => x.Level < 0);
        
        if (box == null)
        {
            return false;
        }
        
        var level = UtilCode.GetWeightChance(BoxPercents);
        
        box.Init(level);
        
        PlayerDataManager.Instance.SaveLocalData();

        return true;
    }
    

    public bool IsEnableStartBox()
    {
        return !Boxes.Any(x => x.IsProgress);
    }

    public bool TryStartBox(int index)
    {
        if (index >= Boxes.Count)
        {
            return false;
        }

        if (!IsEnableStartBox())
        {
            return false;
        }

        StartBoxTime = TimeManager.Instance.Now;
        FinishBoxTime = StartBoxTime + BoxTimes[Boxes[index].Level];
        
        Boxes[index].IsProgress = true;
        
        PlayerDataManager.Instance.SaveLocalData();

        return true;
    }

    public bool IsEnableShowAds(int index)
    {
        if (IsEnableClaimBox(index))
        {
            return false;
        }

        if (!Boxes[index].IsProgress)
        {
            return false;
        }

        if (Boxes[index].IsShowAds)
        {
            return false;
        }
        
        return true;
    }
    
    public bool IsEnableClaimBox(int index)
    {
        if (index >= Boxes.Count)
        {
            return false;
        }

        if (!Boxes[index].IsProgress)
        {
            return false;
        }
        
        if(FinishBoxTime > TimeManager.Instance.Now)
        {
            return false;
        }
        
        return true;
    }

    public bool TryClaimBox(int index, out List<int> arrows)
    {
        arrows = new List<int>();
        
        if (!IsEnableClaimBox(index))
        {
            return false;
        }
        
        arrows.Add(0);
        arrows.Add(0);
        arrows.Add(0);
        // 박스 초기화
        StartBoxTime = -1;
        FinishBoxTime = -1;
        
        var count = Random.Range(BoxMinPieceAmount[Boxes[index].Level], BoxMaxPieceAmount[Boxes[index].Level]);

        for (int i = 0; i <count; i++)
        {
            var arrowIndex = Random.Range(0, PlayerDataManager.ArrowData.Arrows.Count);
            arrows[arrowIndex]++;
            PlayerDataManager.ArrowData.AddArrowPiece(arrowIndex,1);
        }

        PlayerDataManager.ArrowData.TryUpgradeArrow(0);
        PlayerDataManager.ArrowData.TryUpgradeArrow(1);
        PlayerDataManager.ArrowData.TryUpgradeArrow(2);

        Boxes[index].Clear();

        PlayerDataManager.Instance.SaveLocalData();

        return true;
    }
    
}