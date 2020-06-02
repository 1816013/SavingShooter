using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    enum EnemyAIState
    {
        Chase,
        Attack
    }
    public GameObject _detonator;        // 爆発プレハブ
    public GameObject _enemyAttack;      // 敵が攻撃する場所    
    private EnemyStatas _enemyStatas;     // エネミーのステータス
    private EnemyAIState _enemyAIState;      // エネミーのAI
    private EnemyShooting _enemyShooting;
    private CharacterController _charController; // キャラのコントローラー
    private float _attackDistanse;    // 攻撃距離
    private Vector3 _move;                   // 移動ベクトル
    private float _speed;             // 移動速度
    private SphereCollider _sphereCollider;  // 爆発の効果範囲
    private float _attackTime;           // 攻撃間隔用経過時間
    private float _attackInterval;
    private IEnumerator _coroutine;
    private Pathfinding _pathfinding;
    private TagetStatas _target;
    private LineRenderer _lazerPointer;

    // Start is called before the first frame update
    private void Awake()
    {
        _pathfinding = gameObject.GetComponent<Pathfinding>();
        _enemyStatas = transform.GetComponentInParent<EnemyStatas>();      
    }

    private void OnEnable()
    {
        _target = new TagetStatas();
        SetEnemyAI();
        _enemyAIState = EnemyAIState.Chase;
    }
    private void OnDisable()
    {
        if (_enemyAIState == EnemyAIState.Attack)
        { 
            _enemyAIState = EnemyAIState.Chase;
        }
    }

    void Start()
    {     
        _charController = GetComponent<CharacterController>();     
        _coroutine = _enemyStatas.RedBlink();
    }
   

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_enemyStatas.IsDeath())
        {
            SetTarget();          
            if (_enemyAIState == EnemyAIState.Chase)
            {
                Chase();
            }
            if (_enemyAIState == EnemyAIState.Attack)
            {
                if (!_target.player)
                {
                    _enemyAIState = EnemyAIState.Chase;                 
                }
                _attackTime += Time.deltaTime;
                if (_attackTime > _attackInterval)
                {
                    _attackTime = 0;
                    switch (_enemyStatas.GetEnemyType())
                    {
                        case EnemyType.Destroy:
                            Destroy();
                            break;
                        case EnemyType.Shoot:
                            Shoot();
                            break;
                        case EnemyType.ClossRange:
                            break;
                        default:
                            break;
                    }
                   
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
        _attackTime = 0;
        yield break;
    }

    // Enemyのタイプからステータスをセットする
    private void SetEnemyAI()
    {
        _enemyStatas.GetEnemyType();
        switch (_enemyStatas.GetEnemyType())
        {
            case EnemyType.Destroy:
                _sphereCollider = _enemyAttack.GetComponent<SphereCollider>();
                _sphereCollider.enabled = false;
                _attackInterval = 2.0f;
                _attackDistanse = 3.0f;
                _speed = 3.0f;
                break;
            case EnemyType.Shoot:
                _lazerPointer = _enemyAttack.GetComponent<LineRenderer>();
                _lazerPointer.enabled = false;
                _enemyShooting = _enemyAttack.GetComponent<EnemyShooting>();
                _attackInterval = 2.0f;
                _attackDistanse = 10.0f;
                _speed = 2.0f;
                break;
            case EnemyType.ClossRange:
                _attackInterval = 1.0f;
                _attackDistanse = 2.0f;
                _speed = 4.0f;
                break;
            default:
                break;
        }
    }

    // ターゲット設定
    private void SetTarget()
    {
        TagetStatas target = _pathfinding.PathFind(transform.position, _charController.radius);
        transform.LookAt(target.pos);
        _target.player = target.player;
        if (target.player)
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
    }

    // 追跡
    private void Chase()
    {
        _move = transform.forward;
        _move.y += Physics.gravity.y * Time.deltaTime;
        _charController.Move(_move * _speed * Time.deltaTime);
        if ((_target.pos - transform.position).magnitude <= _attackDistanse && _target.player)
        {
            _enemyAIState = EnemyAIState.Attack;
            StartCoroutine(_coroutine); // 予備動作赤く点滅
        }
    }

    // 自爆
    private void Destroy()
    {
        StartCoroutine(DelayDestroy(1));
        StopCoroutine(_coroutine);
        GameObject exp = (GameObject)Instantiate(_detonator.gameObject, transform.position, Quaternion.identity);       
    }
    // 射撃
    private void Shoot()
    {
        _lazerPointer.enabled = true;
       // StopCoroutine(_coroutine);
        StartCoroutine(BlinkLayzerPointer(1));
        _enemyShooting.Shoot();
    }
    IEnumerator BlinkLayzerPointer(int delayFrameCount)
    {
        for (var i = 0; i < delayFrameCount; i++)
        {
            yield return null;
        }
        _lazerPointer.enabled = false;
        yield break;
    }

}
