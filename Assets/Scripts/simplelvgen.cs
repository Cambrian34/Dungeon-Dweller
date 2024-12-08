using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Platform Spawner")]
    public GameObject platformPrefab;          // Platform prefab to spawn
    public int numberOfPlatforms = 10;         // Number of platforms to generate
    public float minXDistance = 1.5f;          // Horizontal distance range between platforms
    public float maxXDistance = 3f;
    public float minYDistance = 0.5f;          // Vertical distance range between platforms
    public float maxYDistance = 2.5f;
    public Vector2 startPosition = new Vector2(0, 0); // Starting point for level generation
    public float platformWidth = 1.0f;         // Platform width for consistent spacing

    [Header("Enemy Spawner")]
    [SerializeField] private GameObject enemyPrefab;   // Enemy prefab to spawn
    [SerializeField] private int numberOfEnemies = 5;  // Total number of enemies to spawn
    private int enemiesSpawned = 0;

    [Header("Gold Spawner")]
    [SerializeField] private GameObject goldPrefab;    // Gold prefab to spawn
    [SerializeField] private int numberOfGold = 5;     // Total number of gold to spawn
    private int goldSpawned = 0;

    private void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        Vector2 spawnPosition = startPosition;

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            // Instantiate platform
            GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

            // Try to spawn enemy on platform
            if (enemiesSpawned < numberOfEnemies && Random.value > 0.5f)
            {
                SpawnEnemyOnPlatform(platform.transform);
                enemiesSpawned++;
            }

            // Try to spawn gold on platform
            if (goldSpawned < numberOfGold && Random.value > 0.5f)
            {
                SpawnGoldOnPlatform(platform.transform);
                goldSpawned++;
            }

            // Calculate the next platform position
            float xOffset = Random.Range(minXDistance, maxXDistance) + platformWidth;
            float yOffset = Random.Range(minYDistance, maxYDistance);
            yOffset = Random.value > 0.5f ? yOffset : -yOffset; // Randomly decide up or down

            spawnPosition += new Vector2(xOffset, yOffset);
        }
    }

    void SpawnEnemyOnPlatform(Transform platformTransform)
    {
        // Set enemy spawn position on the platform with a small random offset
        Vector2 enemyPosition = new Vector2(
            platformTransform.position.x + Random.Range(-platformWidth / 2, platformWidth / 2),
            platformTransform.position.y + 0.5f // Position slightly above platform
        );
        Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);
    }

    void SpawnGoldOnPlatform(Transform platformTransform)
    {
        // Set gold spawn position on the platform with a small random offset
        Vector2 goldPosition = new Vector2(
            platformTransform.position.x + Random.Range(-platformWidth / 2, platformWidth / 2),
            platformTransform.position.y + 0.5f // Position slightly above platform
        );
        Instantiate(goldPrefab, goldPosition, Quaternion.identity);
    }
}
