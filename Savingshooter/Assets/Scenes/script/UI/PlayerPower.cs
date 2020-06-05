using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPower : MonoBehaviour
{
    [SerializeField]
    private GameObject _textobj = null;
    [SerializeField]
    private GameObject _player = null;

    void FixedUpdate()
    {
        Text popText = _textobj.GetComponent<Text>();
        float power = _player.GetComponent<PlayerStatas>().GetPlayerPower();
        power *= 100;
        popText.text = "Power" + power.ToString() + "%";
    }
}
