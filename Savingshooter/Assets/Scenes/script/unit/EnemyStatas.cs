using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Destroy,    // 自爆
    Shoot,      // 射撃
    ClossRange  // 近接攻撃
}

public class EnemyStatas : MonoBehaviour
{
    public GameObject detonator;        // 爆発プレハブ
    private GameObject gameController;
    private Animator animator;
    private bool death;
    [SerializeField]
    private float hitPoint;
    [SerializeField]
    private int score = 50;     // 敵の強さで変わる
    private Renderer _renderer;
    private Color masterColor;
    [SerializeField]
    private EnemyType _enemyType;

    private void Awake()
    {
        animator = transform.GetComponentInChildren<Animator>();
        _renderer = gameObject.GetComponentInChildren<Renderer>();
        masterColor = _renderer.material.color;
    }

    private void Start()
    {
        death = false;
        gameController = GameObject.FindGameObjectWithTag("GameController");
       
    }
    private void OnEnable()
    {
        hitPoint = 100.0f;
        death = false;
    }
    private void OnDisable()
    {
        _renderer.material.color = masterColor;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(hitPoint <= 0 && !death)
        {
            death = true;
           
        }
        if(death)
        {
            if (animator != null)
            {
                animator.SetBool("Destroy", true);
            }
            gameController.GetComponent<GameController>().AddScore(score);
            GameObject exp = (GameObject)Instantiate(detonator.gameObject, transform.position, Quaternion.identity);
            
            gameObject.SetActive(false);
        }
    }
    public void Damage(float damage)
    {
        hitPoint -= damage;
    }
    public bool IsDeath()
    {
        return death;
    }
    public void SetDeath(bool flag)
    {
         death = flag;
    }
    public IEnumerator RedBlink()
    {
        while (true)
        {
            _renderer.material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            _renderer.material.color = masterColor;
            yield return new WaitForSeconds(0.1f);
        }
    }
    public EnemyType GetEnemyType()
    {
        return _enemyType;
    }
}
