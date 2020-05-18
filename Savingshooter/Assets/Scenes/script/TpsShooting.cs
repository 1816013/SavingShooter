using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsShooting : MonoBehaviour
{
    private Animator animator;
    public GameObject bulletPrefab;
    private GameObject player;
    public int maxAmmo = 50;
    private int nowAmmo;
    private float shotInterval = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        animator = player.GetComponentInChildren<Animator>();
        nowAmmo = maxAmmo;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            animator.SetBool("shotF", true);
            shotInterval += 1;
            if (shotInterval % 5 == 0 && nowAmmo > 0)
            {
                nowAmmo -= 1;
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

                bulletRb.AddForce(transform.forward * 1500);

                //GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
                //Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                //Ray ray = new Ray(transform.position,transform.forward);
                //RaycastHit hit;//ヒットした座標
                //if (Physics.Raycast(ray, out hit, 100.0f))
                //{//レイを飛ばしヒットしたら
                // //ヒット座標に向かって弾を飛ばす
                //    bulletRb.velocity = (hit.point - bullet.transform.position).normalized * 50.0f;
                //    Debug.DrawRay(ray.origin, hit.point - ray.origin, Color.blue);
                //}
                //else
                //{ //レイにヒットしなければ
                //  //射程距離の地点に向かって弾を飛ばす
                //    bulletRb.velocity = (ray.GetPoint(100.0f) - bullet.transform.position).normalized * 50.0f;
                //    Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 100.0f);
                //}

                //射撃されてから3秒後に銃弾を破壊する.

                Destroy(bullet, 3.0f);
            }

        }
        else 
        {
            animator.SetBool("shotF", false);
            if (Input.GetKeyDown(KeyCode.R))   // リロード
            {
                if (nowAmmo != maxAmmo)
                {
                    player.GetComponent<PlayerStatas>().AddPlayerEnergy(-5);
                    nowAmmo = maxAmmo;
                }
            }
        }
    }

    public int GetNowAmmo()
    {
        return nowAmmo;
    }
}
