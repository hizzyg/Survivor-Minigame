using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public void B()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            SceneManager.LoadScene("Menu");
        }
        if (true)
        {
            SceneManager.LoadScene("Menu");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
