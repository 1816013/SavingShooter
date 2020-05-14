using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    //敵プレハブ
    public GameObject enemyPrefab;
    
    public float minTime = 2f; //時間間隔の最小値 
    public float maxTime = 5f; //時間間隔の最大値
    //敵生成時間間隔
    private float interval;
    //経過時間
    private float time = 0f;    // 経過時間

    private Vector3 min = new Vector3(13, 5, -30); //時間間隔の最小値  
    private Vector3 max = new Vector3(30, 2, -10); //時間間隔の最大値

    // Start is called before the first frame update
    void Start()
    {
        //時間間隔を決定する
        interval = GetRandomF(minTime, maxTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time > interval)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.position = GetRandomVec(min, max);
            time = 0f;
            interval = GetRandomF(minTime,maxTime);
        }
    }
    private float GetRandomF(float min, float max)
    {
        return Random.Range(min, max);
    }
    private Vector3 GetRandomVec(Vector3 min, Vector3 max)
    {
        Vector3 vec;
        vec.x = Random.Range(min.x, max.x);
        vec.y = Random.Range(min.y, max.y);
        vec.z = Random.Range(min.z, max.z);
        return vec;
    }
}
