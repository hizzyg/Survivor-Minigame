using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameESCMenu : MonoBehaviour
{
    public Transform canvas;
    public bool Pause;

    void Update()
    {
        ESC();
    }

    public void ESC()
    {
        #region ESC Menu
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if (canvas.gameObject.activeInHierarchy == false)
            {
                canvas.gameObject.SetActive(true);
            }
            else
            {
                canvas.gameObject.SetActive(false);
            }
        }
        #endregion
        #region Pause
        if (canvas.gameObject.activeInHierarchy == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        #endregion
       // #region Maus
       // if (canvas.gameObject.activeInHierarchy == false)
       // {
       //     Cursor.visible = false;
       // }
       // else
       // {
       //     Cursor.visible = true;
       // }
       // #endregion

    }

    public void changeToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void quitTheGame()
    {
        Application.Quit();
    }
}
