using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Introduction : MonoBehaviour
{

    public void Page2()
    {
        if (true)
        {
            SceneManager.LoadScene("Introduction2");
        }
    }
    public void Page1()
    {
        if (true)
        {
            SceneManager.LoadScene("Credits");
        }
    }
    public void BackToMenu()
    {
        if (true)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
