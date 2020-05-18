using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCameraControl : MonoBehaviour
{
    public GameObject player;
    public Vector2 MinMaxAngle = new Vector2(-65, 65);
    [SerializeField]
    private float X_Rotation;   // プレイヤーが向いている向き
    [SerializeField]
    private float Y_Rotation;   
    void FixedUpdate()
    {
        X_Rotation += Input.GetAxis("Mouse X") * 5; //マウスの移動.
        Y_Rotation -= Input.GetAxis("Mouse Y") * 5; //マウスの移動.
        Y_Rotation = Mathf.Clamp(Y_Rotation, MinMaxAngle.x, MinMaxAngle.y);//上下の角度移動の最大、最小
        player.transform.eulerAngles = new Vector3(0, X_Rotation, 0);
        transform.rotation = Quaternion.Euler(Y_Rotation, X_Rotation, 0);
    }
}
