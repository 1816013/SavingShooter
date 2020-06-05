using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyattackHit : MonoBehaviour
{
    [SerializeField]
    private GameObject _damageObj = null;
    private DamageUI _damageUI;
    private void Start()
    {
        _damageUI = _damageObj.GetComponent<DamageUI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemyAttack")
        {
            _damageUI.DamageEffect();
            gameObject.GetComponent<PlayerStatas>().AddPlayerEnergy(-20);
        }
        if (other.tag == "enemyShot")
        {
            _damageUI.DamageEffect();
            gameObject.GetComponent<PlayerStatas>().AddPlayerEnergy(-20);
        }
    }
}
