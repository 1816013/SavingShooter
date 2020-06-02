using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{  
    public void Shooting(ref ObjectPooling pool, GameObject bulletPrefab, Transform transform,float power)
    { 
        GameObject bullet = pool.GetPoolObj(bulletPrefab.GetInstanceID(), transform.position);
        bullet.transform.rotation = Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0); 
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = Vector3.zero;
        bulletRb.AddForce(transform.forward * power);
    }
}
