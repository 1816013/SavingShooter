using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsShooting : MonoBehaviour
{
    private Animator animator;
    public GameObject bulletPrefab;
    public GameObject gameController;
    private ObjectPooling _pool;
    private GameObject player;
    private PlayerStatas playerStatas;
    private Shoot _shoot;
    public int maxAmmo = 50;
    private int nowAmmo;
    private float shotTime = 0.0f;
    private bool shotF = false;
    private bool reloading = false;
    private float reloadTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.root.gameObject;
        playerStatas = player.GetComponent<PlayerStatas>();
        animator = player.GetComponentInChildren<Animator>();
        nowAmmo = maxAmmo;
        _shoot = gameController.GetComponent<Shoot>();
        _pool = gameController.GetComponent<ObjectPooling>();
        _pool.CreatePool(bulletPrefab, maxAmmo, bulletPrefab.GetInstanceID(), Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if (!reloading)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                animator.SetBool("shotF", true);
                shotTime += 1;
                if (shotTime % 5 == 0 && nowAmmo > 0)
                {
                    shotF = true;
                }
            }
            else
            {
                animator.SetBool("shotF", false);
                if (Input.GetKeyDown(KeyCode.R))   // リロード
                {
                    Reload();
                }
            }
            if(nowAmmo == 0)
            {
                Reload();
            }
        }
        else
        {
            reloadTime += Time.deltaTime;
        }
        if(reloadTime > 1)
        {
            reloading = false;
            animator.SetBool("Reload", false);
        }
    }
    private void FixedUpdate()
    {
        if (shotF)
        {            
            nowAmmo -= 1;
            _shoot.Shooting(ref _pool, bulletPrefab, transform, 3000);
            //Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0))
            shotF = false;
            
        }
    }

    private void Reload()
    {
        if (nowAmmo != maxAmmo)
        {          
            reloadTime = 0;
            animator.SetBool("shotF", false);
            animator.SetBool("Reload", true);
            playerStatas.AddPlayerEnergy(-5);
            reloading = true;
            nowAmmo = maxAmmo;
        }
    }


    public int GetNowAmmo()
    {
        return nowAmmo;
    }
}
