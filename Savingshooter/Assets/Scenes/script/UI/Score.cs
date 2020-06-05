using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]
    private GameObject _textobj = null;

    void FixedUpdate()
    {
        Text popText = _textobj.GetComponent<Text>();
        int score = GameController._score; 
        popText.text = "Score  " + score.ToString();
    }
}
