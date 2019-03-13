using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCreat : MonoBehaviour
{

    private GameObject[] characterList;
    // Default Index of the model
    private int selectionIndex;
    public float turnSpeed;

    // Use this for initialization
    void Start()
    {
        selectionIndex = PlayerPrefs.GetInt("CharacterSelected");

        // Fill the Array with models
        characterList = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        // toggle off their renderer
        foreach (GameObject go in characterList)
        {
            go.SetActive(false);
        }

        // toggle on the first index
        if (characterList[selectionIndex])
        {
            characterList[selectionIndex].SetActive(true);
        }
    }

    public void SelectLeft()
    {
        // Toggle of the current model
        characterList[selectionIndex].SetActive(false);

        selectionIndex--;
        if (selectionIndex < 0)
        {
            selectionIndex = characterList.Length - 1; 
        }
        // Toggle on the current model
        characterList[selectionIndex].SetActive(true);
    }

    public void SelectRight()
    {
        // Toggle of the current model
        characterList[selectionIndex].SetActive(false);

        selectionIndex++;
        if (selectionIndex == characterList.Length)
        {
            selectionIndex = 0;
        }
        // Toggle on the current model
        characterList[selectionIndex].SetActive(true);
    }

    public void JoystickKeys()
    {
        // Wählt den linken Char aus!
        if (Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            SelectLeft();
        }
        // Wählt den rechen Char aus!
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            SelectRight();
        }
    }

    public void SelectCharacter()
    {
        PlayerPrefs.SetInt("CharacterSelected",selectionIndex);
        SceneManager.LoadScene("Level1");
    }

    public void SelectMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void TurnAround()
    {
        if (Input.GetButton("Fire2"))
        {
            transform.Rotate(new Vector3(0,-(Input.GetAxis("Mouse X")),0)*turnSpeed);
        }
        if (Input.GetAxis("RHorizontal") != 0)
        {
            transform.Rotate(new Vector3(0, -(Input.GetAxis("RHorizontal")), 0) * turnSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TurnAround();
        JoystickKeys();
    }
}
