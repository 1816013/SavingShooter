using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsCameraControl : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private void Start()
    {
        offset = transform.position;
    }

    void FixedUpdate()
    {
        transform.position = offset + player.transform.position;
    }
}
