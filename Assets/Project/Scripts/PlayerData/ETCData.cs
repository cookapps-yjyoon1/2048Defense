using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class ETCData : GameData
{
    protected override eDataType DataType => eDataType.ETC;

    [JsonProperty] public List<bool> IsClearStage = new List<bool>();
    [JsonProperty] public int CurStage = 0;
    [JsonProperty] public bool IsFirstPlay = false;
    
    public override void Initialize()
    {
        for (int i = IsClearStage.Count; i < 9; i++)
        {
            IsClearStage.Add(false);
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
}