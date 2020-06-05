using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour
{
    [SerializeField]
    private GameObject _textobj = null;
    [SerializeField]
    private GameObject _player = null;

    void FixedUpdate()
    {
        Text popText = _textobj.GetComponent<Text>();
        float energy = _player.GetComponent<PlayerStatas>().GetPlayerEnergy();
        energy = Mathf.Floor(energy);
        popText.text = "EN " + energy.ToString(); 
    }
}
