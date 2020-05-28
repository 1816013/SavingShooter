using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public GameObject textobj;
    // Update is called once per frame
    void FixedUpdate()
    {
        Text popText = textobj.GetComponent<Text>();
        int score = GameController.score; 
        popText.text = "Score  " + score.ToString();
    }
}
