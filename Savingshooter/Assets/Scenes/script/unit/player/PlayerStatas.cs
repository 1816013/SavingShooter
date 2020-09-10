using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatas : MonoBehaviour
{
    [SerializeField]
    private GameController _gameController = null;
    private Animator _animator;
    private float _playerEnergy = 200.0f; // エネルギー
    private float _playerPower = 0.5f;  // 出力
    [SerializeField]
    private float _speedMax = 10f; // 最高速度
    private float _playerSpeed; // 現在速度
    private float _powerCost;            // エネルギー消費の係数
    private bool _death;

    private void Start()
    {
        _animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            _playerPower += 0.01f;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _playerPower -= 0.01f;
        }
        _playerPower = Mathf.Clamp(_playerPower, 0.1f, 1.0f); // 最大値1最小値0.1

        if (_playerPower > 0.5)
        {
            _powerCost = (_playerPower - 0.5f) * 4 + 1.0f;    // 最大値1の時　3
        }
        else
        {
            _powerCost = ((_playerPower - 0.1f) * 1.25f) + 0.5f ;     // 最低値  0.1の時 0.5 : 0.5の時  1
        }
        _playerEnergy -= Time.deltaTime * _powerCost * 2;
        _playerSpeed = _speedMax * _playerPower;
        _playerSpeed = Mathf.Clamp(_playerSpeed, 3f, 10f);


        if (_playerEnergy <= 0)
        {
            _death = true;
            _animator.SetBool("Die", true);
            Invoke("DieCall", 2.0f);
        }
    }
    public float GetPlayerPower()
    {
        return _playerPower;
    }
    public float GetPlayerSpeed()
    {
        return _playerSpeed;
    }
    public float GetPlayerEnergy()
    {
        return _playerEnergy;
    }
    public void AddPlayerEnergy(float add)
    {
        _playerEnergy += add;
    }
    private void DieCall()
    {
        _gameController.GetComponent<GameController>().ChangeScene();
    }
    public bool IsDeath()
    {
        return _death;
    }
}
