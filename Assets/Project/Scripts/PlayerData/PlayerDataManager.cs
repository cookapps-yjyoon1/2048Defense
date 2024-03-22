using System.Collections;
using CookApps.LocalData;
using UnityEngine;

public enum eDataType
{
    None,
    ETC,
    Arrow,
    InGame,
    Box,
    All,
}

public class PlayerDataManager : SingletonBehaviour<PlayerDataManager>
{
    private PlayerDataContainer _playerData;

    public static PlayerDataContainer PlayerDataContainer => Instance._playerData;
    public static InGameData InGameData => PlayerDataContainer.InGameData;
    public static ETCData ETCData => PlayerDataContainer.ETCData;
    public static ArrowData ArrowData => PlayerDataContainer.ArrowData;
    public static BoxData BoxData => PlayerDataContainer.BoxData;

    private CookAppsLocalData _localData;

    private static readonly string FILE_NAME = "Defense2048_PlayerData";

    private void Awake()
    {
        localDataSet();
    }
    
    private void localDataSet()
    {
        _localData ??= new CookAppsLocalData(GetKey());
        if (_localData.TryLoad(FILE_NAME, out _playerData) == false)
        {
            _playerData = new PlayerDataContainer();
        }
    }

    public static string GetKey()
    {
        //https://cookapps.atlassian.net/wiki/spaces/TST/pages/25222972384/string 를 참고하세요.
        // key = "cookapps1357@$^*"
        var key =
            "\u4ACA\u4AB0\u4AB0\u4AAE\u4AC7\uCAAF\uCAAF\u4AB2\u4ACF\u4AD2\u4AD9\u4AD4\uCAD7\uCAF1\uCAC8\uCACE";

        for (int oyXIM = 0, alRfs = 0; oyXIM < 16; oyXIM++)
        {
            alRfs = key[oyXIM];
            alRfs -= 0x6C1E;
            alRfs ^= 0x56DC;
            alRfs++;
            alRfs ^= 0x22D2;
            alRfs += 0x5B27;
            alRfs = ((alRfs << 13) | ((alRfs & 0xFFFF) >> 3)) & 0xFFFF;
            alRfs++;
            alRfs = ((alRfs << 4) | ((alRfs & 0xFFFF) >> 12)) & 0xFFFF;
            alRfs = ~alRfs;
            alRfs -= 0xF3F8;
            key = key.Substring(0, oyXIM) + (char)(alRfs & 0xFFFF) + key.Substring(oyXIM + 1);
        }

        return key;
    }

    public IEnumerator LoadLocalData()
    {
        if (!_localData.TryLoad<PlayerDataContainer>(FILE_NAME, out PlayerDataContainer sampleData))
        {
            Debug.Log("Load 실패");
            sampleData = new PlayerDataContainer();
        }

        yield return new WaitUntil(() => sampleData != null);

        _playerData = sampleData;
    }

    public IEnumerator SaveLocalData()
    {
        _playerData.SessionSave();
        CookAppsLocalData.EnumSaveResult enumSaveResult = _localData.Save(_playerData, FILE_NAME);
        
        switch (enumSaveResult)
        {
            case CookAppsLocalData.EnumSaveResult.SUCCESS:
                Debug.Log("저장 완료!");
                break;

            case CookAppsLocalData.EnumSaveResult.FAIL_UNKNOWN:
            case CookAppsLocalData.EnumSaveResult.FAIL_DISK_FULL:
                Debug.LogError("저장에 실패");
                break;
        }

        yield return null;
    }
    
    public IEnumerator ValidCheck()
    {
        _playerData.ValidCheck();

        yield return new WaitForSeconds(2f);
    }
    
}