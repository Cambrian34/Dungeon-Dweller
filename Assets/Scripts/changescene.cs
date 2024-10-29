using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changescene : MonoBehaviour
{
    
    //exit the game
    public void exitGame()
    {
        Application.Quit();
        //print("Game Exited");
    }
    //change to main menu
    public void changeToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        //print("Scene Changed");
    }

    //new game
    public void newGame()
    {
        SceneManager.LoadScene("ClassAndWeaponSelection");
        //print("Scene Changed");
    }

    //load game from save


    //options
    public void options()
    {
        SceneManager.LoadScene("Options");
        //print("Scene Changed");
    }

}
