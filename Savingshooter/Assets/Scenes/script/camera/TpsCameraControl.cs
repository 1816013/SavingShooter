using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsCameraControl : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    private Vector3 _offset;
    private Vector3 _mousePos; //真ん中からのマウスの座標
    private Vector3 _screenSize; // ｽｸﾘｰﾝの大きさ
    private Vector3 _move;    // 移動の大きさ
    private Vector3 _moveCoefficient;    // 移動係数

    private void Start()
    {
        _offset = transform.position;
        _screenSize = new Vector3(Screen.width, Screen.height, 0.0f);
        _move = new Vector3(4.0f, 0.0f, 2.0f);
    }

    void Update()
    {
        _mousePos = Input.mousePosition - _screenSize / 2;
    }
    void FixedUpdate()
    {
        _moveCoefficient.x = _mousePos.x / (_screenSize.x / 2);
        _moveCoefficient.z = _mousePos.y / (_screenSize.y / 2);
        _moveCoefficient.x = Mathf.Clamp(_moveCoefficient.x, -1.0f, 1.0f);
        _moveCoefficient.z = Mathf.Clamp(_moveCoefficient.z, -1.0f, 1.0f);
        Vector3 movement = _move;
        movement.x = _move.x * _moveCoefficient.x;
        movement.z = _move.z * _moveCoefficient.z;

        // 移動
        transform.position = _offset + movement + _player.transform.position;
    }
}