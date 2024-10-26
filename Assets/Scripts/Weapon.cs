using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;         // Weapon Name
    public int damage;                // Weapon's damage
    public float attackSpeed;         // Speed at which the weapon attacks
    public GameObject weaponPrefab;   // The prefab of the weapon
    public bool hasSpecialAbility;    // Does this weapon have a special ability?
    public string specialAbilityName; // Special ability (e.g., "Fire Damage")

    //special ability damage
    public int specialAbilityDamage;
    
    

    public void UseSpecialAbility()
    {
        if (hasSpecialAbility)
        {
            // Logic for using special ability, e.g., apply fire damage
            Debug.Log("Using special ability: " + specialAbilityName);
        }
    }


}
