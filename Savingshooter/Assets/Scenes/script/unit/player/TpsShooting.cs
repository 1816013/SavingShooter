using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab = null;
    [SerializeField]
    private GameObject _gameController = null;
    [SerializeField]
    private int _maxAmmo = 50;
    private Animator _animator; 
    private ObjectPooling _pool;
    private GameObject _player;
    private PlayerStatas _playerStatas;
    private Shoot _shoot;   
    private int _nowAmmo;
    private float _shotTime = 0.0f;
    private bool _shotF = false;
    private bool _reloading = false;
    private float _reloadTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _player = transform.root.gameObject;
        _playerStatas = _player.GetComponent<PlayerStatas>();
        _animator = _player.GetComponentInChildren<Animator>();
        _nowAmmo = _maxAmmo;
        _shoot = _gameController.GetComponent<Shoot>();
        _pool = _gameController.GetComponent<ObjectPooling>();
        _pool.CreatePool(_bulletPrefab, _maxAmmo, _bulletPrefab.GetInstanceID(), Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_reloading)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                _animator.SetBool("shotF", true);
                _shotTime += 1;
                if (_shotTime % 5 == 0 && _nowAmmo > 0)
                {
                    _shotF = true;
                }
            }
            else
            {
                _animator.SetBool("shotF", false);
                if (Input.GetKeyDown(KeyCode.R))   // リロード
                {
                    Reload();
                }
            }
            if(_nowAmmo == 0)
            {
                Reload();
            }
        }
        else
        {
            _reloadTime += Time.deltaTime;
        }
        if(_reloadTime > 1)
        {
            _reloading = false;
            _animator.SetBool("Reload", false);
        }
    }
    private void FixedUpdate()
    {
        if (_shotF)
        {            
            _nowAmmo -= 1;
            _shoot.Shooting(ref _pool, _bulletPrefab, transform, 3000);
            //Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0))
            _shotF = false;
            
        }
    }

    private void Reload()
    {
        if (_nowAmmo != _maxAmmo)
        {          
            _reloadTime = 0;
            _animator.SetBool("shotF", false);
            _animator.SetBool("Reload", true);
            _playerStatas.AddPlayerEnergy(-5);
            _reloading = true;
            _nowAmmo = _maxAmmo;
        }
    }


    public int GetNowAmmo()
    {
        return _nowAmmo;
    }
}
