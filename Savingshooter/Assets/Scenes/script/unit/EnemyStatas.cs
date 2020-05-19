using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatas : MonoBehaviour
{
    public GameObject detonator;        // 爆発プレハブ
    private GameObject gameController;
    private Animator animator;
    private bool death;
    [SerializeField]
    private float hitPoint = 100.0f;
    private int score = 50;     // 敵の強さで変わる

    private void Start()
    {
        death = false;
        gameController = GameObject.Find("GameController");
        animator = transform.GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(hitPoint <= 0 && !death)
        {
            death = true;
            animator.SetBool("Destroy", true);
            gameController.GetComponent<GameController>().AddScore(score);
            GameObject exp = (GameObject)Instantiate(detonator.gameObject, transform.position, Quaternion.identity);
            Destroy(gameObject, 1.0f);
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
}
