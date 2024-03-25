using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class ETCData : GameData
{
    protected override eDataType DataType => eDataType.ETC;

    [JsonProperty] public List<bool> IsClearStage = new List<bool>();
    [JsonProperty] public List<bool> IsClearStageHard = new List<bool>();
    [JsonProperty] public int CurStage = 0;
    [JsonProperty] public bool IsFirstPlay = false;

    [JsonProperty] public bool IsHardMode = false;

    public override void Initialize()
    {
        for (int i = IsClearStage.Count; i < 9; i++)
        {
            IsClearStage.Add(false);
        }
        for (int i = IsClearStageHard.Count; i < 9; i++)
        {
            IsClearStageHard.Add(false);
        }
    }

    public override void LateInitialize()
    {
        
    }
    
    public void SetClearStage(int index)
    {
        IsClearStage[index] = true;
        
        PlayerDataManager.Instance.SaveLocalData();
    }

    public void SetClearStageHard(int index)
    {
        IsClearStageHard[index] = true;

        PlayerDataManager.Instance.SaveLocalData();
    }
}