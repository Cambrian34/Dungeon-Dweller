using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] Weapon equippedWeapon;

    [SerializeField]Transform weaponHolder;
    [SerializeField] Weapon defaultWeapon;
    [SerializeField]Boolean equip1 = false;
    [SerializeField]Boolean equip2 = false;


    void Start()
    {
        // Load the selected class and weapon from PlayerData
        equippedWeapon = PlayerSaveData.selectedWeapon;

       
        if ( equippedWeapon != null)
        {
            // Initialize the player with the selected class and weapon
            Debug.Log(" Weapon: " + equippedWeapon.weaponName);
            EquipWeapon(equippedWeapon);
            equip1 = true;

            
        }
        else
        {
            Debug.LogError("No class or weapon selected!");
            //set default class and weapon
            
            equippedWeapon = defaultWeapon;
            EquipWeapon(equippedWeapon);
            //set health
            equip2 = true;
            
        }
    }

    
    
    public void EquipWeapon(Weapon newWeapon)
    {
        // Instantiate the weapon model and attach it to the player
        Debug.Log("Equipped weapon: " + newWeapon.weaponName);
        GameObject weaponModel = Instantiate(newWeapon.weaponPrefab, weaponHolder);
        //set the weapon model's position and rotation
        weaponModel.transform.localPosition = Vector3.zero;
    }

    public void Attack()
    {
        // Perform the attack logic, using the stats from the weapon
        Debug.Log("Attacking with " + equippedWeapon.weaponName);
        if (equippedWeapon.hasSpecialAbility)
        {
            equippedWeapon.UseSpecialAbility();
        }
    }
    public string GetEquippedWeapon()
    {
        if (equip1 == true)
        {
            equippedWeapon = PlayerSaveData.selectedWeapon;
            return equippedWeapon.weaponName;
        }
        else if (equip2 == true)
        {
            equippedWeapon = defaultWeapon;
            return equippedWeapon.weaponName;
        }
        else
        {
        
            equippedWeapon = defaultWeapon;
            return equippedWeapon.weaponName;
        }
    }
}

public class WeaponTypes
{
    public void SwordAttack()
    {
        Debug.Log("Swinging Sword!");
    }

    public void BowAttack()
    {
        Debug.Log("Shooting Arrow!");
    }

    public void StaffAttack()
    {
        Debug.Log("Casting Fireball!");
    }

    //get equipped weapon
    

}