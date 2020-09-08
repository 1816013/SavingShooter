using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsController : MonoBehaviour
{
    [SerializeField]
    private Camera _tpsCamera = null;
    private PlayerStatas _playerStatas;
    //キャラクターコントローラー
    private CharacterController _charController;
    //　キャラクターの速度
    private Vector3 _move;
    Plane _plane = new Plane();
    private float _distance = 0.0f;
    Vector3 _planeOffset;
    private Animator _animator;

    void Start()
    {
        _playerStatas = GetComponent<PlayerStatas>();
        //キャラクターコントローラの取得
        _charController = GetComponent<CharacterController>();
        _animator =  transform.GetComponentInChildren<Animator>();
        _planeOffset = new Vector3(0, 1.5f, 0);
    }

    private void Update()
    {
        //　キャラクターコントローラのコライダが地面と接触してるかどうか
        if (_charController.isGrounded)
        {
            _move.x = Input.GetAxis("Horizontal");
            _move.z = Input.GetAxis("Vertical");
            Vector3 dir = (-transform.right * _move.x + transform.forward * _move.z).normalized;
            _animator.SetFloat("X", dir.x);
            _animator.SetFloat("Y", dir.z);
        }
    }

    void FixedUpdate()
    {
        if (!_playerStatas.IsDeath())
        {
            // カメラからマウスの場所までののレイ(0.25はプレイヤーの銃が中心からずれているから調整のため)
            var ray = _tpsCamera.ScreenPointToRay(Input.mousePosition + new Vector3(0.25f, 0));

            // プレイヤーの高さにPlaneを更新して、カメラの情報を元に地面判定して距離を取得
            _plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
            if (_plane.Raycast(ray, out _distance))
            {
                // 距離を元に交点を算出して、交点の方を向く
                var lookPoint = ray.GetPoint(_distance);
                Debug.DrawRay(ray.origin, ray.direction * _distance, Color.blue);
                transform.LookAt(lookPoint);
            }
            //　重力値を計算
            _move.y += Physics.gravity.y * Time.deltaTime;
            //　キャラクターコントローラのMoveを使ってキャラクターを移動させる
            _charController.Move(_move * _playerStatas.GetPlayerSpeed() * Time.deltaTime);
        }
    }
}
