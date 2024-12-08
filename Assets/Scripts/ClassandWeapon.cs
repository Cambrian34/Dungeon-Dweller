using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ClassAndWeaponSelection : MonoBehaviour
{
    public PlayerClass[] availableClasses;   // Array of classes
    public Weapon[] availableWeapons;        // Array of weapons

    private PlayerClass selectedClass;
    private Weapon selectedWeapon;
    

    public void SelectClass(int index)
    {
        selectedClass = availableClasses[index];
        Debug.Log("Selected Class: " + selectedClass.className);
    }

    public void SelectWeapon(int index)
    {
        selectedWeapon = availableWeapons[index];
        Debug.Log("Selected Weapon: " + selectedWeapon.weaponName);
    }

    public void StartGame()
    {
        if (selectedClass == null || selectedWeapon == null)
        {
            Debug.Log("Please select a class and weapon.");
            return;
        }

        // Save the selected class and weapon to a static manager or player prefs
        PlayerSaveData.selectedClass = selectedClass;
        PlayerSaveData.selectedWeapon = selectedWeapon;

        // Load the game scene
        SceneManager.LoadScene("MainGame");
    }
}

