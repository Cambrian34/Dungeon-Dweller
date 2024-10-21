using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pjlauncher : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] GameObject projectilePrefab;

    [Header("Spwan Point")]
    [SerializeField] Transform spwanTransform;



    [Header("Projectile Config")]
    [SerializeField] float speed = 5.0f;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [Range(0, 1)]
    [SerializeField] float pitchRange = .2f;
    // Start is called before the first frame update
    public void PlayerLauncher()
    {
        GameObject newpj = Instantiate(projectilePrefab, spwanTransform.position, Quaternion.identity);

        Rigidbody2D rb = newpj.GetComponent<Rigidbody2D>();
        audioSource.Play();
        //audioSource.pitch = Random.Range(1 - pitchRange, 1 + pitchRange);
        //set gameobject tag to projectile
        //newpj.tag = "projectile";

        if (rb != null)
        {
            rb.velocity = new Vector3(0, speed, 0);
            Destroy(newpj, 4.0f);
        }
        else
        {
            Debug.LogError("Rigidbody2D component not found on projectile.");
            //i didnt add a rigid body to the projectile prefab before this
        }
        

        //Destroy(newpj, 2.0f);
    }
    //negative speed version for enemy
    public void LaunchProjectileNeg()
    {
        GameObject pj = Instantiate(projectilePrefab, spwanTransform.position, Quaternion.identity);
        pj.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -speed, 0);

        Destroy(pj, 4.0f);
        //i dont want audio for enemy projectile, wouldve been confusing
    }
}
