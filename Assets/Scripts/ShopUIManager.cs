
using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    [SerializeField] private GameObject shopCanvas;

    [SerializeField] private Inventorysystem playerInventory;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private int healthPotionPrice = 50;
    [SerializeField] private int manaPotionPrice = 30;
    [SerializeField] private int staminaPotionPrice = 40;

    [SerializeField] private ItemObject healthPotion;
    [SerializeField] private ItemObject manaPotion;
    [SerializeField] private ItemObject staminaPotion;

    [SerializeField] PlayerClassManager player;


    private void Start()
    {
        shopCanvas.SetActive(false); // Hide shop UI initially
        
    }

    // Call this method when the player interacts with the NPC
    public void OpenShop()
    {
        shopCanvas.SetActive(true);
    }

    // Close the shop when the player is done
    public void CloseShop()
    {
        shopCanvas.SetActive(false);
    }

    public void BuyHealthPotion()
    {
        if (player.Gold >= healthPotionPrice)
        {
            playerInventory.AddItem(new ItemObject("Health Potion"," ",healthPotion.icon,1)); // Adds 1 health potion to inventory
            player.Gold -= healthPotionPrice;
            Debug.Log("Purchased Health Potion");
            //update ui
            inventoryUI.UpdateUI();
            
        }
        else
        {
            Debug.Log("Not enough gold for Health Potion.");
        }
    }

    public void BuyManaPotion()
    {
        if (player.Gold >= manaPotionPrice)
        {
            playerInventory.AddItem(new ItemObject("Mana Potion"," ",manaPotion.icon,1)); // Adds 1 mana potion to inventory
            player.Gold -= manaPotionPrice;
            Debug.Log("Purchased Mana Potion");
            inventoryUI.UpdateUI();
        }
        else
        {
            Debug.Log("Not enough gold for Mana Potion.");
        }
    }

    public void BuyStaminaPotion()
    {
        if (player.Gold >= staminaPotionPrice)
        {
            playerInventory.AddItem(new ItemObject("Stamina Potion"," ",staminaPotion.icon,1));
            player.Gold -= staminaPotionPrice;
            Debug.Log("Purchased Stamina Potion");
            inventoryUI.UpdateUI();
        }
        else
        {
            Debug.Log("Not enough gold for Stamina Potion.");
        }
    }
    
}


