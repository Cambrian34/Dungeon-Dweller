using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] enemyAIManager enemyai;

    [SerializeField] GameObject enemyprefab;
    [SerializeField] GameObject player;
    [SerializeField] string currentStateString;
    [Header("Config")]
    [SerializeField] float sightDistance = 10;
    [SerializeField] private float attackDelay = 0.5f;

    delegate void AIState();
    AIState currentState;

    //trackers==================================================
    float stateTime = 0;
    bool justChangedState = false;
    private Vector3 lastTargetPos;
    
    private Vector3 startPosition;

    // Start is called before the first frame update
     void Start()
    {
        ChangeState(IdleState);

        //set original position
        lastTargetPos = player.transform.position;
        startPosition = enemyai.transform.position;
    }

    bool CanSeeTarget(){
        return Vector3.Distance(enemyai.transform.position, player.transform.position) < sightDistance;
    }

    private void IdleState()
    {
        if (stateTime == 0)
        {
            currentStateString = "IdleState";
        }
        if (CanSeeTarget())
        {
            ChangeState(AttackState);
            //return;
        }
    }

    void AttackState(){
        enemyai.MoveToward(player.transform.position);
        //stop 2 units away from player and shoot
        
        //enemy.AimShip(targetShip.transform);
        if (stateTime == 0)
        {
            currentStateString = "AttackState";
        }
        if (Vector3.Distance(enemyai.transform.position, player.transform.position) < 2){
            enemyai.StopMoving();
        }

        if (stateTime > attackDelay)
        {
            enemyai.LaunchFireball();
            stateTime = 0; // Reset stateTime after shooting to create a delay
        }

        


        if(!CanSeeTarget())
        {
            lastTargetPos = player.transform.position;
            ChangeState(ReturnToOriginalPositionState);
        }
    }

    private void GetBackToTargetState()
    {
         if (stateTime == 0)
        {
            currentStateString = "BackToTargetState";
        }

        enemyai.MoveToward(lastTargetPos);

        if (Vector3.Distance(enemyai.transform.position, lastTargetPos) < 0.5f){
            ChangeState(IdleState);
        }
    }

    private void ReturnToOriginalPositionState()
    {
        if (stateTime == 0)
        {
            currentStateString = "ReturnToOriginalPositionState";
        }

        // Move back toward the original position
        enemyai.MoveToward(startPosition);

        // If reached original position, go back to idle
        if (Vector3.Distance(enemyai.transform.position, startPosition) < 0.5f)
        {
            ChangeState(IdleState);
        }
    }


    void ChangeState(AIState newAIState){
        currentState = newAIState;
        justChangedState = true;
        stateTime = 0;
    }
    void AITick(){
        if (currentState == null) return;

        if (justChangedState)
        {
            stateTime = 0;
            justChangedState = false;
        }

        currentState?.Invoke();
        stateTime += Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        AITick();
    }
}