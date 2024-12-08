using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10; // Amount of damage the player takes
    [SerializeField] private PlayerClassManager playerClassManager; // Reference to the PlayerClassManager

    // Called when another collider enters the trigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters the damage zone
        if (collision.CompareTag("Player"))
        {
            // Apply damage to the player
            if (playerClassManager != null)
            {
                playerClassManager.TakeDamage(damageAmount);
            }
            else
            {
                Debug.LogWarning("PlayerClassManager is not assigned.");
            }
        }
    }
}
