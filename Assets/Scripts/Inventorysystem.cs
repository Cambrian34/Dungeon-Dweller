using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventorysystem : MonoBehaviour
{
    
    public GameObject inventoryPanel;
    public List<ItemObject> items = new List<ItemObject>();

    public HealthSystem healthSystem;

    //use player health system
    [SerializeField] PlayerClassManager player;
    void Update()
    {
        //check if inventory is open
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryPanel.activeSelf)
            {
                HideInventory();
            }
            else
            {
                ShowInventory();
            }
        }
    }
    public void AddItem(ItemObject newItem)
    {
        // Check if the item already exists in inventory to increase quantity
        ItemObject existingItem = items.Find(item => item.itemName == newItem.itemName);
        if (existingItem != null)
        {
            existingItem.quantity += newItem.quantity;
        }
        else
        {
            items.Add(newItem);
        }
    }

    public void RemoveItem(ItemObject itemToRemove)
    {
        items.Remove(itemToRemove);
    }

    public ItemObject FindItem(string itemName)
    {
        return items.Find(item => item.itemName == itemName);
    }

    //hide rhe inventory panel
    public void HideInventory()
    {
        inventoryPanel.SetActive(false);
    }
    void Start()
    {
        // Initially, show the class selection panel and hide the weapon selection panel
        HideInventory();
    }

    public void ShowInventory()
    {
        inventoryPanel.SetActive(true);  
    }

    // Update is called once per frame to check for button press to show inventory and hide inventory

    // add health to player
    public void AddHealth(int health)
    {
        // Find the health potion in the inventory
        ItemObject healthPotion = FindItem("Health Potion");
        if (healthPotion != null)
        {
            // Increase the player's health
            healthPotion.quantity -= 1;
            //healthSystem.Heal(health);
            Debug.Log("Player health increased by " + health);

            player.AddHealth(health);


        }
        else
        {
            Debug.Log("No health potions in inventory");
        }
    }
    

}

