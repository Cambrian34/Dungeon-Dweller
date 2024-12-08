using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class enemyAIManager : MonoBehaviour


{
    //rigidbody
    [SerializeField]Rigidbody2D rb;

    [Header("Enemy projectile")]
    [SerializeField] pjlauncher projectile;

    [Header("Fuel")]
    [SerializeField] GameObject Gold;

    [Header("Enemy speed")]
    [SerializeField] float speed = 5.0f;

    [Header("Enemy spawn")]
    [SerializeField] GameObject enemysource;
    [SerializeField] Transform spwanTransform;

    [Header("Hp")]
    [SerializeField] int hp = 200;
    private int maxHp = 200;
    HealthSystem healthSystem;

    //healthbar slider
    [SerializeField] Slider healthSlider;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [Range(0, 1)]

    //public Slider healthSlider;//implement later

    //direction of enemy
    private bool movingLeft = true;  // Track if enemy is moving left

    

    public event Action<enemyAIManager> OnDeath;



    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystem(hp);

        //register enemy with enemy tracker
        FindObjectOfType<EnemyTracker>()?.RegisterEnemy(this);


    }

    // Update is called once per frame
    void Update()
    {
        //move enemy
        //MoveEnemy();
        UpdateHealthBar();


    }
    private void Die()
    {
        OnDeath?.Invoke(this);
         
        Debug.Log("Enemy is dead.");

        //Drop a health potion with a 50% chance, a random weapon with a 10% chance, and gold with a 100% chance
        
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            // Drop a health potion
            //Instantiate(healthPotionPrefab, transform.position, Quaternion.identity);
        }
        else if (UnityEngine.Random.Range(0, 10) == 0)
        {
            // Drop a random weapon
            //Instantiate(weaponPrefabs[UnityEngine.Random.Range(0, weaponPrefabs.Length)], transform.position, Quaternion.identity);
        }
        // Drop gold
        dropGold();

        // Play the enemy death sound
        audioSource.Play();

        
        // Trigger enemy death sound, and logic
        Destroy(gameObject); 
    }
    


    //collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        //check if enemy is hit by player projectile
        if (collision.CompareTag("Fireball"))
        {
            
            Destroy(collision.gameObject);
            UpdateHealthBar();

            healthSystem.TakeDamage(50);
            Debug.Log("Enemy hp: " + healthSystem.GetHealth());
            if (healthSystem.GetHealth() <= 0)
            {
                Die();
            }

        }
        //if hit by player sword
        if (collision.CompareTag("Sword"))
        {
            //destroy projectile
            //Destroy(collision.gameObject);
            //lower hp
            healthSystem.TakeDamage(100);
            Debug.Log("Enemy hp: " + healthSystem.GetHealth());
            if (healthSystem.GetHealth() <= 0)
            {
                Die();
            }
        }
        //if hit by player arrow
        if (collision.CompareTag("Arrow"))
        {
            //destroy projectile
            //Destroy(collision.gameObject);
            //lower hp
            healthSystem.TakeDamage(60);
            Debug.Log("Enemy hp: " + healthSystem.GetHealth());
            UpdateHealthBar();
            if (healthSystem.GetHealth() <= 0)
            {
                Die();
            }
        }


        //print("enemy hit");
    }
    //update health bar
    void UpdateHealthBar()
    {
        hp = healthSystem.GetHealth();
        healthSlider.value = Mathf.Clamp(hp / (float)maxHp, 0f, 1f);
    }


    //redact
    //use bounds to move enemy
    public void MoveEnemy()
    {
        //get bounds of the screen
        Bounds bounds = GetViewportBounds(Camera.main);
        //not working as expected
        // Move enemy left or right depending on current direction
        if (movingLeft)
        {
            // Move enemy left
            MoveLeft();

            // If enemy reaches the left bound, reverse direction
            if (transform.localPosition.x <= bounds.min.x )
            {
                movingLeft = false;  // Start moving right
            }
        }
        else
        {
            // Move enemy right
            MoveRight();

            // If enemy reaches the right bound, reverse direction
            if (transform.localPosition.x >= bounds.max.x )
            {
                movingLeft = true;  // Start moving left
            }
        }
        //shoot projectile every 2 seconds 
        if (Time.time % 2 == 0)
        {
            LaunchFireball();
        }



    }



    public Bounds GetViewportBounds(Camera camera)
    {
        if (!camera)
        {
            return new Bounds();
        }
        var screenAspect = (float)Screen.width / (float)Screen.height;
        var cameraHeight = camera.orthographicSize * 2;
        return new Bounds(
            camera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
    }






    //move left
    public void MoveLeft()
    {
        //transform.position += new Vector3(-1, 0, 0);
        GetComponent<Transform>().localPosition += new Vector3(-(speed * Time.deltaTime), 0, 0);
    }

    //move right
    public void MoveRight()
    {
        //transform.position += new Vector3(1, 0, 0);
        GetComponent<Transform>().localPosition += new Vector3(speed * Time.deltaTime, 0, 0);
    }

    //break apart enemy and drop rocket fuel
    public void dropGold()
    {
        GameObject fuel = Instantiate(Gold, transform.position, Quaternion.identity);
        //add gravity to fuel
        
        //destroy after 2 seconds
        //Destroy(fuel, 4.0f);


    }



    //spwan new enemy on death
    public void SpawnNewEnemy()
    {
        //spwan new enemy
        GameObject newEnemy = Instantiate(enemysource, spwanTransform.position, Quaternion.identity);

         // Ensure the enemy's script is enabled
    enemyAIManager enemyScript = newEnemy.GetComponent<enemyAIManager>();
    if (enemyScript != null)
    {
        enemyScript.enabled = true;  // Enable the script if it was disabled
        enemyScript.movingLeft = true;  // Reset direction
        enemyScript.speed = 5.0f;        // Reset speed (if required)
    }

    // Ensure the collider is enabled
    Collider2D enemyCollider = newEnemy.GetComponent<Collider2D>();
    if (enemyCollider != null)
    {
        enemyCollider.enabled = true;  // Enable the collider if it was disabled
    }

    }

    public  void MoveToward(Vector3 playerposition)
    {
        playerposition.z = 0;
        Vector3 direction = playerposition - transform.position;
        Move(direction.normalized);
    }

    public void Move(Vector3 movement)
    {
        rb.AddForce(movement * speed);
    }

    public void LaunchFireball()
    {
        projectile.LaunchFireEnemyAi();
    }

    public void LaunchFirebal2()
    {
        projectile.LaunchFireball(1);
    }
    public void StopMoving()
    {
        rb.linearVelocity = Vector3.zero;
    }
    public pjlauncher GetProjectileLauncher(){
        return projectile;
    }
}
