using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayzerPointer : MonoBehaviour
{
    private LineRenderer razerPointer;
    // Start is called before the first frame update
    void Start()
    {
        razerPointer = gameObject.GetComponent<LineRenderer>();
        //offset = new Vector3(0.25f, 0, 0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        razerPointer.SetPosition(0, transform.position);
        razerPointer.SetPosition(1, transform.position + transform.forward * 50);
    }
}
