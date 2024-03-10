

using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject[] prefab;
    protected Dictionary<int, List<GameObject>> pool; // 인덱스 별로 오브젝트 리스트를 관리하는 딕셔너리

    private void Awake()
    {
        pool = new Dictionary<int, List<GameObject>>();

        for(int i = 0 ; i < prefab.Length; i++)
        {
            var item = prefab[i];
            var objectList = new List<GameObject>();

            for (int j = 0; j < 10; j++)
            {
                GameObject newObj = Instantiate(item,transform);
                newObj.SetActive(false);
                objectList.Add(newObj);
            }

            pool.Add(i, objectList);
        }
    }

    private GameObject CreateNewObject(int index)
    {
        GameObject obj = Instantiate(prefab[index], transform);
        obj.SetActive(false);
        return obj;
    }

    public GameObject Get(int index,Vector3 position)
    {
        if (!pool.ContainsKey(index) || pool[index].Count == 0)
        {
            Debug.LogWarning("No object available for key: " + index);
            return null;
        }

        foreach (var obj in pool[index])
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = position;
                obj.SetActive(true);
                return obj;
            }
        }
        
        GameObject newObj = CreateNewObject(index);
        pool[index].Add(newObj);
        newObj.transform.position = position;
        newObj.SetActive(true);
        
        return newObj;
    }
    
    public void ReturnObjectToPool(int key, GameObject obj)
    {
        if (!pool.ContainsKey(key))
        {
            Debug.LogWarning("Invalid pool key: " + key);
            return;
        }

        obj.SetActive(false);
    }
}

