using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changescene : MonoBehaviour
{
    public void changeToScene()
    {
        SceneManager.LoadScene("Starship");
        //print("Scene Changed");
    }
    //exit the game
    public void exitGame()
    {
        Application.Quit();
        //print("Game Exited");
    }
    //change to main menu
    public void changeToMainMenu()
    {
        SceneManager.LoadScene("Main");
        //print("Scene Changed");
    }
}
