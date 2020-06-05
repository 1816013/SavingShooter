using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum EnemyAIState
    {
        Chase,
        Attack
    }
    [SerializeField]
    private GameObject _detonator = null;        // 爆発プレハブ
    [SerializeField]
    private GameObject _enemyAttack = null;      // 敵が攻撃する場所    
    private EnemyStatas _enemyStatas;     // エネミーのステータス
    private EnemyAIState _enemyAIState;      // エネミーのAI
    private EnemyShooting _enemyShooting;
    private Animator _animator;
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
        _animator = transform.GetComponentInChildren<Animator>();
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
            Vector3 gravity = Vector3.zero;
            gravity.y += Physics.gravity.y * Time.deltaTime;
            _charController.Move(gravity);
            SetTarget();          
            if (_enemyAIState == EnemyAIState.Chase)
            {
                Chase();
            }
            if (_enemyAIState == EnemyAIState.Attack)
            {
                if (!_target._player || (_target._pos - transform.position).magnitude > _attackDistanse )
                {
                    _enemyAIState = EnemyAIState.Chase;                 
                }
                if (_attackTime == 0)
                {
                    // ここは予備動作
                    if (_enemyStatas.GetEnemyType() == EnemyType.ClossRange)
                    {
                        _animator.SetBool("Attack", true);
                    }
                    if (_enemyStatas.GetEnemyType() == EnemyType.Destroy)
                    {
                        StartCoroutine(_coroutine); // 予備動作赤く点滅
                    }
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
                            ClossRangeAttack();
                            break;
                        default:
                            break;
                    }
                   
                }
            }
        }        
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
                _sphereCollider = _enemyAttack.GetComponent<SphereCollider>();
                _sphereCollider.enabled = false;
                _attackInterval = 1.0f;
                _attackDistanse = 2f;
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
        transform.LookAt(target._pos);
        _target._player = target._player;
        if (target._player)
        {
            _target = target;
        }
        else
        {
            if ((_target._pos - transform.position).magnitude < 0.1f)
            {

                _target = target;
            }
        }
    }

    // 追跡
    private void Chase()
    {
        _move = transform.forward;
       // _move.y += Physics.gravity.y * Time.deltaTime;
        _charController.Move(_move * _speed * Time.deltaTime);
        if ((_target._pos - transform.position).magnitude <= _attackDistanse && _target._player)
        {
            _enemyAIState = EnemyAIState.Attack;

            
        }
    }

    // 自爆
    private void Destroy()
    {
        _sphereCollider.enabled = true;
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

    // 近距離攻撃
    private void ClossRangeAttack()
    {
       
        AttackEnd(1);
        _sphereCollider.enabled = true;
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


    IEnumerator AttackEnd(int delayFrameCount)
    {
        for (var i = 0; i < delayFrameCount; i++)
        {
            yield return null;
        }
        _animator.SetBool("Attack", false);
        _sphereCollider.enabled = false;
        yield break;
    }

}
