using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    //save manager
    SaveSystem saveSystem;

    //check the last direction the player was facing
    private bool facingRight = true;




    // Update is called once per frame
    void Update()
    {
        //move left
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
            facingRight = false;
        }
        //move right
         if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
            facingRight = true;
        }
        //move up
         if (Input.GetKeyDown(KeyCode.W))
        {
            MoveUp();
        }
        /*move down
         if (Input.GetKey(KeyCode.S))
        {
            MoveDown();
        }*/
        //launch projectile
        if (Input.GetKeyDown(KeyCode.Space))
        {   
            try{
            //check player class for projectile
            if (PlayerSaveData.selectedClass.className == "Archer")
                {
                    if (facingRight)
                    {
                        BowAttack(1);
                    }
                    else
                    {
                        BowAttack(-1);
                    }
                }
            else if (PlayerSaveData.selectedClass.className == "Warrior")
                {
                    SwordAttack();
                }
            else
                {
                    //print("Default projectile"); 
                    //print to console
                    Debug.Log("Default projectile");
                    if (facingRight)
                    {
                        LaunchProjectile(1);
                    }
                    else
                    {
                        LaunchProjectile(-1);
                }
            }}
            catch{
                //print("Default projectile"); 
                //print to console
                Debug.Log("Default projectile");
                if (facingRight)
                {
                    LaunchProjectile(1);
                }
                else
                {
                    LaunchProjectile(-1);
                }
               
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            //go to main menu
            changescene sceneChanger = gameObject.AddComponent<changescene>();
            sceneChanger.changeToMainMenu();
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
        GetComponent<Transform>().localPosition += new Vector3(0, 2*speed*Time.deltaTime, 0);

        
    }

    //move down
    public void MoveDown()
    {
        //transform.position += new Vector3(0, -1, 0);
        GetComponent<Transform>().localPosition += new Vector3(0, -(30*Time.deltaTime), 0);
    }
    //launch projectile
    public void LaunchProjectile(int direction)
    {
        projectile.PlayerLauncher(direction);
    }
    public void SwordAttack()
    {   
        //play sound
        //audioSource.Play();
    }
    //bow attack
    public void BowAttack(int direction)
    {
        projectile.LaunchArrow(direction);
        //play sound
        //audioSource.Play();
    }

    //collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        //check if enemy is hit by player projectile
        if(collision.CompareTag("Wall")){
            
            
            //destroy projectile
            Destroy(collision.gameObject);
            /*go to main menu
            changescene sceneChanger = gameObject.AddComponent<changescene>();
            sceneChanger.changeToMainMenu();
            */
        }

        //if with fuel add points
        if(collision.CompareTag("Gold")){
            //destroy fuel
            Destroy(collision.gameObject);
            //add points
            score += 100;
            //play sound
            //audioSource.Play();         //stop sound
//readd sound later
        }
    

        //print("enemy hit");
    }


}
