﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public GameObject fpsCamera;
    //キャラクターコントローラー
    private CharacterController charController;
    //　キャラクターの速度
    private Vector3 move;
    private float speed = 5.0f;

    void Start()
    {
        //キャラクターコントローラの取得
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        //　キャラクターコントローラのコライダが地面と接触してるかどうか
        if (charController.isGrounded)
        {
            move = (Input.GetAxis("Horizontal") * fpsCamera.transform.right + Input.GetAxis("Vertical") * fpsCamera.transform.forward).normalized;
        }
        //　重力値を計算
        move.y += Physics.gravity.y * Time.deltaTime;
        //　キャラクターコントローラのMoveを使ってキャラクターを移動させる
        charController.Move(move * speed * Time.deltaTime);
    }
}