using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class procedralvl : MonoBehaviour
{
    [Header("Level Parameters")]
    public int levelWidth = 32;  // Width of the level
    public int levelHeight = 12; // Height of the level

    [Header("Prefabs")]
    public GameObject platformPrefab;
    public GameObject coinPrefab;
    public GameObject enemyPrefab;
    public GameObject lavaPrefab;
    public GameObject endPrefab;

    private char[,] levelData;

   void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        levelData = new char[levelWidth, levelHeight];

        // Initialize the level with empty spaces
        for (int y = 0; y < levelHeight; y++)
        {
            for (int x = 0; x < levelWidth; x++)
            {
                levelData[x, y] = '0';  // '0' means empty space
            }
        }

        // Generate continuous platforms
        PlaceContinuousPlatforms();

        // Randomly place objects (coins, enemies, etc.)
        PlaceCoins();
        PlaceEnemies();
        PlaceLava();
        PlaceEndPoints();

        // Create the level based on levelData
        CreateLevelObjects();
    }

    void PlaceContinuousPlatforms()
    {
        // Create platforms touching each other without gaps
        for (int y = 0; y < levelHeight; y++)
        {
            for (int x = 0; x < levelWidth; x++)
            {
                if (y == levelHeight - 1 || Random.Range(0f, 1f) < 0.2f) // Ensure platforms touch the ground
                {
                    if (!IsOccupied(x, y, 2, 1)) // Check if 2x1 platform can fit
                    {
                        levelData[x, y] = '1';  // Platform
                    }
                }
            }
        }
    }

    void PlaceCoins()
    {
        for (int y = 0; y < levelHeight; y++)
        {
            for (int x = 0; x < levelWidth; x++)
            {
                if (levelData[x, y] == '1' && Random.Range(0f, 1f) < 0.15f)  // Only place coins on platforms
                {
                    if (!IsOccupied(x, y, 1, 1)) // Check if single coin can fit
                    {
                        levelData[x, y] = '5';  // Coin
                    }
                }
            }
        }
    }

    void PlaceEnemies()
    {
        for (int y = 0; y < levelHeight; y++)
        {
            for (int x = 0; x < levelWidth; x++)
            {
                if (levelData[x, y] == '1' && Random.Range(0f, 1f) < 0.1f)  // Only place enemies on platforms
                {
                    if (!IsOccupied(x, y, 1, 1)) // Check if single enemy can fit
                    {
                        levelData[x, y] = '3';  // Enemy
                    }
                }
            }
        }
    }

    void PlaceLava()
    {
        for (int y = 0; y < levelHeight; y++)
        {
            for (int x = 0; x < levelWidth; x++)
            {
                if (Random.Range(0f, 1f) < 0.05f)  // 5% chance to place lava (lower density)
                {
                    if (!IsOccupied(x, y, 1, 1)) // Check if lava can fit
                    {
                        levelData[x, y] = '6';  // Lava
                    }
                }
            }
        }
    }

    void PlaceEndPoints()
    {
        // Place end point at a random location (e.g., near the bottom-right corner)
        int x = Random.Range(levelWidth - 10, levelWidth - 1);
        int y = Random.Range(levelHeight - 4, levelHeight - 1);
        if (!IsOccupied(x, y, 1, 1)) // Ensure the end point does not overlap
        {
            levelData[x, y] = '2';  // End point
        }
    }

    void CreateLevelObjects()
    {
        float objectSpacing = 2.0f; // Spacing between objects

        for (int y = 0; y < levelHeight; y++)
        {
            for (int x = 0; x < levelWidth; x++)
            {
                Vector3 position = new Vector3(x * objectSpacing, y * objectSpacing, 0); // Position of the object

                switch (levelData[x, y])
                {
                    case '1':
                        Instantiate(platformPrefab, position, Quaternion.identity);
                        break;

                    case '5':
                        Instantiate(coinPrefab, position, Quaternion.identity);
                        break;

                    case '3':
                        Instantiate(enemyPrefab, position, Quaternion.identity);
                        break;

                    case '6':
                        Instantiate(lavaPrefab, position, Quaternion.identity);
                        break;

                    case '2':
                        Instantiate(endPrefab, position, Quaternion.identity);
                        break;
                }
            }
        }
    }

    // Collision detection method to check if a grid position is already occupied
    bool IsOccupied(int x, int y, int width, int height)
    {
        // Loop through the area that the object would occupy
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int checkX = x + i;
                int checkY = y + j;

                // Ensure the position is within bounds and check if it's occupied
                if (checkX >= 0 && checkX < levelWidth && checkY >= 0 && checkY < levelHeight)
                {
                    if (levelData[checkX, checkY] != '0') // If any position is occupied, return true
                    {
                        return true;
                    }
                }
                else
                {
                    // Out of bounds, treat as occupied
                    return true;
                }
            }
        }
        return false;  // If no collision, return false
    }
}

