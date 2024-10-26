using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuManager : MonoBehaviour
{
    public GameObject classSelectionPanel;  // Reference to the class selection panel
    public GameObject weaponSelectionPanel; // Reference to the weapon selection panel

    void Start()
    {
        // Initially, show the class selection panel and hide the weapon selection panel
        ShowClassSelection();
    }

    public void ShowClassSelection()
    {
        classSelectionPanel.SetActive(true);
        weaponSelectionPanel.SetActive(false);
    }

    public void ShowWeaponSelection()
    {
        classSelectionPanel.SetActive(false);
        weaponSelectionPanel.SetActive(true);
    }

    

}

