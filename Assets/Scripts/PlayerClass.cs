using UnityEngine;

[CreateAssetMenu(fileName = "NewClass", menuName = "Player Class")]

/**
    * PlayerClass is a ScriptableObject that defines the properties of a player class.
    * This includes the class name, maximum health, attack power, movement speed, default weapon, and special ability.
    * The class also contains a method to use the special ability.
    *methods for setting classes for mage, warrior and archer
    * UseSpecialAbility() method can be overridden in derived classes to implement class-specific abilities.
    * 
    *
    */
public class PlayerClass : ScriptableObject
{
   public string className;   // Class Name (e.g., Warrior)
    public int maxHealth;      // Maximum Health
    public float attackPower;  // Basic attack power
    public float speed;        // Movement speed
    public GameObject defaultWeaponPrefab; // The default weapon assigned to the class
    public Sprite classIcon;   // Optional: Class Icon for UI representation

    // Class-specific abilities can be added here
    public string specialAbilityName;
    public float specialAbilityCooldown;
    
    public void UseSpecialAbility()
    {
        // Add logic for special ability (e.g., Warrior's shield, Mage's fireball)
        Debug.Log("Using special ability: " + specialAbilityName);
        if (specialAbilityName == "Fireball")
        {
            // Logic for fireball ability (e.g., instantiate fireball projectile)
            Debug.Log("Casting Fireball!");
        }
        else if (specialAbilityName == "Shield")
        {
            // Logic for shield (e.g., reduce damage taken)
            Debug.Log("Activating Shield!");
        }
        
    }

    //set classes for mage, warrior and archer
    public void Mage()
    {
        className = "Mage";
        maxHealth = 100;
        attackPower = 10;
        speed = 5;
        defaultWeaponPrefab = null;
        classIcon = null;
        specialAbilityName = "Fireball";
        specialAbilityCooldown = 10;
    }

    public void Warrior()
    {
        className = "Warrior";
        maxHealth = 150;
        attackPower = 15;
        speed = 3;
        defaultWeaponPrefab = null;
        classIcon = null;
        specialAbilityName = "Shield";
        specialAbilityCooldown = 15;
    }

    public void Archer()
    {
        className = "Archer";
        maxHealth = 120;
        attackPower = 12;
        speed = 7;
        defaultWeaponPrefab = null;
        classIcon = null;
        specialAbilityName = "Arrow Rain";
        specialAbilityCooldown = 12;
    }
    

}
