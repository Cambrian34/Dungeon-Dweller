using UnityEngine;

public class InteractionZone : MonoBehaviour
{
    [SerializeField] private EnemyTracker enemyTracker;

    private void Awake()
    {
        if (enemyTracker == null)
        {
            enemyTracker = GameObject.FindGameObjectWithTag("enemytracker")
                ?.GetComponent<EnemyTracker>();

            if (enemyTracker == null)
            {
                Debug.LogError("EnemyTracker not found or missing 'enemytracker' tag!");
            }
        }
    }

    public bool CanInteract()
    {
        // Check if a player is in the zone and all enemies are defeated
        Collider2D[] colliders = Physics2D.OverlapBoxAll(
            transform.position, 
            GetComponent<BoxCollider2D>().size, 
            0f
        );

        bool playerInZone = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player")) // Ensure "Player" tag is set
            {
                playerInZone = true;
                break;
            }
        }

        return playerInZone && enemyTracker != null && enemyTracker.enemies.Count == 0;
    }

    private void OnDrawGizmos()
    {
        // Optional: Visualize the interaction zone in the scene view
        Gizmos.color = Color.green;
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            Gizmos.DrawWireCube(transform.position, boxCollider.size);
        }
    }
}
