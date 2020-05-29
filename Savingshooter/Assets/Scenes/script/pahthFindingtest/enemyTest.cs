using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTest : MonoBehaviour
{
    private Pathfinding pathfinding;
    private targetGenerator _targetGenerator;
    private Vector3 _target;
    // Start is called before the first frame update
    void Awake()
    {
        pathfinding = gameObject.GetComponent<Pathfinding>();
    }
    private void Start()
    {
        _target = pathfinding.PathFind(transform.position, 0.25f).pos;
    }
    private void OnEnable()
    {
      //  _target = pathfinding.PathFind(transform.position, 0.25f).pos;
    }

    // Update is called once per frame
    void Update()
    { 
        if ((_target - transform.position).magnitude < 0.1f)
        {
            _target = Vector3.zero;
            _target = pathfinding.PathFind(transform.position, 0.25f).pos;
            //gameObject.SetActive(false);
        }
        transform.LookAt(_target);
        transform.position += transform.forward * Time.deltaTime;
       

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
