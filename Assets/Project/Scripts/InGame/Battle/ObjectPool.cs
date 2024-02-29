using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject[] prefab;
    public List<GameObject>[] pool;

    private void Awake()
    {
        pool = new List<GameObject>[prefab.Length];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index, Vector3 pivotPos)
    {
        GameObject select = null;

        foreach (GameObject item in pool[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.transform.position = pivotPos;
                select.SetActive(true);
                break;
            }
        }

        if (select == null)
        {
            select = Instantiate(prefab[index], transform);
            select.transform.position = pivotPos;
            pool[index].Add(select);
        }

        return select;
    }
}
