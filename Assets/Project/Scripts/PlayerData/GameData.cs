using System;
using Newtonsoft.Json;

[Serializable]
public abstract class GameData
{
    [JsonIgnore] protected abstract eDataType DataType { get; }
    public abstract void Initialize();
    public abstract void LateInitialize();

    public long LastSessionDateTime;

    public void SaveData()
    {
        LastSessionDateTime = TimeManager.Instance.Now;
    }
}
