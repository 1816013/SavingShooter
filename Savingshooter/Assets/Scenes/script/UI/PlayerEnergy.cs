using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour
{
    public GameObject textobj;
    public GameObject player;
    // Update is called once per frame
    void FixedUpdate()
    {
        Text popText = textobj.GetComponent<Text>();
        float energy = player.GetComponent<PlayerStatas>().GetPlayerEnergy();
        energy = Mathf.Floor(energy);
        popText.text = "EN " + energy.ToString(); 
    }
}
