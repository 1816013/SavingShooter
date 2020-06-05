using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = _player.transform.position + _offset;
    }
}
