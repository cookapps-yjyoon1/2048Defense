using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[Serializable]
public class PlayerDataContainer
{
    public ArrowData ArrowData = new ArrowData();
    public InGameData InGameData = new InGameData();
    public ETCData ETCData = new ETCData();
    public BoxData BoxData = new BoxData();
    
    public void Assign(eDataType type, [NotNull] object data)
    {
        switch (type)
        {
            case eDataType.InGame: InGameData = data as InGameData; break;
            case eDataType.ETC: ETCData = data as ETCData; break;
            case eDataType.Arrow: ArrowData = data as ArrowData; break;
            case eDataType.Box: BoxData = data as BoxData; break;

            case eDataType.None:
            case eDataType.All:
            default:
            {
                Debug.LogError($"Assign Failed : {type}");
                break;
            }
        }   
    }   
    
    public bool TryGetPlayerData(eDataType type, out GameData data)
    {
        data = null;
        switch (type)
        {
            case eDataType.InGame: data = InGameData; break;
            case eDataType.ETC:  data = ETCData; break;
            case eDataType.Arrow:  data = ArrowData;  break;
            case eDataType.Box:  data = BoxData;  break;
            
            case eDataType.None: return false;
            case eDataType.All: return false;
            default:
            {
                Debug.LogError($"Get PlayerData Failed : {type}");
                return false;
            }
        }   
        return true;
    }

    public void ValidCheck()
    {
        PlayerDataAction(x => x.Initialize());
        PlayerDataAction(x => x.LateInitialize());
    }

    public void SessionSave()
    {
        PlayerDataAction(x=>x.SaveData());
    }

    private void PlayerDataAction(Action<GameData> action)
    {
        if (action == null)
        {
            return;
        }
        
        foreach (eDataType dataType in Enum.GetValues(typeof(eDataType)))
        {
            if (TryGetPlayerData(dataType, out var data))
            {
                action(data);
            }
        }
    }
}
