using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPower : MonoBehaviour
{
    public GameObject textobj;
    public GameObject player;
    // Update is called once per frame
    void FixedUpdate()
    {
        Text popText = textobj.GetComponent<Text>();
        float power = player.GetComponent<PlayerStatas>().GetPlayerPower();
        power *= 100;
        popText.text = "Power" + power.ToString() + "%";
    }
}
