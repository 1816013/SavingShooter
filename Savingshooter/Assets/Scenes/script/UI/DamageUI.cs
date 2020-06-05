using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();
        _image.color = Color.clear;
    }

    void Update()
    {
        if (_image.color != Color.clear)
        {
            _image.color = Color.Lerp(_image.color, Color.clear, Time.deltaTime);
        }
        
    }

    public void DamageEffect()
    {
        _image.color = new Color(0.5f, 0f, 0f, 0.5f);
    }
}
