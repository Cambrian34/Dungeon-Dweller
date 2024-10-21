using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playercontrol : MonoBehaviour
{
    public float speed = 10.0f;

    [Header("Tools")]
    [SerializeField] pjlauncher projectile;


    [Header("Score")]
    public  int score = 0;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;


    // Update is called once per frame
    void Update()
    {
        //move left
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        //move right
         if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        //move up
         if (Input.GetKey(KeyCode.W))
        {
            MoveUp();
        }
        //move down
         if (Input.GetKey(KeyCode.S))
        {
            MoveDown();
        }
        //launch projectile
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchProjectile();
        }

        
    }
    //move left
    public void MoveLeft()
    {
        //transform.position += new Vector3(-1, 0, 0);
        GetComponent<Transform>().localPosition += new Vector3(-(speed*Time.deltaTime), 0, 0);
        }

    //move right
    public void MoveRight()
    {
        //transform.position += new Vector3(1, 0, 0);
        GetComponent<Transform>().localPosition += new Vector3(speed*Time.deltaTime, 0, 0);
           }

    //move up
    public void MoveUp()
    {
        //transform.position += new Vector3(0, 1, 0);
        GetComponent<Transform>().localPosition += new Vector3(0, speed*Time.deltaTime, 0);

        
    }

    //move down
    public void MoveDown()
    {
        //transform.position += new Vector3(0, -1, 0);
        GetComponent<Transform>().localPosition += new Vector3(0, -(speed*Time.deltaTime), 0);
    }
    //launch projectile
    public void LaunchProjectile()
    {
        projectile.PlayerLauncher();
    }

    //collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        //check if enemy is hit by player projectile
        if(collision.CompareTag("pj enemy")){
            
            //destroy enemy
            Destroy(gameObject);
            //destroy projectile
            Destroy(collision.gameObject);
            //go to main menu
            changescene sceneChanger = gameObject.AddComponent<changescene>();
            sceneChanger.changeToMainMenu();
            
        }

        //if with fuel add points
        if(collision.CompareTag("Fuel")){
            //destroy fuel
            Destroy(collision.gameObject);
            //add points
            score += 100;
            //play sound
            audioSource.Play();
            //stop sound

        }
    

        //print("enemy hit");
    }


}
