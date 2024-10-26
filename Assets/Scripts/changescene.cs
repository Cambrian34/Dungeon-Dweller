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
        SceneManager.LoadScene("Main");
        //print("Scene Changed");
    }

    //new game
    public void newGame()
    {
        SceneManager.LoadScene("ClassAndWeaponSelection");
        //print("Scene Changed");
    }

    //load game from save

}
