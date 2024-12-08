using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyTracker : MonoBehaviour
{
    [SerializeField] private GameObject npcMerchantPrefab; // Merchant NPC prefab
    public List<enemyAIManager> enemies = new List<enemyAIManager>(); // List to track all enemies
    private Vector3 lastEnemyPosition; // Stores the last enemy's position

    [SerializeField] int enemyCount = 0;//used to check if enemies are being added to the list 

    // Method to add an enemy to the tracker
    public void RegisterEnemy(enemyAIManager enemy)
    {
        if (!enemies.Contains(enemy))
        {
            enemies.Add(enemy);
            enemyCount++;
            enemy.OnDeath += HandleEnemyDeath;
        }
    }

    // Handle enemy death
    private void HandleEnemyDeath(enemyAIManager enemy)
    {
        lastEnemyPosition = enemy.transform.position; // Store the last enemy's position
        enemies.Remove(enemy); // Remove the enemy from the list

        // Check if all enemies are dead
        if (enemies.Count == 0)
        {
            SpawnMerchant();
        }
    }

    // Spawn NPC merchant at the last enemy's position
    private void SpawnMerchant()
    {
        npcMerchantPrefab.transform.position = lastEnemyPosition;

    }
}