/*

public class procedralvl : MonoBehaviour
{
    [Header("Level Parameters")]
    public int levelWidth = 32;  // Width of the level
    public int levelHeight = 12; // Height of the level

    [Header("Prefabs")]
    public GameObject platformPrefab;
    public GameObject coinPrefab;
    public GameObject enemyPrefab;
    public GameObject lavaPrefab;
    public GameObject endPrefab;

    private char[,] levelData;

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        levelData = new char[levelWidth, levelHeight];

        // Initialize the level with empty spaces
        for (int y = 0; y < levelHeight; y++)
        {
            for (int x = 0; x < levelWidth; x++)
            {
                levelData[x, y] = '0';  // '0' means empty space
            }
        }

        // Randomly place objects (platforms, coins, enemies, etc.)
        PlacePlatforms();
        PlaceCoins();
        PlaceEnemies();
        PlaceLava();
        PlaceEndPoints();

        // Create the level based on levelData
        CreateLevelObjects();
    }

    void PlacePlatforms()
    {
        for (int y = 0; y < levelHeight; y++)
        {
            for (int x = 0; x < levelWidth; x++)
            {
                if (Random.Range(0f, 1f) < 0.1f)  // 10% chance to place a platform
                {
                    levelData[x, y] = '1';  // Platform
                }
            }
        }
    }

    void PlaceCoins()
    {
        for (int y = 0; y < levelHeight; y++)
        {
            for (int x = 0; x < levelWidth; x++)
            {
                if (Random.Range(0f, 1f) < 0.05f)  // 5% chance to place a coin
                {
                    levelData[x, y] = '5';  // Coin
                }
            }
        }
    }

    void PlaceEnemies()
    {
        for (int y = 0; y < levelHeight; y++)
        {
            for (int x = 0; x < levelWidth; x++)
            {
                if (Random.Range(0f, 1f) < 0.05f)  // 5% chance to place an enemy
                {
                    levelData[x, y] = '3';  // Enemy
                }
            }
        }
    }

    void PlaceLava()
    {
        for (int y = 0; y < levelHeight; y++)
        {
            for (int x = 0; x < levelWidth; x++)
            {
                if (Random.Range(0f, 1f) < 0.05f)  // 5% chance to place lava
                {
                    levelData[x, y] = '6';  // Lava
                }
            }
        }
    }

    void PlaceEndPoints()
    {
        // Place end point at random location (e.g., near the bottom-right corner)
        int x = Random.Range(levelWidth - 10, levelWidth - 1);
        int y = Random.Range(levelHeight - 4, levelHeight - 1);
        levelData[x, y] = '2';  // End point
    }

    void CreateLevelObjects()
    {
        for (int y = 0; y < levelHeight; y++)
        {
            for (int x = 0; x < levelWidth; x++)
            {
                Vector3 position = new Vector3(x * 60, y * 60, 0); // Spacing objects 60 units apart

                switch (levelData[x, y])
                {
                    case '1':
                        Instantiate(platformPrefab, position, Quaternion.identity);
                        break;

                    case '5':
                        Instantiate(coinPrefab, position, Quaternion.identity);
                        break;

                    case '3':
                        Instantiate(enemyPrefab, position, Quaternion.identity);
                        break;

                    case '6':
                        Instantiate(lavaPrefab, position, Quaternion.identity);
                        break;

                    case '2':
                        Instantiate(endPrefab, position, Quaternion.identity);
                        break;
                }
            }
        }
    }
}

*/