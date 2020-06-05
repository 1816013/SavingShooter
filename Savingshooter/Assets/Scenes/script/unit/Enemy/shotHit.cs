using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotHit : MonoBehaviour
{
    private EnemyStatas _enemyStatas;              //Statasクラス

    // Start is called before the first frame update
    void Awake()
    {
        _enemyStatas = gameObject.GetComponent<EnemyStatas>();
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("shell"))
        {
            ShotStatas shotStatas = other.GetComponent<ShotStatas>();
            if (_enemyStatas == null)
            {
                Debug.Log("バグっている");
            }
            //ステータスクラスのDamage関数を呼び出す
            _enemyStatas.Damage(shotStatas.GetShotPower());

            //ぶつかってきたオブジェクトを破壊する.
            other.gameObject.SetActive(false);
        }
    }
}
