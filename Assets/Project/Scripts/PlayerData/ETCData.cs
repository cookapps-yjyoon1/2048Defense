using System.Collections.Generic;

public class ETCData : GameData
{
    protected override eDataType DataType => eDataType.ETC;

    public List<bool> IsClearStage = new List<bool>();
    public int CurStage = 0;
    public bool IsFirstPlay = false;
    
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
    }
}