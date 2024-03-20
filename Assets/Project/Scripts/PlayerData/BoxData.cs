using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Random = UnityEngine.Random;

[Serializable]
public class Box
{
    public int Level;
    public bool IsProgress;

    public void Init(int level)
    {
        Level = level;
    }
}

public class BoxData : GameData
{
    [JsonIgnore] public const int MaxBoxCount = 4;

    public static readonly float[] BoxPercents = new float[] { BoxPrecent1, BoxPrecent2, BoxPrecent3 };
    public static readonly long[] BoxTimes = new long[] { BoxTime1, BoxTime2, BoxTime3 };
    public static readonly int[] BoxMinPieceAmount = new int[] { MinPieceAmount1, MinPieceAmount2, MinPieceAmount3 };
    public static readonly int[] BoxMaxPieceAmount = new int[] { MaxPieceAmount1, MaxPieceAmount2, MaxPieceAmount3 };
    
    
    [JsonIgnore] public const float BoxPrecent1 = 70;
    [JsonIgnore] public const long BoxTime1 = 900;
    [JsonIgnore] public const int MinPieceAmount1 = 8;
    [JsonIgnore] public const int MaxPieceAmount1 = 12;

    [JsonIgnore] public const float BoxPrecent2 = 20;
    [JsonIgnore] public const long BoxTime2 = 1800;
    [JsonIgnore] public const int MinPieceAmount2 = 20;
    [JsonIgnore] public const int MaxPieceAmount2 = 26;

    [JsonIgnore] public const float BoxPrecent3 = 10;
    [JsonIgnore] public const long BoxTime3 = 3600;
    [JsonIgnore] public const int MinPieceAmount3 = 50;
    [JsonIgnore] public const int MaxPieceAmount3 = 56;

    [JsonProperty] public long StartBoxTime;
    [JsonProperty] public long FinishBoxTime;

    [JsonProperty] public List<Box> Boxes = new List<Box>();
    
    protected override eDataType DataType => eDataType.Box;
    public override void Initialize()
    {
    }

    public override void LateInitialize()
    {
    }

    public bool IsEnableAddBox()
    {
        if (Boxes.Count >= MaxBoxCount)
        {
            return false;
        }

        return true;
    }
    
    public bool TryAddBox()
    {
        if (!IsEnableAddBox())
        {
            return false;
        }
        
        var index = UtilCode.GetWeightChance(BoxPercents);

        Box box = new Box();

        box.Init(index);
        Boxes.Add(box);

        return true;
    }

    public bool IsEnableOpenBox()
    {
        return !Boxes.Exists(x => x.IsProgress);
    }

    public bool TryOpenBox(int index)
    {
        if (!IsEnableOpenBox())
        {
            return false;
        }

        StartBoxTime = TimeManager.Instance.Now;
        FinishBoxTime = StartBoxTime + BoxTimes[Boxes[index].Level];
        
        Boxes[index].IsProgress = true;

        return true;
    }

    public bool IsEnableClaimBox(int index)
    {
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

    public bool TryClaimBox(int index)
    {
        if (!IsEnableClaimBox(index))
        {
            return false;
        }

        StartBoxTime = -1;
        FinishBoxTime = -1;
        Boxes[index].IsProgress = false;

        var count = Random.Range(BoxMinPieceAmount[Boxes[index].Level], BoxMaxPieceAmount[Boxes[index].Level]);

        for (int i = 0; i <count; i++)
        {
            PlayerDataManager.ArrowData.AddArrowPiece(Random.Range(0,PlayerDataManager.ArrowData.Arrows.Count),1);
        }

        return true;
    }
    
}