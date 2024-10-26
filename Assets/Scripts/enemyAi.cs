using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] enemyai enemyai;

    [SerializeField] GameObject enemyprefab;
    [SerializeField] GameObject player;
    [SerializeField] string currentStateString;
    [Header("Config")]
    [SerializeField] float sightDistance = 10;

    delegate void AIState();
    AIState currentState;

    //trackers==================================================
    float stateTime = 0;
    bool justChangedState = false;
    Vector3 lastTargetPos;

    // Start is called before the first frame update
     void Start()
    {
        ChangeState(IdleState);
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
            return;
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

        if (stateTime > 0.5f){
            enemyai.LaunchFirebal2(); //shoot at player!
             //delay between shots using a 
        

        }
        if(enemyai.GetProjectileLauncher().GetAmmo() == 0){
            enemyai.GetProjectileLauncher().Reload();
        }
        /*
        if(enemy.GetProjectileLauncher().GetAmmo() == 0){
            enemy.GetProjectileLauncher().Reload();
        }
        */


        if(!CanSeeTarget())
        {
            lastTargetPos = player.transform.position;
            ChangeState(GetBackToTargetState);
            return;
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
    currentState();
    stateTime += Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        AITick();
    }
}