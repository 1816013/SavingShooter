using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsController : MonoBehaviour
{
    public GameObject fpsCamera;
    //キャラクターコントローラー
    private CharacterController charController;
    //　キャラクターの速度
    private Vector3 move;
    [SerializeField]
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
            move = (Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward).normalized;
        }
        //　重力値を計算
        move.y += Physics.gravity.y * Time.deltaTime;
        //　キャラクターコントローラのMoveを使ってキャラクターを移動させる
        charController.Move(move * speed * Time.deltaTime);
    }
}
