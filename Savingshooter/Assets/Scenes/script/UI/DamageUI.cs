using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    Image img;

    void Start()
    {
        img = GetComponent<Image>();
        img.color = Color.clear;
    }

    void Update()
    {
        if (img.color != Color.clear)
        {
            img.color = Color.Lerp(this.img.color, Color.clear, Time.deltaTime);
        }
        
    }

    public void DamageEffect()
    {
        img.color = new Color(0.5f, 0f, 0f, 0.5f);
    }
}
