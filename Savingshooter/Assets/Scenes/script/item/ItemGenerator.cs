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
    private float minTime = 0;  // これ以上じゃないと生成しない
    private float maxTime = 3;  // これ以上になると強制生成

    private void Start()
    {
        _pool = gameObject.GetComponent<ObjectPooling>();
        _pool.CreatePool(itemPrefab, 5, itemPrefab.GetInstanceID());
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
            GameObject item = _pool.GetPoolObj(itemPrefab.GetInstanceID(), new Vector3(10.0f, 0.0f, -6.0f));
            intervalTime = 0.0f;
        }
    }
    bool RandomWithEnergy()
    {
     
        float random = Random.Range(0, 100);
        playerStatas = player.GetComponent<PlayerStatas>();
        if(200.0f / playerStatas.GetPlayerEnergy() > random)
        {
            return true;
        }
        return false;
    }
}
