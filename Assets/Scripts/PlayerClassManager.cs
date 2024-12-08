using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerClassManager : MonoBehaviour
{
    public PlayerClass playerClass;

    [SerializeField] PlayerClass defaultClass;
    [SerializeField] Weapon defaultWeapon;
    private GameObject currentWeapon;
    
    [Header("Health")]
    public int health = 100;
    private HealthSystem healthSystem;
    public Slider healthSlider;

    private Weapon equippedWeapon;

    [Header("Gold")]
    public int Gold = 0;
    internal int Stamina;
    internal int Mana;

    void Start()
    {
        // Load the selected class and weapon from PlayerData
        playerClass = PlayerSaveData.selectedClass;
        equippedWeapon = PlayerSaveData.selectedWeapon;

        // Initialize the health system
        healthSystem = new HealthSystem(health);
        healthSlider.maxValue = health;
        healthSlider.value = healthSystem.CurrentHealth;

        if (playerClass != null && equippedWeapon != null)
        {
            // Initialize the player with the selected class and weapon
            Debug.Log("Player Class: " + playerClass.className + ", Weapon: " + equippedWeapon.weaponName);
            // Equip weapon and set health
            playerClass.maxHealth = 100;
            health = playerClass.maxHealth; // Ensure health is set from player class max health
            healthSystem = new HealthSystem(health);
        }
        else
        {
            Debug.LogError("No class or weapon selected!");
            // Set default class and weapon
            playerClass = defaultClass;
            equippedWeapon = defaultWeapon;
            playerClass.maxHealth = 100;
            health = playerClass.maxHealth;
            healthSystem = new HealthSystem(health);
        }
    }

    public void ActivateClassAbility()
    {
        playerClass.UseSpecialAbility(); // Call the class-specific ability
    }

    public void TakeDamage(int damageAmount)
    {
        healthSystem.TakeDamage(damageAmount);
        healthSlider.value = healthSystem.CurrentHealth;
        UpdateHealthBar();

        if (healthSystem.IsDead())
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        healthSystem.Heal(healAmount);
        healthSlider.value = healthSystem.CurrentHealth;
    }

    private void Die()
    {
        Debug.Log("Player is dead.");
        // Handle player death, like triggering animations or reloading the scene

        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If with gold, add points
        if (collision.CompareTag("Gold"))
        {
            Destroy(collision.gameObject); // Destroy gold object
            Gold += 100; // Add points
        }
        // If hit by enemy projectile
        if (collision.CompareTag("enemy projectile"))
        {
            Destroy(collision.gameObject); // Destroy projectile
            TakeDamage(10); // Apply damage
        }
        // Touches damage zone
        if (collision.CompareTag("damage zone"))
        {
            // Go to main menu
            changescene sceneChanger = gameObject.AddComponent<changescene>();
            sceneChanger.changeToMainMenu();
        }

        // If player touches ghost, lose health
        if (collision.CompareTag("ghost"))
        {
            TakeDamage(10); // Apply damage
        }
    }

    // Get health
    public int GetHealth()
    {
        return healthSystem.CurrentHealth;
    }

    // Add health
    public void AddHealth(int healthAmount)
    {
        healthSystem.Heal(healthAmount);
        healthSlider.value = healthSystem.CurrentHealth;
    }

    // Damage player
    public void Damage(int damage)
    {
        healthSystem.TakeDamage(damage);
        healthSlider.value = healthSystem.CurrentHealth;
    }

    // Implementing Stamina and Mana
    internal void AddStamina(int value)
    {
        Stamina += value;
    }

    internal void AddMana(int value)
    {
        Mana += value;
    }

    // Helper method to update health bar
    private void UpdateHealthBar()
    {
        healthSlider.value = healthSystem.CurrentHealth;
    }
}
