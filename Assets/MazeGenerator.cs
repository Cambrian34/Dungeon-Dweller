using System;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int mazeWidth = 10;
    public int mazeHeight = 10;
    public GameObject wallPrefab;
    public GameObject pathPrefab;

    private int[,] maze; 

    private const int N = 1; 
    private const int S = 2; 
    private const int E = 4; 
    private const int W = 8; 

    private readonly int[,] GO_DIR = new int[,]
    {
        { 0, -1 }, // N
        { 0, 1 },  // S
        { 1, 0 },  // E
        { -1, 0 }  // W
    };

    private readonly Dictionary<int, int> REVERSE = new Dictionary<int, int>
    {
        { N, S },
        { S, N },
        { E, W },
        { W, E }
    };

    void Start()
    {
        GenerateMaze();
        BuildMaze();
    }

    void GenerateMaze()
    {
        maze = new int[mazeWidth, mazeHeight];
        for (int x = 0; x < mazeWidth; x++)
            for (int y = 0; y < mazeHeight; y++)
                maze[x, y] = 1;  // Initially, all cells are walls.

        DigPassages(0, 0);  // Start at top-left corner.
    }

    void DigPassages(int x, int y)
    {
        maze[x, y] = 0;  // Mark as path.
        int[] directions = { N, S, E, W };
        ShuffleArray(directions);

        foreach (int direction in directions)
        {
            int newX = x + GO_DIR[Array.IndexOf(new int[] { N, S, E, W }, direction), 0];
            int newY = y + GO_DIR[Array.IndexOf(new int[] { N, S, E, W }, direction), 1];

            if (newX >= 0 && newX < mazeWidth && newY >= 0 && newY < mazeHeight && maze[newX, newY] == 1)
            {
                maze[x, y] |= direction;
                maze[newX, newY] |= REVERSE[direction];
                DigPassages(newX, newY);  // Recursively dig.
            }
        }
    }

    void ShuffleArray(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    void BuildMaze()
    {
        for (int y = 0; y < mazeHeight; y++)
        {
            for (int x = 0; x < mazeWidth; x++)
            {
                Vector3 position = new Vector3(x, 0, y);

                if (maze[x, y] == 0) 
                {
                    Instantiate(pathPrefab, position, Quaternion.identity); 
                }
                else 
                {
                    if ((maze[x, y] & N) == 0) 
                        Instantiate(wallPrefab, position + new Vector3(0, 0, 0.5f), Quaternion.identity);

                    if ((maze[x, y] & S) == 0) 
                        Instantiate(wallPrefab, position + new Vector3(0, 0, -0.5f), Quaternion.identity);

                    if ((maze[x, y] & E) == 0) 
                        Instantiate(wallPrefab, position + new Vector3(0.5f, 0, 0), Quaternion.identity);

                    if ((maze[x, y] & W) == 0) 
                        Instantiate(wallPrefab, position + new Vector3(-0.5f, 0, 0), Quaternion.identity);
                }
            }
        }
    }
}
