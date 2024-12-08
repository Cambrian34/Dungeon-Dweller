using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    

    private RoomTemplates rooms;
    private int rand;
    public bool spawned = false;
    public float waitTime = 4f;

    void Start()
    {
        Destroy(gameObject, waitTime);
        rooms = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        if (!spawned)
        {
            if (openingDirection == 1)
            {
                // Need to spawn a room with a BOTTOM door.
                rand = Random.Range(0, rooms.bottomRooms.Length);
                Instantiate(rooms.bottomRooms[rand], transform.position, rooms.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == 2)
            {
                // Need to spawn a room with a TOP door.
                rand = Random.Range(0, rooms.topRooms.Length);
                Instantiate(rooms.topRooms[rand], transform.position, rooms.topRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                // Need to spawn a room with a LEFT door.
                rand = Random.Range(0, rooms.leftRooms.Length);
                Instantiate(rooms.leftRooms[rand], transform.position, rooms.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                // Need to spawn a room with a RIGHT door.
                rand = Random.Range(0, rooms.rightRooms.Length);
                Instantiate(rooms.rightRooms[rand], transform.position, rooms.rightRooms[rand].transform.rotation);
            }

            spawned = true; // Mark as spawned
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            // Only instantiate a closed room if no room has been spawned yet
            if (!other.GetComponent<RoomSpawner>().spawned && !spawned)
            {
                Instantiate(rooms.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            spawned = true;
        }
    }
}

public class AddRoom : MonoBehaviour
{
    private RoomTemplates templates;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

        // Add this room to the list only if it's not already in the list
        if (!templates.rooms.Contains(this.gameObject))
        {
            templates.rooms.Add(this.gameObject);
        }
    }
}

public class Destroyer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Ensure we only destroy the intended room or object, not just anything in the trigger
        if (other.CompareTag("Room"))
        {
            Destroy(other.gameObject);
        }
    }
}
