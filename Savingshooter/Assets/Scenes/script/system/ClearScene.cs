using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearScene : MonoBehaviour
{
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ChangeScene();
        }
    }
    void ChangeScene()
    {
        SceneManager.LoadScene("titleScene");
    }
}
