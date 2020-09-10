using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Destroy,    // 自爆
    Shoot,      // 射撃
    ClossRange,  // 近接攻撃
    Max
}

public class EnemyStatas : MonoBehaviour
{
    [SerializeField]
    private GameObject _detonator = null;        // 爆発プレハブ
    [SerializeField]
    private float _hitPoint = 100.0f;
    [SerializeField]
    private int _score;     // 敵の強さで変わる
    private GameObject _gameController;
    private Animator _animator;
    private bool _death;
    private Renderer _renderer;
    private Color _masterColor;
    [SerializeField]
    private EnemyType _enemyType;

    private void Awake()
    {
        _animator = transform.GetComponentInChildren<Animator>();
        _renderer = gameObject.GetComponentInChildren<Renderer>();
        _masterColor = _renderer.material.color;

    }

    private void Start()
    {
        _death = false;
        _gameController = GameObject.FindGameObjectWithTag("GameController");
       
    }
    private void OnEnable()
    {
        var time = (int)(Time.time / 5);
        switch (_enemyType)
        {
            case EnemyType.Destroy:
                _hitPoint = 20.0f + 10 * time;
                _score = 50;
                break;
            case EnemyType.Shoot:
                _hitPoint = 60.0f + 10 * time;
                _score = 100;
                break;
            case EnemyType.ClossRange:
                _hitPoint = 40.0f + 10 * time;
                _score = 50;
                break;
            default:
                break;
        }
        
        _death = false;
    }
    private void OnDisable()
    {
        _renderer.material.color = _masterColor;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(_hitPoint <= 0 && !_death)
        {
            _death = true;
           
        }
        if(_death)
        {
            if (_animator != null)
            {
                _animator.SetBool("Destroy", true);
            }
            _gameController.GetComponent<GameController>().AddScore(_score);
            GameObject exp = (GameObject)Instantiate(_detonator.gameObject, transform.position, Quaternion.identity);
            
            gameObject.SetActive(false);
        }
    }
    public void Damage(float damage)
    {
        _hitPoint -= damage;
    }
    public bool IsDeath()
    {
        return _death;
    }
    public void SetDeath(bool flag)
    {
         _death = flag;
    }
    public IEnumerator RedBlink()
    {
        while (true)
        {
            _renderer.material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            _renderer.material.color = _masterColor;
            yield return new WaitForSeconds(0.1f);
        }
    }
    public EnemyType GetEnemyType()
    {
        return _enemyType;
    }
}
