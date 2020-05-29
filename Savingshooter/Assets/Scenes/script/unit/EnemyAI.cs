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
    public GameObject enemyAttack;      // 攻撃    
    private EnemyStatas enemyStatas;     // エネミーのステータス
   // private GameObject target;          // プレイヤー
    private EnemyState enemyState;      // エネミーのAI
    private CharacterController charController; // キャラのコントローラー
    private float attackDistanse = 5.0f;    // 攻撃距離
    private Vector3 move;                   // 移動ベクトル
    private float speed = 3.0f;             // 移動速度
    private SphereCollider sphereCollider;  // 爆発の効果範囲
    private float attackIntarval;           // 攻撃間隔
    private IEnumerator coroutine;
    private Pathfinding pathfinding;
    private TagetStatas _target;
    // Start is called before the first frame update


    private void Awake()
    {
        pathfinding = gameObject.GetComponent<Pathfinding>();
    }


    void Start()
    {
        enemyStatas = transform.GetComponentInParent<EnemyStatas>();
        charController = GetComponent<CharacterController>();
        sphereCollider = enemyAttack.GetComponent<SphereCollider>();
        sphereCollider.enabled = false;

        coroutine = enemyStatas.RedBlink();
    }
    private void OnEnable()
    {
        _target = new TagetStatas();
       // _target = pathfinding.PathFind(transform.position, 0.5f);
        enemyState = EnemyState.Chase;       
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
            TagetStatas target = pathfinding.PathFind(transform.position, charController.radius);
            transform.LookAt(target.pos);
            if(target.player)
            {
                _target = target;
            }
            else
            {
                if ((_target.pos - transform.position).magnitude < 0.1f)
                {

                    _target = target;
                }
            }
           
          
            if (enemyState == EnemyState.Chase)
            {
                move = transform.forward;
                move.y += Physics.gravity.y * Time.deltaTime;
                charController.Move(move * speed * Time.deltaTime);
                if ((_target.pos - transform.position).magnitude <= attackDistanse && _target.player)
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
                  //  sphereCollider.enabled = true;
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
