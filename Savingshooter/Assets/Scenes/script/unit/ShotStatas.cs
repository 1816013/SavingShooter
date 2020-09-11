using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotStatas : MonoBehaviour
{
    private GameObject _player;
    private PlayerStatas _playerStatas;

    private float _shotPower;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerStatas = _player.GetComponent<PlayerStatas>();
        _shotPower = _playerStatas.GetPlayerPower() * 100.0f;
    }
    private void OnEnable()
    {
        StartCoroutine(DelayDestroyBullet(2.0f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy")|| other.CompareTag("Player") || other.CompareTag("Item") || other.CompareTag("shell")|| other.CompareTag("enemyShot"))
        {
            return;
        }
        DestroyBullet();
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
        return _shotPower;
    }
}
