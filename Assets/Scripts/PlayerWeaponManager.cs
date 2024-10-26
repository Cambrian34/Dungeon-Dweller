using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerWeaponManager : MonoBehaviour
{
    public Weapon equippedWeapon;
    
    public void EquipWeapon(Weapon newWeapon)
    {
        equippedWeapon = newWeapon;
        // Instantiate the weapon model and attach it to the player
        Debug.Log("Equipped weapon: " + newWeapon.weaponName);
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
}