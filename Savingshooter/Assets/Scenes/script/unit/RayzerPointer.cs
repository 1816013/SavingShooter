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
    }

    // Update is called once per frame
    void LateUpdate()
    {
        razerPointer.SetPosition(0, transform.position);
        razerPointer.SetPosition(1, transform.position + transform.forward * 50);
    }
}
