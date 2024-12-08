using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MerchantNPC : MonoBehaviour
{
    [SerializeField] ShopUIManager shopUI;  // Reference to ShopUIManager script
    [SerializeField] private GameObject shopCanvas;
    private bool isPlayerInRange = false;

    //message to display on screen, maybe using canvas



    private void Start()
    {
        if (shopUI == null)
        {
            Debug.LogWarning("ShopUIManager not assigned to MerchantNPC.");
        }
        else
    {
        Debug.Log("ShopUIManager assigned: " + shopUI);
    }
    }

    private void Update()
    {
        //Tell player to press E to open shop using canvas

        // Open the shop UI when the player presses 'E' in the NPC's range
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            OpenShop();
        }
    }

    private void OpenShop()
    {
        shopUI.OpenShop();
        shopCanvas.SetActive(true);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Press E to open the shop.");

            //popup message on screen


        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            shopUI.CloseShop();  // Automatically closes shop if player leaves range
            Debug.Log("You left the shop area.");
        }
    }

    //next lvl 
}

