using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    //敵プレハブ
    [SerializeField]
    private GameObject _destroyEnemy = null;
    [SerializeField]
    private GameObject _shootingEnemy = null;
    [SerializeField]
    private GameObject _clossRangeEnemy = null;

    private ObjectPooling _pool;

    [SerializeField]
    private float minTime = 5f; // 時間間隔の最小値 
    [SerializeField]
    private float maxTime = 7f; // 時間間隔の最大値
    //敵生成時間間隔
    private float _interval;
    //経過時間
    private float _time = 0f;    // 経過時間

    private Vector3 _minVec = new Vector3(-1, 0, -1); 
    private Vector3 _maxVec = new Vector3(1, 0, 1); 

    private float _minDistance = 25;    // 出現距離の最小値
    private float _maxDistance = 30;    // 出現距離の最大値

    private List<GameObject> _enemyPrefabList;


    // Start is called before the first frame update
    void Start()
    {
        _enemyPrefabList = new List<GameObject>();
        _enemyPrefabList.Add(_destroyEnemy);
        _enemyPrefabList.Add(_shootingEnemy);
        _enemyPrefabList.Add(_clossRangeEnemy); 
        //時間間隔を決定する
        _interval = GetRandomF(minTime, maxTime);
        _pool = gameObject.GetComponent<ObjectPooling>();
        // エネミーは作られた瞬間にレイキャストする関係上座標入力が必要
        for (int i = 0; i < (int)EnemyType.Max; i++)
        {   // 8 は敵の初期プール数
            _pool.CreatePool(_enemyPrefabList[i], 8, _enemyPrefabList[i].GetInstanceID(), GetRandomVec(_minVec, _maxVec).normalized * GetRandomF(_minDistance, _maxDistance)); 
        }
        // 一番最初の敵
        for (int i = 0; i < 5; i++)
        {
            int random = Random.Range((int)EnemyType.Destroy, (int)EnemyType.Max);
            _pool.GetPoolObj(_enemyPrefabList[random].GetInstanceID(), GetRandomVec(_minVec, _maxVec).normalized * GetRandomF(_minDistance, _maxDistance));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _time += Time.deltaTime;

        if (_time > _interval)
        {
            for (int i = 0; i < 5; i++)
            {
                int random = Random.Range((int)EnemyType.Destroy, (int)EnemyType.Max);
                _pool.GetPoolObj(_enemyPrefabList[random].GetInstanceID(), GetRandomVec(_minVec, _maxVec).normalized * GetRandomF(_minDistance, _maxDistance));
            }

            _time = 0f;
            _interval = GetRandomF(minTime,maxTime);
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
