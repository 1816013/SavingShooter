using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotHit : MonoBehaviour
{
    public GameObject enemy;   //敵オブジェクト
    private EnemyStatas enemyStatas;              //Statasクラス

    // Start is called before the first frame update
    void Start()
    {
        enemyStatas = enemy.GetComponent<EnemyStatas>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("shell"))
        {
            ShotStatas shotStatas = other.GetComponent<ShotStatas>();
            //HPクラスのDamage関数を呼び出す
            enemyStatas.Damage(shotStatas.GetShotPower());

            //ぶつかってきたオブジェクトを破壊する.
            Destroy(other.gameObject);
        }
    }
}
