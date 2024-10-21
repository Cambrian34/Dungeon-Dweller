using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class enemy : MonoBehaviour
{
    [Header("Enemy projectile")]
    [SerializeField] pjlauncher projectile;

    [Header("Fuel")]
    [SerializeField] GameObject Fuel;

    [Header("Enemy speed")]
    [SerializeField] float speed = 5.0f;

    [Header("Enemy spawn")]
    [SerializeField] GameObject enemysource;
    [SerializeField] Transform spwanTransform;

    //direction of enemy
    private bool movingLeft = true;  // Track if enemy is moving left

    //movement script
    //[Header("Movement script")]
    //[SerializeField] movement movement2;

    //camera 
    //[Header("Camera")]
    //[SerializeField] Camera camera;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //move enemy
        MoveEnemy();


    }
    //collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        //check if enemy is hit by player projectile
        if (collision.CompareTag("projectile"))
        {
            //Debug.Log("Break apart!");
            //destroy enemy
            Destroy(gameObject);

            //destroy projectile
            Destroy(collision.gameObject);
            //break apart enemy
            dropFuel();
            //spwan new enemy
            SpawnNewEnemy();

        }
        //if another thing collides, output to console


        //print("enemy hit");
    }
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
            projectile.LaunchProjectileNeg();
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
    public void dropFuel()
    {
        GameObject fuel = Instantiate(Fuel, transform.position, Quaternion.identity);
        //add gravity to fuel
        fuel.GetComponent<Rigidbody2D>().gravityScale = 1;
        //destroy after 2 seconds
        Destroy(fuel, 4.0f);


    }



    //spwan new enemy on death
    public void SpawnNewEnemy()
    {
        //spwan new enemy
        GameObject newEnemy = Instantiate(enemysource, spwanTransform.position, Quaternion.identity);

         // Ensure the enemy's script is enabled
    enemy enemyScript = newEnemy.GetComponent<enemy>();
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











}
