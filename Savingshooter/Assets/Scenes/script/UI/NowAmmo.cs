using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NowAmmo : MonoBehaviour
{
    [SerializeField]
    private GameObject _textobj = null;
    [SerializeField]
    private GameObject _shooting = null;

    void FixedUpdate()
    {
        Text popText = _textobj.GetComponent<Text>();
        int nowAmmo = _shooting.GetComponent<TpsShooting>().GetNowAmmo();
        popText.text = "Ammo " + nowAmmo.ToString();
    }
}
