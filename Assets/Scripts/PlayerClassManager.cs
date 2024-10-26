using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerClassManager : MonoBehaviour
{
    public PlayerClass playerClass;

    [SerializeField] PlayerClass defaultClass;
    [SerializeField] Weapon defaultWeapon;
    private GameObject currentWeapon;

    private Weapon equippedWeapon;

    void Start()
    {
        // Load the selected class and weapon from PlayerData
        playerClass = PlayerSaveData.selectedClass;
        equippedWeapon = PlayerSaveData.selectedWeapon;

        if (playerClass != null && equippedWeapon != null)
        {
            // Initialize the player with the selected class and weapon
            Debug.Log("Player Class: " + playerClass.className + ", Weapon: " + equippedWeapon.weaponName);
            EquipWeapon(equippedWeapon.weaponPrefab);
        }
        else
        {
            Debug.LogError("No class or weapon selected!");
            //set default class and weapon
            playerClass = defaultClass;
            equippedWeapon = defaultWeapon;
            EquipWeapon(equippedWeapon.weaponPrefab);
        }
    }

    // Equip the selected weapon
    public void EquipWeapon(GameObject weaponPrefab)
    {
        // Logic to equip the weapon (instantiate weapon prefab, attach to player, etc.)
        GameObject newWeapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
        newWeapon.transform.SetParent(this.transform);  // Attach to the player
    }

    public void EquipWeapon2(GameObject weaponPrefab)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }
        currentWeapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
        currentWeapon.transform.SetParent(this.transform); // Attach the weapon to the player
    }
     public void ActivateClassAbility()
    {
        playerClass.UseSpecialAbility();  // Call the class-specific ability
    }
}
