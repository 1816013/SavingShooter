using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsController : MonoBehaviour
{
    
    public Camera tpsCamera;
    private PlayerStatas _playerStatas;
    //キャラクターコントローラー
    private CharacterController charController;
    //　キャラクターの速度
    private Vector3 move;
    [SerializeField]
    private float speed = 5.0f;
    Plane plane = new Plane();
    float distance = 0.0f;
    Vector3 planeOffset;
    private Animator animator;

    void Start()
    {
        _playerStatas = GetComponent<PlayerStatas>();
        //キャラクターコントローラの取得
        charController = GetComponent<CharacterController>();
        animator =  transform.GetComponentInChildren<Animator>();
        planeOffset = new Vector3(0, 1.5f, 0);
    }

    private void Update()
    {
        //　キャラクターコントローラのコライダが地面と接触してるかどうか
        if (charController.isGrounded)
        {
            move.x = Input.GetAxis("Horizontal");
            move.z = Input.GetAxis("Vertical");
            Vector3 dir = (-transform.right * move.x + transform.forward * move.z).normalized;
            animator.SetFloat("X", dir.x);
            animator.SetFloat("Y", dir.z);
        }
    }

    void FixedUpdate()
    {
        if (!_playerStatas.IsDeath())
        {
            // カメラからマウスの場所までののレイ(0.25はプレイヤーの銃が中心からずれているから調整のため)
            var ray = tpsCamera.ScreenPointToRay(Input.mousePosition + new Vector3(0.25f, 0));

            // プレイヤーの高さにPlaneを更新して、カメラの情報を元に地面判定して距離を取得
            plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
            if (plane.Raycast(ray, out distance))
            {
                // 距離を元に交点を算出して、交点の方を向く
                var lookPoint = ray.GetPoint(distance);
                Debug.DrawRay(ray.origin, ray.direction * distance, Color.blue);
                transform.LookAt(lookPoint);
            }
            //　重力値を計算
            move.y += Physics.gravity.y * Time.deltaTime;
            //　キャラクターコントローラのMoveを使ってキャラクターを移動させる
            charController.Move(move * speed * Time.deltaTime);
        }
    }
}
