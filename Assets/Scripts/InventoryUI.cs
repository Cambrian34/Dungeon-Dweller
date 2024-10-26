using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventorysystem inventory;  // Reference to the player's inventory
    public Transform inventoryPanel;  // UI panel to display items
    public GameObject itemSlotPrefab; // Prefab for each inventory slot

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
        inventory.RemoveItem(item);
        UpdateUI();

    }
}
