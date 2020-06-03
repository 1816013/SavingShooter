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
        player = GameObject.FindGameObjectWithTag("Player");
        playerStatas = player.GetComponent<PlayerStatas>();
        shotPower = playerStatas.GetPlayerPower() * 100.0f;
    }
    private void OnEnable()
    {
        StartCoroutine(DelayDestroyBullet(1.0f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("enemy")&& !other.CompareTag("Player") && !other.CompareTag("Item"))
        {
            gameObject.SetActive(false);
        }
    }


    private void DestroyBullet()
    {
        gameObject.SetActive(false);
    }
    IEnumerator DelayDestroyBullet(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    public float GetShotPower()
    {
        return shotPower;
    }
}
