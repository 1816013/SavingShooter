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
    public void CreatePool(GameObject obj, int maxCount, int key, Vector3 pos)
    {
        _poolObj = obj;
        if (_poolList.ContainsKey(key) == false)
        {
            _poolList.Add(key, new List<GameObject>());
        }
        for(int i = 0; i < maxCount; i++)
        {
            GameObject newObj = CreateNewObject(pos);
            newObj.SetActive(false);
            _poolList[key].Add(newObj);
        }
    }
    public GameObject GetPoolObj(int key, Vector3 pos)
    {
        foreach(GameObject obj in _poolList[key])
        {
            if(obj.activeSelf == false)
            {
                obj.transform.position = pos;
                obj.SetActive(true);
                return obj;
            }
        }
        GameObject newObj = CreateNewObject(pos);   
        newObj.SetActive(true);
        _poolList[key].Add(newObj);
        return newObj;
    }

    private GameObject CreateNewObject(Vector3 pos)
    {
        GameObject newObj = Instantiate(_poolObj, pos, Quaternion.identity);
       //newObj.name = _poolObj.name + (_poolList.Count + 1);

        return newObj;
    }
}
