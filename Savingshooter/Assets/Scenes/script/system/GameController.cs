using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    static public int _score = 0;
    [SerializeField]
    private GameObject _pauseUI = null;

    private float fps;

    private void Start()
    {
        _score = 0;
    }
    // Update is called once per frame
    void Update()
    {
        fps = 1f / Time.deltaTime;
          Debug.Log(Time.timeSinceLevelLoad);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseUI.SetActive(!_pauseUI.activeSelf);
            if(_pauseUI.activeSelf)
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
        return _score;
    }

    public void AddScore(int add)
    {
        _score += add;
    }
}
