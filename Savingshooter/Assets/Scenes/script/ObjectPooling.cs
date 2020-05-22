using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private Dictionary<int, List<GameObject>> _poolList;
    private GameObject _poolObj;

    private void Awake()
    {
        _poolList = new Dictionary<int, List<GameObject>>();
    }
    public void CreatePool(GameObject obj, int maxCount, int key)
    {
        _poolObj = obj;
        if (_poolList.ContainsKey(key) == false)
        {
            _poolList.Add(key, new List<GameObject>());
        }
        for(int i = 0; i < maxCount; i++)
        {
            GameObject newObj = CreateNewObject();
            newObj.SetActive(false);
            _poolList[key].Add(newObj);
        }
    }
    public GameObject GetPoolObj(int key)
    {
        foreach(GameObject obj in _poolList[key])
        {
            if(obj.activeSelf == false)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        GameObject newObj = CreateNewObject();
        newObj.SetActive(true);
        _poolList[key].Add(newObj);
        return newObj;
    }

    private GameObject CreateNewObject()
    {
        GameObject newObj = Instantiate(_poolObj);
       //newObj.name = _poolObj.name + (_poolList.Count + 1);

        return newObj;
    }
}
