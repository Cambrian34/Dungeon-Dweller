using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] GameObject elevator;      // Reference to the elevator object
    [SerializeField] Transform origin;        // Starting position of the elevator
    [SerializeField] Transform destination;   // Destination position of the elevator
    [SerializeField] float speed = 1.0f;      // Speed of the elevator's movement

    private bool isPlayerTouching = false;    // Flag to track if the player is touching the elevator
    private bool isMoving = false;            // Tracks whether the elevator is currently moving
    private Vector3 targetPosition;           // The position the elevator is currently moving towards
    private bool movingUp = true;             // Tracks the direction of the elevator's movement

    void Start()
    {
        targetPosition = origin.position;      // Initialize the target position as the origin
    }

    void Update()
    {
        // Check if the player presses the Q key and is touching the elevator
        if (isPlayerTouching && Input.GetKeyDown(KeyCode.Q) && !isMoving)
        {
            ToggleDirection();                // Toggle the movement direction
        }

        // Move the elevator if it is currently moving
        if (isMoving)
        {
            MoveElevator();
        }
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player has entered the elevator's trigger zone
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Press 'Q' to use the elevator.");
            isPlayerTouching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player has exited the elevator's trigger zone
        if (collision.CompareTag("Player"))
        {
            isPlayerTouching = false;
        }
    }
    void ToggleDirection()
{
    movingUp = !movingUp;
    if (movingUp)
    {
        targetPosition = destination.position;
    }
    else
    {
        targetPosition = origin.position;
    }
    isMoving = true;
}

void MoveElevator()
{
    // Move the elevator towards the target position
    transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

    // Check if the elevator has reached the target position
    if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
    {
        ToggleDirection();
    }
}
}