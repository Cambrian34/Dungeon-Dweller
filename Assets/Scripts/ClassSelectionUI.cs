using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ClassSelectionUI : MonoBehaviour
{
    public PlayerClassManager playerClassManager;
    public List<PlayerClass> availableClasses;

    public void OnClassSelected(int index)
    {
        playerClassManager.playerClass = availableClasses[index];
        playerClassManager.EquipWeapon(playerClassManager.playerClass.defaultWeaponPrefab);
    }
}

