using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections.Generic;

public class levelgenerationv2 : MonoBehaviour
{
    [Header("Level Data")]
    public string[] levelData;

    [Header("Prefabs")]
    public GameObject platformPrefab;    // Platform prefab to spawn
    public GameObject coinPrefab;        // Coin prefab to spawn
    public GameObject endPrefab;         // End point prefab to spawn
    public GameObject enemyPrefab;       // Enemy prefab to spawn
    public GameObject lavaPrefab;        // Lava prefab to spawn

    private List<GameObject> platforms = new List<GameObject>();
    private List<GameObject> coins = new List<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> lava = new List<GameObject>();
    private List<GameObject> endPoints = new List<GameObject>();

    private void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int i = 0; i < levelData.Length; i++)
        {
            string line = levelData[i];
            for (int j = 0; j < line.Length; j++)
            {
                // Multiply by a factor (e.g., 60) to create proper spacing for objects
                Vector2 position = new Vector2(j * 60, i * 60);

                switch (line[j])
                {
                    case '0':
                        // Empty space, nothing is placed
                        break;

                    case '1':
                        // Platform (creates a platform at '1' position)
                        CreatePlatform(position);
                        break;

                    case '5':
                        // Coin (creates a coin at '5' position)
                        CreateCoin(position);
                        break;

                    case '2':
                        // End point (creates an end goal at '2' position)
                        CreateEndPoint(position);
                        break;

                    case '6':
                        // Lava (creates lava at '6' position)
                        CreateLava(position);
                        break;

                    case '3':
                        // Enemy (creates an enemy at '3' position)
                        CreateEnemy(position);
                        break;

                    // This was used in development to quickly test levels
                    case '8':
                        // Hidden passage or debug object, not currently used
                        break;
                }
            }
        }
    }

    void CreatePlatform(Vector2 position)
    {
        GameObject platform = Instantiate(platformPrefab, position, Quaternion.identity);
        platforms.Add(platform);
    }

    void CreateCoin(Vector2 position)
    {
        GameObject coin = Instantiate(coinPrefab, position, Quaternion.identity);
        coins.Add(coin);
    }

    void CreateEndPoint(Vector2 position)
    {
        GameObject end = Instantiate(endPrefab, position, Quaternion.identity);
        endPoints.Add(end);
    }

    void CreateEnemy(Vector2 position)
    {
        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        enemies.Add(enemy);
    }

    void CreateLava(Vector2 position)
    {
        GameObject lavaObj = Instantiate(lavaPrefab, position, Quaternion.identity);
        lava.Add(lavaObj);
    }
}

