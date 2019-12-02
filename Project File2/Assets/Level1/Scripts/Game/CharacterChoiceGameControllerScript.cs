using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterChoiceGameControllerScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        JoystickKeys();
    }

    // Update is called once per frame
    void Update()
    {
        JoystickKeys();
    }

    public void JoystickKeys()
    {
        // Lädt das Menu beim Klicken von Button "B"!
        if (Input.GetKey(KeyCode.Joystick1Button1))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
