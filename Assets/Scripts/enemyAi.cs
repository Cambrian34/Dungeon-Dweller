using System;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private enemyAIManager enemyai; 
    [SerializeField] private GameObject player;
    [SerializeField] private string currentStateString;

    [Header("Config")]
    [SerializeField] private float sightDistance = 10f;
    [SerializeField] private float attackDelay = 0.5f;
    [SerializeField] private float moveSpeed = 3.0f; // Speed at which enemy moves towards player

    private delegate void AIState();
    private AIState currentState;

    private float stateTime = 0f;
    private bool justChangedState = false;
    private Vector3 lastTargetPosition;
    private Vector3 startPosition;

    private Rigidbody2D rb;

    private void Start()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (!player)
            {
                Debug.LogError("Player object not found. Please assign it in the inspector or ensure it has the 'Player' tag.");
                return;
            }
        }

        if (!enemyai)
        {
            Debug.LogError("EnemyAIManager not assigned. Please assign it in the inspector.");
            return;
        }

        // Get the Rigidbody2D component for physics-based movement
        rb = GetComponent<Rigidbody2D>();

        // Set initial positions
        lastTargetPosition = player.transform.position;
        startPosition = transform.position;

        // Ensure enemy gravity is enabled to prevent floating
        if (rb != null)
        {
            rb.gravityScale = 1f;  // Make sure gravity is affecting the enemy
        }

        ChangeState(IdleState);
    }

    private bool CanSeeTarget()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= sightDistance;
    }

    private void IdleState()
    {
        if (stateTime == 0)
        {
            currentStateString = "IdleState";
        }

        // Rotate to face the player when in sight
        if (CanSeeTarget())
        {
            ChangeState(AttackState);
        }
    }

    private void AttackState()
    {
        if (stateTime == 0)
        {
            currentStateString = "AttackState";
        }

        // Rotate to face the player
        FacePlayer();

        // Move towards the player
        Vector3 playerPosition = player.transform.position;
        enemyai.MoveToward(playerPosition);

        // Stop 2 units away from the player and attack
        if (Vector3.Distance(transform.position, playerPosition) <= 2f)
        {
            enemyai.StopMoving();

            if (stateTime > attackDelay)
            {
                enemyai.LaunchFireball();
                stateTime = 0f; // Reset state time to create attack delay
            }
        }

        // Transition to ReturnToOriginalPositionState if the player is out of sight
        if (!CanSeeTarget())
        {
            lastTargetPosition = playerPosition;
            ChangeState(ReturnToOriginalPositionState);
        }
    }

    private void GetBackToTargetState()
    {
        if (stateTime == 0)
        {
            currentStateString = "BackToTargetState";
        }

        enemyai.MoveToward(lastTargetPosition);

        if (Vector3.Distance(transform.position, lastTargetPosition) < 0.5f)
        {
            ChangeState(IdleState);
        }
    }

    private void ReturnToOriginalPositionState()
    {
        if (stateTime == 0)
        {
            currentStateString = "ReturnToOriginalPositionState";
        }

        enemyai.MoveToward(startPosition);

        if (Vector3.Distance(transform.position, startPosition) < 0.5f)
        {
            ChangeState(IdleState);
        }
    }

    private void ChangeState(AIState newAIState)
    {
        currentState = newAIState;
        justChangedState = true;
        stateTime = 0f;
    }

    private void AITick()
    {
        if (currentState == null) return;

        if (justChangedState)
        {
            stateTime = 0f;
            justChangedState = false;
        }

        currentState?.Invoke();
        stateTime += Time.deltaTime;
    }

    private void FacePlayer()
    {
        if (player.transform.position.x > transform.position.x)
        {
            // Rotate to face right
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            // Rotate to face left
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public string GetCurrentState()
    {
        return currentStateString;
    }

    private void Update()
    {
        AITick();
    }
}
