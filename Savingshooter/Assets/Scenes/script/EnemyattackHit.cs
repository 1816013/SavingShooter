using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyattackHit : MonoBehaviour
{
    public GameObject damageObj;
    private DamageUI damageUI;
    private void Start()
    {
        damageUI = damageObj.GetComponent<DamageUI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemyAttack")
        {
            damageUI.DamageEffect();
            gameObject.GetComponent<PlayerStatas>().AddPlayerEnergy(-20);
        }
    }
}
