using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCameraControl : MonoBehaviour
{
    public GameObject _player;
    public Vector2 _minMaxAngle = new Vector2(-65, 65);
    [SerializeField]
    private float _xRotation;   // プレイヤーが向いている向き
    [SerializeField]
    private float _yRotation;   
    void FixedUpdate()
    {
        _xRotation += Input.GetAxis("Mouse X") * 5; //マウスの移動.
        _yRotation -= Input.GetAxis("Mouse Y") * 5; //マウスの移動.
        _yRotation = Mathf.Clamp(_yRotation, _minMaxAngle.x, _minMaxAngle.y);//上下の角度移動の最大、最小
        _player.transform.eulerAngles = new Vector3(0, _xRotation, 0);
        transform.rotation = Quaternion.Euler(_yRotation, _xRotation, 0);
    }
}
