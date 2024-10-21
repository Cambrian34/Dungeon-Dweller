using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    // Two doors in the scene used to transition to the next scene
    public GameObject door;
    public GameObject door2;

    // Target positions for door movement
    private float door1originPosX ;
    private float door2originPosX;

    [SerializeField] private float transitionspeed = 0.05f;

    // Flag to check if transition has started
    private bool isTransitioning = false;
    private  bool starttransition = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize if needed
        door1originPosX = door.transform.position.x;
        door2originPosX = door2.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.F)){
            Debug.Log("F key was pressed");
            starttransition = true;
        }
        // Check if the "F" key is pressed and transition hasn't started yet
        if (starttransition && !isTransitioning)
        {
            //log the key press
            
            // Start the transition
            Transition();

            // Delay the second transition (closing the doors) by 4 seconds
            //Invoke("Transition2", 4.0f);

            // Set flag to prevent multiple transitions
            
        }
        if (!starttransition && !isTransitioning){
            Transition2();
        }
    }

    // Method to transition to the next scene (open doors)
    public void Transition()
    {
        //move door one slowly to the right until x position is 2.64
        if (door.transform.position.x <= 29)
        {
            door.transform.position = new Vector3(door.transform.position.x + transitionspeed*Time.deltaTime, door.transform.position.y, door.transform.position.z);
        }
        //move door two slowly to the left until x position is 2.64
        if (door2.transform.position.x >= 25)
        {
            door2.transform.position = new Vector3(door2.transform.position.x -  transitionspeed*Time.deltaTime, door2.transform.position.y, door2.transform.position.z);
        }
        if (door.transform.position.x >= 29 && door2.transform.position.x <= 25){
            starttransition = false;
        }
    }

    // Method to transition back (close the doors)
    public void Transition2()
    {
        // Move door one back to the left
        if (door.transform.position.x >= door1originPosX)
        {
            door.transform.position = new Vector3(door.transform.position.x - transitionspeed*Time.deltaTime, door.transform.position.y, door.transform.position.z);
        }

        // Move door two back to the right
        if (door2.transform.position.x <= door2originPosX)
        {
            door2.transform.position = new Vector3(door2.transform.position.x + transitionspeed*Time.deltaTime, door2.transform.position.y, door2.transform.position.z);
        }
    }
}

