using System.Collections.Generic;
using UnityEngine;

public class UnitList : MonoBehaviour
{
    public static List<EnemyController> enumyList = new List<EnemyController>();
    
    public void OnDestroy()
    {
        enumyList.Clear();
    }
}
