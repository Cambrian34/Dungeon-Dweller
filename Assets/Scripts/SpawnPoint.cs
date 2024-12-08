using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public GameObject[] objectsToSpawn;

    void Start()
{
    // Check if objectsToSpawn is null or empty
    if (objectsToSpawn == null || objectsToSpawn.Length == 0)
    {
        Debug.LogWarning("objectsToSpawn is not assigned or empty. Skipping spawn logic.");
        return; // Exit the method to prevent further errors
    }

    int index = Time.frameCount % objectsToSpawn.Length; // Cycles through indices based on frame count
    Instantiate(objectsToSpawn[index], transform.position, Quaternion.identity);



    }

}
