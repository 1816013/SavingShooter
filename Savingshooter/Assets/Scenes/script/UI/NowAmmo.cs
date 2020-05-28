using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NowAmmo : MonoBehaviour
{
    public GameObject textobj;
    public GameObject shooting;
    // Update is called once per frame
    void FixedUpdate()
    {
        Text popText = textobj.GetComponent<Text>();
        int nowAmmo = shooting.GetComponent<TpsShooting>().GetNowAmmo();
        popText.text = "Ammo " + nowAmmo.ToString();
    }
}
