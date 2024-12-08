using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventorysystem : MonoBehaviour
{
    public GameObject inventoryPanel;
    public List<ItemObject> items = new List<ItemObject>();

    public HealthSystem healthSystem;

    private bool isPaused = false;
    private AudioSource[] musicSources;

    //use player health system
    [SerializeField] PlayerClassManager player;

    void Update()
    {
        //check if inventory is open
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryPanel.activeSelf)
            {
                TogglePause();
                HideInventory();
            }
            else
            {
                TogglePause();
                ShowInventory();
            }
        }
    }

    // Non-stackable AddItem method
    public void AddItem(ItemObject newItem)
    {
        // Check if the item already exists in inventory
        ItemObject existingItem = items.Find(item => item.itemName == newItem.itemName);
        if (existingItem == null)
        {
            items.Add(newItem);  // Only add if the item doesn't already exist in the inventory
        }
    }

    // Non-stackable RemoveItem method
    public void RemoveItem(ItemObject itemToRemove)
    {
        // Check if item exists in inventory
        ItemObject existingItem = items.Find(item => item.itemName == itemToRemove.itemName);

        if (existingItem != null)
        {
            // Remove item from inventory, as quantities are not tracked
            items.Remove(existingItem);
        }
    }

    public ItemObject FindItem(string itemName)
    {
        return items.Find(item => item.itemName == itemName);
    }

    //hide the inventory panel
    public void HideInventory()
    {
        inventoryPanel.SetActive(false);
    }

    void Start()
    {
        // Initially, show the class selection panel and hide the weapon selection panel
        HideInventory();

        // Automatically find all AudioSources in the scene
        musicSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
    }

    public void ShowInventory()
    {
        inventoryPanel.SetActive(true);
    }

    // Add health to player
    public void AddHealth(int health)
    {
        ItemObject healthPotion = FindItem("Health Potion");
        if (healthPotion != null)
        {
            // Remove health potion from inventory and apply health increase
            RemoveItem(healthPotion);
            Debug.Log("Player health increased by " + health);

            player.AddHealth(health);
        }
        else
        {
            Debug.Log("No health potions in inventory");
        }
    }

    internal void AddStamina(int v)
    {
        ItemObject staminaPotion = FindItem("Stamina Potion");
        if (staminaPotion != null)
        {
            // Remove stamina potion from inventory and apply stamina increase
            RemoveItem(staminaPotion);
            player.AddStamina(v);
        }
        else
        {
            Debug.Log("No stamina potions in inventory");
        }
    }

    internal void AddMana(int v)
    {
        ItemObject manaPotion = FindItem("Mana Potion");
        if (manaPotion != null)
        {
            // Remove mana potion from inventory and apply mana increase
            RemoveItem(manaPotion);
            player.AddMana(v);
        }
        else
        {
            Debug.Log("No mana potions in inventory");
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f; // Pauses the game
        isPaused = true;
        Debug.Log("Game Paused");
        foreach (AudioSource source in musicSources)
        {
            if (source != null && source.isPlaying)
            {
                source.Pause(); // Pause each music track
            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resumes the game
        isPaused = false;
        Debug.Log("Game Resumed");
        foreach (AudioSource source in musicSources)
        {
            if (source != null)
            {
                source.UnPause(); // Resume each music track
            }
        }
    }
}
