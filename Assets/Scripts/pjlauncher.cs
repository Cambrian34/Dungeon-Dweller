using System.Collections;
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

    //weapon transform for sword
    [Header("Weapon Transform")]
    [SerializeField] Transform weaponTransform;

    [Header("Projectile Config")]
    [SerializeField] float speed = 5.0f;

    [Header("Audio")]
    [SerializeField] AudioSource fireballSound;

    //arrow sound effect
    [Header("Arrow Sound Effect")]
    [SerializeField] AudioSource arrowSound;

    //player and enemy death sound effect
    [Header("Death Sound Effect")]
    [SerializeField] AudioSource deathSound;

    [Header("Pitch Range")]
    [SerializeField] float pitchRange = .2f;

    // Start is called before the first frame update
    void Awake()
    {
        currentAmmo = maxAmmo;
    }

    public void PlayerLauncher(int direction)
    {
        GameObject newpj = Instantiate(projectilePrefab, spwanTransform.position, Quaternion.identity);
        Rigidbody2D rb = newpj.GetComponent<Rigidbody2D>();

        // Play fireball sound when firing projectile
        fireballSound.Play();
        
        // Set random pitch for fireball sound
        fireballSound.pitch = Random.Range(1 - pitchRange, 1 + pitchRange);

        if (rb != null)
        {
            rb.linearVelocity = new Vector3(direction * speed, 0, 0);
            Destroy(newpj, 4.0f);
        }
        else
        {
            Debug.LogError("Rigidbody2D component not found on projectile.");
        }
    }

    // Negative speed version for enemy
    public void LaunchFireEnemyAi()
    {
        Cooldown();
        GameObject pj = Instantiate(projectilePrefab, spwanTransform.position, Quaternion.identity);
        pj.GetComponent<Rigidbody2D>().linearVelocity = new Vector3(speed, 0, 0);
        Destroy(pj, 4.0f);
        // No audio for enemy projectiles
    }

    // Launch arrow projectile
    public void LaunchArrow(int direction)
    {
        GameObject arrow = Instantiate(arrowPrefab, spwanTransform.position, Quaternion.identity);

        // Rotate arrow
        arrow.transform.Rotate(0, 0, direction * 90);

        // Play arrow sound when firing arrow
        arrowSound.Play();
        
        // Set random pitch for arrow sound
        arrowSound.pitch = Random.Range(1 - pitchRange, 1 + pitchRange);

        arrow.GetComponent<Rigidbody2D>().linearVelocity = new Vector3(direction * speed, 0, 0);
        Destroy(arrow, 5.0f);
    }

    public void LaunchFireball(int direction)
    {
        Cooldown();

        currentAmmo -= 1;
        GameObject newProjectile = Instantiate(projectilePrefab, spwanTransform.position, Quaternion.identity);
        newProjectile.GetComponent<Rigidbody2D>().linearVelocity = new Vector3(direction * speed, 0, 0);

        // Play fireball sound with randomized pitch
        fireballSound.Play();
        fireballSound.pitch = Random.Range(1f - pitchRange, 1f + pitchRange);

        Destroy(newProjectile, 2);
    }

    void Cooldown()
    {
        coolingDown = true;
        StartCoroutine(CoolingDownRoutine());

        IEnumerator CoolingDownRoutine()
        {
            yield return new WaitForSeconds(cooldownTime);
            coolingDown = false;
        }
    }

    bool currentlyReloading = false;

    public void Reload()
    {
        if (currentlyReloading)
        {
            return;
        }
        if (currentAmmo == maxAmmo)
        {
            return;
        }
        currentlyReloading = true;
        currentReloadTime = 0;
        StartCoroutine(ReloadRoutine());

        IEnumerator ReloadRoutine()
        {
            Debug.Log("Reload Routine Active!");

            while (currentReloadTime < maxReloadTime)
            {
                yield return null;
                currentReloadTime += Time.deltaTime;
            }

            // Play reload sound after reloading
            // You can add an audio clip for reloading sound here
            // reloadSound.Play();

            currentReloadTime = 0;
            currentAmmo = maxAmmo;
            currentlyReloading = false;
            Debug.Log("Reload Routine Done!");
        }
    }

    public int GetAmmo()
    {
        return currentAmmo;
    }

    public int GetMaxAmmo()
    {
        return maxAmmo;
    }

    // Check if player hits enemy with sword in weapon transform
    public void CheckSwordHit()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(weaponTransform.position, new Vector2(1, 1), 0);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<HealthSystem>().TakeDamage(50);
                // Play death sound when enemy is hit
                deathSound.Play();
            }
        }
    }
}
