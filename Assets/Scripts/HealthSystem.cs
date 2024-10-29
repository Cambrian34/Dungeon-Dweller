using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthSystem
{
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    
    public HealthSystem(int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    // Take damage and update health
    public void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
    }

    // Heal and update health
    public void Heal(int healAmount)
    {
        CurrentHealth += healAmount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
    }

    // Check if health is zero (indicates death)
    public bool IsDead()
    {
        return CurrentHealth <= 0;
    }

    //getter for current health
    public int GetHealth()
    {
        return CurrentHealth;
    }
}

