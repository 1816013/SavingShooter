using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsCameraControl : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private Vector3 mousePos; //真ん中からのマウスの座標
    private Vector3 screenSize; // ｽｸﾘｰﾝの大きさ
    private Vector3 move;    // 移動の大きさ
    private Vector3 moveCoefficient;    // 移動係数

    private void Start()
    {
        offset = transform.position;
        screenSize = new Vector3(Screen.width, Screen.height, 0.0f);
        move = new Vector3(4.0f, 0.0f, 2.0f);
    }

    void Update()
    {
        mousePos = Input.mousePosition - screenSize / 2;
    }
    void FixedUpdate()
    {
        moveCoefficient.x = mousePos.x / (screenSize.x / 2);
        moveCoefficient.z = mousePos.y / (screenSize.y / 2);
        moveCoefficient.x = Mathf.Clamp(moveCoefficient.x, -1.0f, 1.0f);
        moveCoefficient.z = Mathf.Clamp(moveCoefficient.z, -1.0f, 1.0f);
        Vector3 movement = move;
        movement.x = move.x * moveCoefficient.x;
        movement.z = move.z * moveCoefficient.z;

        // 移動
        transform.position = offset + movement + player.transform.position;
    }
}