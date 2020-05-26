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
    public int maxAmmo = 50;
    private int nowAmmo;
    private float shotInterval = 0.0f;
    private bool shotF = false;
    private bool reloading = false;
    private float reloadInterval = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.root.gameObject;
        playerStatas = player.GetComponent<PlayerStatas>();
        animator = player.GetComponentInChildren<Animator>();
        nowAmmo = maxAmmo;
        _pool = gameController.GetComponent<ObjectPooling>();
        _pool.CreatePool(bulletPrefab, maxAmmo, bulletPrefab.GetInstanceID());
    }

    // Update is called once per frame
    void Update()
    {
        if (!reloading)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                animator.SetBool("shotF", true);
                shotInterval += 1;
                if (shotInterval % 5 == 0 && nowAmmo > 0)
                {
                    shotF = true;
                }
            }
            else
            {
                animator.SetBool("shotF", false);
                if (Input.GetKeyDown(KeyCode.R))   // リロード
                {
                    if (nowAmmo != maxAmmo)
                    {
                        reloadInterval = 0;
                        animator.SetBool("Reload", true);
                        playerStatas.AddPlayerEnergy(-5);
                        reloading = true;
                        nowAmmo = maxAmmo;
                    }
                }
            }
        }
        else
        {
            reloadInterval += Time.deltaTime;
        }
        if(reloadInterval > 1)
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
            //Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0))
            GameObject bullet = _pool.GetPoolObj(bulletPrefab.GetInstanceID());
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.velocity = Vector3.zero;
            bulletRb.AddForce(transform.forward * 1500);
            shotF = false;
            
        }
    }


    public int GetNowAmmo()
    {
        return nowAmmo;
    }
}
