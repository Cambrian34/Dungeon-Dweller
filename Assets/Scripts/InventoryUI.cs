using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Inventorysystem inventory;  // Reference to the player's inventory
    public Transform inventoryPanel;  // UI panel to display items
    public GameObject itemSlotPrefab; // Prefab for each inventory slot

    //icons for items
    
    public Sprite healthPotionIcon;
    
    public Sprite manaPotionIcon;
    public Sprite staminaPotionIcon;
    /*
    public Sprite swordIcon;
    public Sprite shieldIcon;
    public Sprite bowIcon;
    public Sprite arrowIcon;
    public Sprite staffIcon;
    public Sprite wandIcon;
    */

    //array of icons
    //public Sprite[] itemIcons;
    



    void Start()
    {
        UpdateUI();
        
    }

    public void UpdateUI()
    {
        Debug.Log("Updating UI...");
        Debug.Log("Number of items in inventory: " + inventory.items.Count);

        if (inventoryPanel == null) { Debug.LogError("inventoryPanel is null"); return; }
        if (itemSlotPrefab == null) { Debug.LogError("itemSlotPrefab is null"); return; }
        if (inventory == null || inventory.items == null) { Debug.LogError("Inventory or inventory items list is null"); return; }
    
        // Clear current items in inventory panel
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }
    
        // Create a new button for each item
        foreach (ItemObject item in inventory.items)
        {
            GameObject slot = Instantiate(itemSlotPrefab, inventoryPanel);

            // Set the item name and quantity in the slot
            //USING TMPro
            if(item.itemName == "Health Potion")
            {
                TextMeshProUGUI[] texts = slot.GetComponentsInChildren<TextMeshProUGUI>();
                //set item name to object name
                texts[0].text = item.itemName;
                //set image to health potion icon
                Image[] images = slot.GetComponentsInChildren<Image>();

                //set icon to health potion
                images[0].sprite = healthPotionIcon;

                

                
            }
            
            else if(item.itemName == "Mana Potion")
            {
                TextMeshProUGUI[] texts = slot.GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = item.itemName;

                //set image to mana potion icon
                Image[] images = slot.GetComponentsInChildren<Image>();

                //set icon to mana potion
                images[0].sprite = manaPotionIcon;

            }
            else if(item.itemName == "Stamina Potion")
            {
                TextMeshProUGUI[] texts = slot.GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = item.itemName;

                //set image to stamina potion icon
                Image[] images = slot.GetComponentsInChildren<Image>();

                //set icon to stamina potion
                images[0].sprite = staminaPotionIcon;
            }
            /*
            else if(item.itemName == "Sword")
            {
                TextMeshProUGUI[] texts = slot.GetComponentsInChildren<TextMeshProUGUI>();
                //texts[0].text = item.itemName;
            }
            else if(item.itemName == "Shield")
            {
                TextMeshProUGUI[] texts = slot.GetComponentsInChildren<TextMeshProUGUI>();
                //texts[0].text = item.itemName;
            }
            else if(item.itemName == "Bow")
            {
                TextMeshProUGUI[] texts = slot.GetComponentsInChildren<TextMeshProUGUI>();
                //texts[0].text = item.itemName;
            }
            else if(item.itemName == "Arrow")
            {
                TextMeshProUGUI[] texts = slot.GetComponentsInChildren<TextMeshProUGUI>();
                //texts[0].text = item.itemName;
            }
            else if(item.itemName == "Staff")
            {
                TextMeshProUGUI[] texts = slot.GetComponentsInChildren<TextMeshProUGUI>();
                //texts[0].text = item.itemName;
            }
            else if(item.itemName == "Wand")
            {
                TextMeshProUGUI[] texts = slot.GetComponentsInChildren<TextMeshProUGUI>();
                //texts[0].text = item.itemName;
            }*/
            else
            {
                Debug.LogError("Item not found");
            }
            

            //set icon beased on item 
            

            
    
            Button itemButton = slot.GetComponent<Button>();
            if (itemButton != null)
            {
                itemButton.onClick.AddListener(() => UseItem(item));
            }
            else
            {
                Debug.LogError("Button component not found in itemSlotPrefab");
            }
        }
    }
    

    public void UseItem(ItemObject item)
    {
        Debug.Log("Using item: " + item.itemName);
        // Define what happens when an item is used
        //if (item.itemName == "Health Potion")
        //destroy item
        if(item.itemName == "Health Potion")
        {
            //heal player
            //player.AddHealth(10);
            inventory.AddHealth(10);
            Debug.Log("Player health increased by 10");
        }
        inventory.RemoveItem(item);
        UpdateUI();

    }
}
