using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
