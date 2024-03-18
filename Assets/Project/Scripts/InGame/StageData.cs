using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stage
{
    public float correction;
    public List<GameObject> mob;
    public List<GameObject> mob_elite;
    public List<GameObject> mob_boss;
}

public class StageData : MonoBehaviour
{
    public List<Stage> stage;
}
