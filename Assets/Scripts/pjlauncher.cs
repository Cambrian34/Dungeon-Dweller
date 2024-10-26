using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class pjlauncher : MonoBehaviour

{
    [Header("Ammo")]
    [SerializeField] int maxAmmo = 10;
    [SerializeField] int currentAmmo = 10;
    [SerializeField] float maxReloadTime = 10;
    [SerializeField] float cooldownTime = .25f;
    float currentReloadTime = 0;
     bool coolingDown = false;


    [Header("Fire Projectile")]
    [SerializeField] GameObject projectilePrefab;

    //arrow projectile 
    [Header("Arrow Projectile")]
    [SerializeField] GameObject arrowPrefab;

    [Header("Spawn Point")]
    [SerializeField] Transform spwanTransform;



    [Header("Projectile Config")]
    [SerializeField] float speed = 5.0f;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [Range(0, 1)]
    [SerializeField] float pitchRange = .2f;
    // Start is called before the first frame update


     void Awake(){
        currentAmmo = maxAmmo;
    }
    public void PlayerLauncher(int direction)
    {
        GameObject newpj = Instantiate(projectilePrefab, spwanTransform.position, Quaternion.identity);

        Rigidbody2D rb = newpj.GetComponent<Rigidbody2D>();
        //audioSource.Play();
        //audioSource.pitch = Random.Range(1 - pitchRange, 1 + pitchRange);
        //set gameobject tag to projectile
        //newpj.tag = "projectile";

        if (rb != null)
        {
            rb.velocity = new Vector3(direction*speed,0, 0);
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
    public void LAunchFireEnemyAi()
    {
        GameObject pj = Instantiate(projectilePrefab, spwanTransform.position, Quaternion.identity);
        pj.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -speed, 0);

        Destroy(pj, 4.0f);
        //i dont want audio for enemy projectile, wouldve been confusing
    }

    //launch arrow projectile
    public void LaunchArrow(int direction)
    {
        GameObject arrow = Instantiate(arrowPrefab, spwanTransform.position, Quaternion.identity);
        arrow.GetComponent<Rigidbody2D>().velocity = new Vector3(direction*speed, 0, 0);
        Destroy(arrow, 5.0f);
    }

    public void LaunchFireball(int direction){ //returns a recoil amount

        
        Cooldown();

        currentAmmo -= 1;
        GameObject newProjectile = Instantiate(projectilePrefab, spwanTransform.position, Quaternion.identity);
        newProjectile.GetComponent<Rigidbody2D>().velocity = new Vector3(direction*speed ,0, 0);
        //audioSource.pitch = Random.Range(1f-pitchRange,1f+pitchRange);
        //audioSource.Play();

        Destroy(newProjectile,2);
       
    }

    void Cooldown(){
        coolingDown = true;
        StartCoroutine(CoolingDownRoutine());
        IEnumerator CoolingDownRoutine(){
            yield return new WaitForSeconds(cooldownTime);
            coolingDown = false;
        }
    }
     bool currentlyReloading = false;
    public void Reload(){

        if(currentlyReloading){
            return;
        }
        if(currentAmmo == maxAmmo){
            return;
        }
        currentlyReloading = true;
        currentReloadTime = 0;
        StartCoroutine(ReloadRoutine());

        IEnumerator ReloadRoutine(){
            Debug.Log("Reload Routine Active!");
            //yield return new WaitForSeconds(reloadTime);

            while(currentReloadTime < maxReloadTime){
                yield return null;
                currentReloadTime += Time.deltaTime;
            }
            currentReloadTime = 0;
            currentAmmo = maxAmmo;
            currentlyReloading = false;
            Debug.Log("Reload Routine Done!");
        }

    }

     public int GetAmmo(){
        return currentAmmo;
    }
    public int GetMaxAmmo(){
        return maxAmmo;
    }
}
