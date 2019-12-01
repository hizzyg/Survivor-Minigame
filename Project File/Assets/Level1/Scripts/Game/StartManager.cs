using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{

    public void changeScene()
    {
        SceneManager.LoadScene("CharacterSelect");
    }
    public void Introduction()
    {
        SceneManager.LoadScene("Introduction");
    }
    public void Introduction2()
    {
        SceneManager.LoadScene("Introduction2");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}