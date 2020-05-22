using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotHit : MonoBehaviour
{
    private EnemyStatas enemyStatas;              //Statasクラス
    ShotStatas shotStatas;

    // Start is called before the first frame update
    void Start()
    {
        enemyStatas = gameObject.GetComponent<EnemyStatas>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("shell"))
        {
            ShotStatas shotStatas = other.GetComponent<ShotStatas>();
            if(shotStatas == null)
            {
                Debug.Log(other.name);
                Debug.Log("バグっている");
            }
            //HPクラスのDamage関数を呼び出す
            enemyStatas.Damage(shotStatas.GetShotPower());

            //ぶつかってきたオブジェクトを破壊する.
            other.gameObject.SetActive(false);
        }
    }
}
