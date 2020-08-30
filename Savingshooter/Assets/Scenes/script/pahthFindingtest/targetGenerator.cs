using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetGenerator : MonoBehaviour
{
    public GameObject targetPrefab;
    private GameObject target;

    public void GenerateTarget(Vector3 pos)
    {
        target = GameObject.Instantiate(targetPrefab);
        target.transform.position = new Vector3(pos.x, 1f, pos.z);
    }
 
}
