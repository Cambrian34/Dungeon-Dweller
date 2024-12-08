using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class lvlgenv3 : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms; // index 0 --> closed, index 1 --> LR, index 2 --> LRB, index 3 --> LRT, index 4 --> LRBT
    //Ten total
    //ll rooms: left opening, right opening, top opening, bottom opening, left right opening, closed, left right top opening, left right bottom opening, left right top bottom opening
    private int direction;
    private bool lvlcomplete = false;
    private bool stopGeneration;
    private int downCounter;
    private int roomCount;

    public float moveIncrement;
    public int maxRooms = 100; // Maximum number of rooms to generate
    public float startTimeBtwSpawn;

    private float timeBtwSpawn;

    [SerializeField] GameObject player;
    public LayerMask whatIsRoom;

    [Header("Prefabs")]
    public GameObject coinPrefab;        // Coin prefab to spawn
    public GameObject enemyPrefab;       // Enemy prefab to spawn

    private List<Vector2> roomPositions = new List<Vector2>(); // Track all room positions to prevent overlap

    private void Start()
    {
        // Randomly select a starting position for the room
        int randStartingPos = Random.Range(0, startingPositions.Length);
        Vector3 roomCenter = startingPositions[randStartingPos].position; // Use starting position for room

        // Move the lvlgenv3 object to the chosen starting position (room's position)
        transform.position = roomCenter;

        // Instantiate the first room at the starting position
        Instantiate(rooms[1], roomCenter, Quaternion.identity);
        roomPositions.Add(roomCenter); // Store the first room position

        // Move player to the starting position (center of the first room)
        player.transform.position = roomCenter;

        direction = Random.Range(1, 6);
        timeBtwSpawn = startTimeBtwSpawn;
        roomCount = 1;
    }

    private void Update()
    {
        if (lvlcomplete)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (timeBtwSpawn <= 0 && !stopGeneration)
        {
            Move();
            timeBtwSpawn = startTimeBtwSpawn;
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }

    private void Move()
    {
        if (stopGeneration) return;

        if (roomCount >= maxRooms)
        {
            Debug.Log("Max room count:" + roomCount);
            stopGeneration = true;
            Debug.Log("Generation Complete: Reached max room count.");
            return;
        }

        Vector2 targetPosition = transform.position;

        if (direction == 1 || direction == 2)
        { // Move right
            if (transform.position.x < 90)
            {
                downCounter = 0;
                targetPosition = new Vector2(transform.position.x + moveIncrement, transform.position.y);
                transform.position = targetPosition;

                if (!RoomOverlap(targetPosition)) // Check if room overlaps
                {
                    SpawnRoom(targetPosition);
                }

                direction = Random.Range(1, 6);

                if (direction == 3) direction = 1;
                if (direction == 4) direction = 5;
            }
            else
            {
                direction = 5; // Switch to moving down if hitting the boundary
            }
        }
        else if (direction == 3 || direction == 4)
        { // Move left
            if (transform.position.x > 10)
            {
                downCounter = 0;
                targetPosition = new Vector2(transform.position.x - moveIncrement, transform.position.y);
                transform.position = targetPosition;

                if (!RoomOverlap(targetPosition)) // Check if room overlaps
                {
                    SpawnRoom(targetPosition);
                }

                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5; // Switch to moving down if hitting the boundary
            }
        }
        else if (direction == 5)
        { // Move down
            downCounter++;
            if (transform.position.y > -90)
            {
                HandleRoomBelow();

                targetPosition = new Vector2(transform.position.x, transform.position.y - moveIncrement);
                transform.position = targetPosition;

                if (!RoomOverlap(targetPosition)) // Check if room overlaps
                {
                    SpawnRoom(targetPosition);
                }

                roomCount++;
                direction = Random.Range(1, 6);
            }
            else
            {
                stopGeneration = true;
                Debug.Log("Generation Stopped: Hit lower boundary.");
            }
        }
    }

    private void SpawnRoom(Vector2 position)
    {
        int randRoom = Random.Range(1, 4);
        GameObject room = Instantiate(rooms[randRoom], position, Quaternion.identity);
        roomPositions.Add(position); // Store the new room's position
        roomCount++;

        // After room is created, spawn coins and enemies within it
        SpawnCoinsAndEnemies(room.transform);
    }

    private void SpawnCoinsAndEnemies(Transform roomTransform)
    {
        // Define a range for random positions where coins and enemies can be placed inside the room
        float minX = roomTransform.position.x - 30;
        float maxX = roomTransform.position.x + 30;
        float minY = roomTransform.position.y - 30;
        float maxY = roomTransform.position.y + 30;

        // Randomly place a coin
        Vector2 coinPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        Instantiate(coinPrefab, coinPosition, Quaternion.identity);

        // Randomly place an enemy
        Vector2 enemyPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);
    }

    private bool RoomOverlap(Vector2 position)
    {
        foreach (Vector2 existingPosition in roomPositions)
        {
            if (Vector2.Distance(position, existingPosition) < moveIncrement)
            {
                return true; // Overlap detected
            }
        }
        return false;
    }

    private void HandleRoomBelow()
    {
        Collider2D previousRoom = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);

        if (previousRoom != null && previousRoom.GetComponent<Room>() != null)
        {
            var roomType = previousRoom.GetComponent<Room>().roomType;

            if (roomType != 4 && roomType != 2)
            {
                if (downCounter >= 2)
                {
                    previousRoom.GetComponent<Room>().RoomDestruction();
                    Instantiate(rooms[4], transform.position, Quaternion.identity);
                }
                else
                {
                    previousRoom.GetComponent<Room>().RoomDestruction();
                    int randRoomDownOpening = Random.Range(2, 5);
                    if (randRoomDownOpening == 3) randRoomDownOpening = 2;
                    Instantiate(rooms[randRoomDownOpening], transform.position, Quaternion.identity);
                }
            }
        }
    }
}
