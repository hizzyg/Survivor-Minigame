using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

    public Player playerHealth;
    public float restartDelay = 5f;

    Animator anim;
    float restartTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        PlayersCurrentHealth();
    }

   void PlayersCurrentHealth()
   {
       if (playerHealth.currentHealth <= 0)
       {
           anim.SetTrigger("GameOver");
           restartTime += Time.deltaTime;
   
           if (restartTime >= restartDelay)
           {
               SceneManager.LoadScene("CharacterSelect");
               //Application.LoadLevel(Application.loadedLevel);
           }
       }
   }
}
