using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class col : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            gameObject.SetActive(false);
        }
    }
}
