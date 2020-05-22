using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    enum EnemyState
    {
        Chase,
        Destoroy
    }
    public GameObject detonator;        // 爆発プレハブ
    public GameObject enemyMat;         // 敵のマテリアル
    public GameObject enemyAttack;      // 攻撃
    private EnemyStatas enemyStatas;     // エネミーのステータス
    private GameObject target;          // プレイヤー
    private EnemyState enemyState;      // エネミーのAI
    private CharacterController charController; // キャラのコントローラー
    private float attackDistanse = 5.0f;    // 攻撃距離
    private Vector3 move;                   // 移動ベクトル
    private float speed = 3.0f;             // 移動速度
    private SphereCollider sphereCollider;  // 爆発の効果範囲
    private float attackIntarval;           // 攻撃間隔
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        enemyStatas = transform.GetComponentInParent<EnemyStatas>();
        charController = GetComponent<CharacterController>();
        sphereCollider = enemyAttack.GetComponent<SphereCollider>();
        sphereCollider.enabled = false;
        enemyState = EnemyState.Chase;
        target = GameObject.FindWithTag("Player");
        coroutine = enemyStatas.RedBlink();
    }
    private void OnDisable()
    {
        if (enemyState == EnemyState.Destoroy)
        {
            sphereCollider.enabled = false;
            enemyState = EnemyState.Chase;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!enemyStatas.IsDeath())
        {
            transform.LookAt(target.transform.position);
            if (enemyState == EnemyState.Chase)
            {
                move = (target.transform.position - transform.position).normalized;
                move.y += Physics.gravity.y * Time.deltaTime;
                charController.Move(move * speed * Time.deltaTime);
                if ((target.transform.position - transform.position).magnitude <= attackDistanse)
                {
                    enemyState = EnemyState.Destoroy;
                    StartCoroutine(coroutine);
                }
            }
            if (enemyState == EnemyState.Destoroy)
            {
                attackIntarval += Time.deltaTime;
                if (attackIntarval > 2.0f)
                {
                    StartCoroutine(DelayDestroy(1));
                    StopCoroutine(coroutine);
                    sphereCollider.enabled = true;
                    GameObject exp = (GameObject)Instantiate(detonator.gameObject, transform.position, Quaternion.identity);
                }
            }
        }
        
    }
    IEnumerator DelayDestroy(int delayFrameCount)
    {
        for (var i = 0; i < delayFrameCount; i++)
        {
            yield return null;
        }
        gameObject.SetActive(false);
        attackIntarval = 0;
        yield break;
    }
   
}
