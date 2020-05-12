using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatas : MonoBehaviour
{
    private GameObject gameController;
    [SerializeField]
    private float hitPoint = 100.0f;
    private int score = 50;     // 敵の強さで変わる

    private void Start()
    {
        gameController = GameObject.Find("GameController");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(hitPoint <= 0)
        {
            gameController.GetComponent<GameController>().AddScore(score);
            Destroy(gameObject);
        }
    }
    public void Damage(float damage)
    {
        hitPoint -= damage;
    }
}
