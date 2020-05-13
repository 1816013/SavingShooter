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
    private GameObject target;          // プレイヤー
    private EnemyState enemyState;      // エネミーのステータス
    private CharacterController charController; // キャラのコントローラー
    private float attackDistanse = 5.0f;    // 攻撃距離
    private Vector3 move;                   // 移動ベクトル
    private float speed = 3.0f;             // 移動速度
    private SphereCollider sphereCollider;  // 爆発の効果範囲
    private float attackIntarval;           // 攻撃間隔
    private Color masterColor;              // マテリアルの元の色
    // Start is called before the first frame update
    void Start()
    {
        masterColor = enemyMat.GetComponent<Renderer>().material.color;
        charController = GetComponent<CharacterController>();
        sphereCollider = enemyAttack.GetComponent<SphereCollider>();
        sphereCollider.enabled = false;
        enemyState = EnemyState.Chase;
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(target.transform.position);
        if(enemyState == EnemyState.Chase)
        {
            move = (target.transform.position - transform.position).normalized ;
            charController.Move(move * speed * Time.deltaTime);
            if ((target.transform.position - transform.position).magnitude <= attackDistanse)
            {
                enemyState = EnemyState.Destoroy;
            }
        }
        if(enemyState == EnemyState.Destoroy)
        {
            attackIntarval += Time.deltaTime;
            StartCoroutine("RedBlink");
            if (attackIntarval > 2.0f)
            {
                sphereCollider.enabled = true;
                GameObject exp = (GameObject)Instantiate(detonator.gameObject, transform.position, Quaternion.identity);
                Destroy(gameObject,0.1f);
            }
        }
        
    }
    IEnumerator RedBlink()
    {
        enemyMat.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        enemyMat.GetComponent<Renderer>().material.color = masterColor;
        yield return new WaitForSeconds(0.2f);
    }
}
