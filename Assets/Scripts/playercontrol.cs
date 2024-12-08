using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PlayerControl : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    //[SerializeField] float jumpForce = 5.0f;

    [Header("Tools")]
    [SerializeField] pjlauncher projectile;

    [Header("Weapon")]
    [SerializeField] Transform weaponTransform;

    // Default weapon
    [SerializeField] Weapon defaultWeapon;

    // Save manager
    SaveSystem saveSystem;

    // Inventory system
    [SerializeField] Inventorysystem inventorySystem;

    // Check the last direction the player was facing
    [SerializeField] bool facingRight = true;

    // Rigidbody 
    [SerializeField] Rigidbody2D rb;

    //canvas for options
    [SerializeField] GameObject optionsCanvas;

    void Start()
    {
        
        
    }

    // Update is called once per frame
    [SerializeField] private PlayerWeaponManager playerWeapon;

    // Update is called once per frame
    void Update()
    {
        // Move left
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
            facingRight = false;
            UpdateWeaponRotation();
        }

        // Move right
        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
            facingRight = true;
            UpdateWeaponRotation();
        }

        // Move up
        if (Input.GetKey(KeyCode.W))
        {
            MoveUp();
        }


        // Attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PerformAttack();
        }

        // Escape to main menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //show options canvas
            Toggleoptions();

        }
    }

    //toggle options canvas and pause game by calling the toggle pause method from the inventory system
    public void Togglepau()
    {
        //show options canvas
        
        inventorySystem.TogglePause();
    }

    //toggle options canvas and pause game by calling the toggle pause method from the inventory system
    public void Toggleoptions()
    {
        //check current state of options canvas
        if (optionsCanvas.activeSelf)
        {
            optionsCanvas.SetActive(false);
            Togglepau();
        }
        else
        {
            optionsCanvas.SetActive(true);
            Togglepau();
        }

    }
    

    // Move left (one press per move)
    public void MoveLeft()
    {
        GetComponent<Transform>().localPosition += new Vector3(-(speed * Time.deltaTime), 0, 0);
        //transform.position += new Vector3(-1, 0, 0);
    }

    // Move right (one press per move)
    public void MoveRight()
    {
        GetComponent<Transform>().localPosition += new Vector3(speed * Time.deltaTime, 0, 0);
        //transform.position += new Vector3(1, 0, 0);
    }

    // Move up (one press per move)
    public void MoveUp()
    {
        GetComponent<Transform>().localPosition += new Vector3(0,speed * Time.deltaTime, 0);
        //rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Apply jump force
        //transform.position += new Vector3(0, 1, 0);
        }

    // Move down (one press per move)
    public void MoveDown()
    {   
        GetComponent<Transform>().localPosition += new Vector3(0, -speed * Time.deltaTime, 0);
        //transform.position += new Vector3(0, -1, 0);
    }

    // Launch projectile
    public void LaunchProjectile(int direction)
    {
        projectile.PlayerLauncher(direction);
    }

    // Perform the attack based on the player's class and weapon
    public void PerformAttack()
    {
        
            //check what the player is holding and perform the attack, using whats in the weapon prefab
            if (playerWeapon.GetEquippedWeapon() == "Sword")
            {
                SwordAttack();
            }
            else if (playerWeapon.GetEquippedWeapon() == "Bow")
            {
                BowAttack(facingRight ? 1 : -1);
            }
            else if (playerWeapon.GetEquippedWeapon() == "Staff")
            {
                StaffAttack(facingRight ? 1 : -1);
            }
            else
            {
                Debug.LogError("No weapon equipped.");
            }
        
        
    }

    // Sword attack (perform action when the sword swings)
    public void SwordAttack()
    {
        weaponTransform.localRotation = Quaternion.Euler(0, 0, 90);
        StartCoroutine(ResetWeaponRotation());
    }

    private IEnumerator ResetWeaponRotation()
    {
        yield return new WaitForSeconds(0.1f);
        weaponTransform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    // Bow attack (shoot arrow in specified direction)
    public void BowAttack(int direction)
    {
        projectile.LaunchArrow(direction);
    }

    // Staff attack (shoot fireball in specified direction)
    public void StaffAttack(int direction)
    {
        projectile.LaunchFireball(direction);
    }

    // Update weapon rotation based on player's facing direction
    private void UpdateWeaponRotation()
    {
        if (weaponTransform == null) return;

        if (facingRight)
        {
            weaponTransform.localRotation = Quaternion.Euler(0, 0, 0);
            CheckSwordHit();
        }
        else
        {
            weaponTransform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    // Check if the sword hit an enemy
    public void CheckSwordHit()
    {
        /*Collider2D[] hits = Physics2D.OverlapBoxAll(weaponTransform.position, new Vector2(1, 1), 0);
        //foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<HealthSystem>().TakeDamage(50);
            }
        }*/
    }

    // Set default class and weapon when errors occur
    private void SetDefaultClassAndWeapon()
    {
        if (PlayerSaveData.selectedClass == null)
        {
            PlayerSaveData.selectedClass = new PlayerClass();
        }

        if (PlayerSaveData.selectedWeapon == null)
        {
            PlayerSaveData.selectedWeapon = defaultWeapon;
        }

        PlayerSaveData.selectedClass.className = "Warrior";
        PlayerSaveData.selectedWeapon.weaponName = "Bow";

        Debug.Log("Player class and weapon set to default: Warrior and Bow.");
    }
}
