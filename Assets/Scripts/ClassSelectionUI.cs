using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ClassSelectionUI : MonoBehaviour
{
    public PlayerClassManager playerClassManager;
    public PlayerWeaponManager playerWeaponManager;
    public List<PlayerClass> availableClasses;

    public void OnClassSelected(int index)
    {
        playerClassManager.playerClass = availableClasses[index];
        //playerWeaponManager.EquipWeapon(playerWeaponManager..defaultWeaponPrefab);
    }
}

