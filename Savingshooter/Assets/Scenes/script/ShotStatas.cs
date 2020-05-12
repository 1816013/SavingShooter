using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotStatas : MonoBehaviour
{
    private GameObject player;
    private PlayerStatas playerStatas;

    private float shotPower;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerStatas = player.GetComponent<PlayerStatas>();
        shotPower = playerStatas.GetPlayerPower() * 100.0f;
    }

    public float GetShotPower()
    {
        return shotPower;
    }
}
