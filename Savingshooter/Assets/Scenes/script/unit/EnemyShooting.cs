using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject _enemyBulletPrefab;
    private GameObject _gameController;
    private ObjectPooling _pool;
    private Shoot _shoot;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController");
        _pool = _gameController.GetComponent<ObjectPooling>();
        _pool.CreatePool(_enemyBulletPrefab, 10, _enemyBulletPrefab.GetInstanceID(), Vector3.zero);
        _shoot = _gameController.GetComponent<Shoot>();

    }

    public void Shoot()
    {
        _shoot.Shooting(ref _pool, _enemyBulletPrefab, transform, 1000);
    }
}
