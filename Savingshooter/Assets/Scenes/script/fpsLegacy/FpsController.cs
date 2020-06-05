using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsController : MonoBehaviour
{
    [SerializeField]
    private GameObject _fpsCamera;
    //キャラクターコントローラー
    private CharacterController _charController;
    //　キャラクターの速度
    private Vector3 _move;
    [SerializeField]
    private float _speed = 5.0f;

    void Start()
    {
        //キャラクターコントローラの取得
        _charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        //　キャラクターコントローラのコライダが地面と接触してるかどうか
        if (_charController.isGrounded)
        {
            _move = (Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward).normalized;
        }
        //　重力値を計算
        _move.y += Physics.gravity.y * Time.deltaTime;
        //　キャラクターコントローラのMoveを使ってキャラクターを移動させる
        _charController.Move(_move * _speed * Time.deltaTime);
    }
}
