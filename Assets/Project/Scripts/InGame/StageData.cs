using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stage
{
    public float correction;
    public List<GameObject> mob;
    public List<GameObject> mob_boss;
}

public class StageData : MonoBehaviour
{
    public List<Stage> stage;

    public float GetStageCorrection(int _stage)
    {
        return stage[_stage].correction;
    }

    public GameObject GetMobType(int _stage, int _mobType)
    {
        return stage[_stage].mob[_mobType];
    }

    public GameObject GetBossMobType(int _stage, int _bossMobType)
    {
        return stage[_stage].mob_boss[_bossMobType];
    }
}
