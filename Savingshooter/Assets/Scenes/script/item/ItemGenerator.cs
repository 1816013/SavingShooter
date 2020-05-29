using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject itemPrefab;
    private ObjectPooling _pool;
    public GameObject player;
    private PlayerStatas playerStatas;
    private bool instanceF;   // 生成するか
    private float intervalTime;     // 経過時間
    private float time;             // 2秒ごとにランダムにする時用
    private float minTime = 10;  // これ以上じゃないと生成しない
    private float maxTime = 30;  // これ以上になると強制生成
    private float minDistance = 5;
    private float maxDistance = 10;

    private void Start()
    {
        _pool = gameObject.GetComponent<ObjectPooling>();
        playerStatas = player.GetComponent<PlayerStatas>();
        _pool.CreatePool(itemPrefab, 5, itemPrefab.GetInstanceID(), Vector3.zero);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        intervalTime += Time.deltaTime;
        time += Time.deltaTime;
        if (intervalTime >= 10 && time > 2.0f)
        {
            time = 0.0f;
            instanceF = RandomWithEnergy();
        }
        
        if (intervalTime >= maxTime || intervalTime >= minTime && instanceF)
        { 
            GameObject item = _pool.GetPoolObj(itemPrefab.GetInstanceID(), RandamVec3(new Vector3(-1f, -1f, -1f), new Vector3(1f, 1f, 1f)) * Random.Range(minDistance, maxDistance));
            intervalTime = 0.0f;
        }
    }
    bool RandomWithEnergy()
    {
     
        float random = Random.Range(0, 100);       
        if(200.0f / playerStatas.GetPlayerEnergy() > random)
        {
            return true;
        }
        return false;
    }
    private Vector3 RandamVec3(Vector3 min, Vector3 max )
    {
        Vector3 vec;
        vec.x = Random.Range(min.x, max.x);
        vec.y = Random.Range(min.y, max.y);
        vec.z = Random.Range(min.z, max.z);
        return vec;
    }
}
