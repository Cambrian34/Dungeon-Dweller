using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventorysystem : MonoBehaviour
{
    
    public GameObject inventoryPanel;
    public List<ItemObject> items = new List<ItemObject>();
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
    

}

