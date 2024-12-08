using System;
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
        print("Scene Changed to Main Menu");
    }

    //new game
    public void newGame()
    {
        SceneManager.LoadScene("ClassAndWeaponSelection");
        print("Scene Changed to Class and Weapon Selection");
    }

    //load game from save


    //options
    public void options()
    {
        SceneManager.LoadScene("Options");
        //print("Scene Changed");
    }

    public void changescene1(String scene)
    {
        SceneManager.LoadScene(scene);
    }
    


}
