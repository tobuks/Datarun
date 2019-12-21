using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private PlayerController player;
    public static bool isStart = false;
    public void PlayGame()
    {
        isStart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        /*player=GameObject.Find("Player").GetComponent<PlayerController>();
        player.GiveUp();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1,LoadSceneMode.Single);*/
        
    }
    public void LoadPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }


    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    
}
