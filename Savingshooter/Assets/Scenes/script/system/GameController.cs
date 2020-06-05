using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    static public int score = 0;
    [SerializeField]
    private GameObject pauseUI = null;
   

    private void Start()
    {
        score = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUI.SetActive(!pauseUI.activeSelf);
            if(pauseUI.activeSelf)
            {
                Time.timeScale = 0;
            }
            else
            {  
                Time.timeScale = 1;
            }
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("clearScene");
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int add)
    {
        score += add;
    }
}
