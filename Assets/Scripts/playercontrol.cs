using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class playercontrol : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float jumpForce = 5.0f;


    [Header("Tools")]
    [SerializeField] pjlauncher projectile;


    [Header("Weapon")]
    [SerializeField] Transform weaponTransform;


    

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;

    //save manager
    SaveSystem saveSystem;

    //check the last direction the player was facing
    public bool facingRight = true;

    //rigidbody 
    [SerializeField] Rigidbody2D rb;

    [SerializeField] private PlayerClassManager playerclass;
    [SerializeField] private PlayerWeaponManager playerWeapon;





    // Update is called once per frame
    void Update()
    {
        //move left
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
            facingRight = false;
            UpdateWeaponRotation();
        }
        //move right
         if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
            facingRight = true;
            UpdateWeaponRotation();
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
            ShootProjectile();
            /*
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
               
            }*/


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
        //GetComponent<Transform>().localPosition += new Vector3(0, 2*speed*Time.deltaTime, 0);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);


        
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


    private void ShootProjectile()
    {
        int direction = facingRight ? 1 : -1;

        // Check the equipped weapon and execute the corresponding attack
        switch (playerWeapon.GetEquippedWeapon())
        {
            case "Bow":
                Debug.Log("Shooting Arrow!");
                projectile.LaunchArrow(direction);
                //PlayAttackSound();
                break;

            case "Sword":
                Debug.Log("Swinging Sword!");
                SwordAttack();
                break;

            case "Default":
            default:
                Debug.Log("Default projectile launched.");
                projectile.PlayerLauncher(direction);
                //PlayAttackSound();
                Debug.Log("Default projectile launched.");
                break;
        }
    }


    //weapon dirwection

    // Update weapon rotation based on player's facing direction
    private void UpdateWeaponRotation()
    {
        if (weaponTransform == null) return;

        if (facingRight)
        {
            // Rotate weapon to face right
            
            weaponTransform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            // Rotate weapon to face left
            weaponTransform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    


}
