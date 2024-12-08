using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class PlayerClassManager : MonoBehaviour
{
    public PlayerClass playerClass;

    [SerializeField] PlayerClass defaultClass;
    [SerializeField] Weapon defaultWeapon;
    private GameObject currentWeapon;
    [Header("Health")]
    public int health=100;
    private HealthSystem healthSystem;

    public Slider healthSlider;
    public Image healthBarImage;

    private Weapon equippedWeapon;
    [Header("Gold")]
    public  int Gold = 0;
    internal int Stamina;
    internal int Mana;

    void Start()
    {
        // Load the selected class and weapon from PlayerData
        playerClass = PlayerSaveData.selectedClass;
        equippedWeapon = PlayerSaveData.selectedWeapon;

        healthSystem = new HealthSystem(health);
        healthSlider.maxValue = health;
        healthSlider.value = healthSystem.CurrentHealth;

        if (playerClass != null && equippedWeapon != null)
        {
            // Initialize the player with the selected class and weapon
            Debug.Log("Player Class: " + playerClass.className + ", Weapon: " + equippedWeapon.weaponName);
            //EquipWeapon(equippedWeapon.weaponPrefab);
            //set health
            health = playerClass.maxHealth;
        }
        else
        {
            Debug.LogError("No class or weapon selected!");
            //set default class and weapon
            playerClass = defaultClass;
            equippedWeapon = defaultWeapon;
            //EquipWeapon(equippedWeapon.weaponPrefab);
            //set health
            playerClass.maxHealth = 100;
            health = playerClass.maxHealth;
        }
    }



    
     public void ActivateClassAbility()
    {
        playerClass.UseSpecialAbility();  // Call the class-specific ability
    }

    public void TakeDamage(int damageAmount)
    {
        healthSystem.TakeDamage(damageAmount);
        //healthSlider.value = healthSystem.CurrentHealth;
         UpdateHealthBar();

        if (healthSystem.IsDead())
        {
            Die();
        }
    }
    //transform image size based on health
    private void UpdateHealthBar()
    {
        if (healthBarImage != null)
        {
            // Update health bar image's fill amount based on current health
            healthBarImage.fillAmount = (float)healthSystem.CurrentHealth / healthSystem.MaxHealth;
        }
    }

    public void Heal(int healAmount)
    {
        healthSystem.Heal(healAmount);
        //healthSlider.value = healthSystem.CurrentHealth;
         UpdateHealthBar();
    }

    private void Die()
    {
        Debug.Log("Player is dead.");
        // Handle player death, like triggering animations or reloading the scene

        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    //collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        //if with fuel add points
        if(collision.CompareTag("Gold")){
            //destroy fuel
            Destroy(collision.gameObject);
            //add points
            Gold += 100;
            //play sound
            //audioSource.Play();         //stop sound
        }
        //if hit by enemy projectile
        if(collision.CompareTag("enemy projectile")){
            //destroy projectile
            Destroy(collision.gameObject);
            //go to main menu
            //changescene sceneChanger = gameObject.AddComponent<changescene>();
            //sceneChanger.changeToMainMenu();
            //reduce health
            //healthSystem.Damage(10);
            TakeDamage(10);
        }
        //touches damage zone
        if(collision.CompareTag("damage zone")){
            
            //go to main menu
            changescene sceneChanger = gameObject.AddComponent<changescene>();
            sceneChanger.changeToMainMenu();
        }
    }


    //get health
    public int GetHealth()
    {
        return healthSystem.CurrentHealth;
    }

    //add hp
    public void AddHealth(int health)
    {
        healthSystem.Heal(health);
        healthSlider.value = healthSystem.CurrentHealth;
    }

    //damage player
    public void Damage(int damage)
    {
        healthSystem.TakeDamage(damage);
        healthSlider.value = healthSystem.CurrentHealth;
    }

    internal void AddStamina(int v)
    {
        throw new NotImplementedException();
    }

    internal void AddMana(int v)
    {
        throw new NotImplementedException();
    }
}
