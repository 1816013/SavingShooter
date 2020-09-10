using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _itemPrefab = null;
    [SerializeField]
    private GameObject _player = null;
    private ObjectPooling _pool;    
    private PlayerStatas _playerStatas;
    private bool _instanceF;   // 生成するか
    private float _intervalTime;     // 経過時間
    private float _time;             // 2秒ごとにランダムにする時用
    private float _minTime = 3;  // これ以上じゃないと生成しない
    private float _maxTime = 15;  // これ以上になると強制生成
    private float _minDistance = 5;
    private float _maxDistance = 10;

    private void Start()
    {
        _pool = gameObject.GetComponent<ObjectPooling>();
        _playerStatas = _player.GetComponent<PlayerStatas>();
        _pool.CreatePool(_itemPrefab, 5, _itemPrefab.GetInstanceID(), Vector3.zero);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _intervalTime += Time.deltaTime;
        _time += Time.deltaTime;
        if (_time >= 2)
        {
            _time = 0.0f;
            _instanceF = RandomWithEnergy();
        }
        
        if (_intervalTime >= _maxTime || (_intervalTime >= _minTime && _instanceF))
        { 
            GameObject item = _pool.GetPoolObj(_itemPrefab.GetInstanceID(), RandamVec3(new Vector3(-1f, 0, -1f), new Vector3(1f, 1f, 1f)) * Random.Range(_minDistance, _maxDistance));
            _intervalTime = 0.0f;
        }
    }
    bool RandomWithEnergy()
    {
     
        float random = Random.Range(0, 50);       
        if(200.0f / _playerStatas.GetPlayerEnergy() > random)
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
