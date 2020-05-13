using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyattackHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemyAttack")
        {
            gameObject.GetComponent<PlayerStatas>().AddPlayerEnergy(-20);
        }
    }
}
