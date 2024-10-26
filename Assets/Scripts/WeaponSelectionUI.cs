
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WeaponSelectionUI : MonoBehaviour
{
    public PlayerWeaponManager weaponManager;
    public List<Weapon> availableWeapons;

    public void OnWeaponSelected(int index)
    {
        weaponManager.EquipWeapon(availableWeapons[index]);
    }
}

