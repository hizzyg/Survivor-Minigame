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
        SceneManager.LoadScene("Credits");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Introduction");
    }
}