using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayzerPointer : MonoBehaviour
{

    private LineRenderer _razerPointer;
    // Start is called before the first frame update
    void Start()
    {
        _razerPointer = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _razerPointer.SetPosition(0, transform.position);
        _razerPointer.SetPosition(1, transform.position + transform.forward * 50);
    }
}
