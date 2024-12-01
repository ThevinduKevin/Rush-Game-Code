using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public int maxAmmoInMag = 10;       // Maximum ammo capacity in the magazine
    public int maxAmmoInStorage = 30;  // Maximum ammo capacity in the storage
    public float shootCooldown = 0.5f; // Cooldown time between shots
    public float reloadCooldown = 0.5f;
    private float switchCooldown = 0.5f;
    public float shootRange = 100f;    // Range of the raycast

    public ParticleSystem impactEffect;

    public int currentAmmoInMag;
    public int currentAmmoInStorage;
    public int damage = 10;
    public bool canShoot = true;
    public bool canSwitch = true;
    private bool isReloading = false;
    private float shootTimer;

    public Transform cartridgeEjectionPoint;
    public GameObject cartridgePrefab;
    public float cartridgeEjectionForce = 5f;

    public Animator gun;
    public ParticleSystem muzzleFlash;
    public GameObject muzzleFlashLight;
    public AudioSource shoot;

    public int score = 0; // Player's score
    

    void Start()
    {
        currentAmmoInMag = maxAmmoInMag;
        currentAmmoInStorage = maxAmmoInStorage;
        canSwitch = true;
        muzzleFlashLight.SetActive(false);
    }

    void Update()
    {
        currentAmmoInMag = Mathf.Clamp(currentAmmoInMag, 0, maxAmmoInMag);
        currentAmmoInStorage = Mathf.Clamp(currentAmmoInStorage, 0, maxAmmoInStorage);

        if (Input.GetButtonDown("Fire1") && canShoot && !isReloading)
        {
            switchCooldown = shootCooldown;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            switchCooldown = reloadCooldown;
            Reload();
        }

        if (shootTimer > 0f)
        {
            shootTimer -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        if (currentAmmoInMag > 0 && shootTimer <= 0f)
        {
            canSwitch = false;

            // Play shooting effects
            shoot.Play();
            muzzleFlash.Play();
            muzzleFlashLight.SetActive(true);
            gun.SetBool("shoot", true);

            // Ensure the camera is not null
            if (Camera.main == null)
            {
                Debug.LogError("Camera.main is null. Make sure your camera is tagged as 'MainCamera'.");
                return;
            }

            // Perform raycast
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, shootRange))
            {
                Debug.Log("Raycast hit: " + hit.collider?.name);

                // Check if the object has the StreetObject component
                StreetObject streetObject = hit.collider.GetComponent<StreetObject>();
                if (streetObject != null)
                {
                    // Award points and destroy the street object
                    ScoreManager.Instance.AddScore(streetObject.pointValue);
                    streetObject.OnShot();
                    Debug.Log($"Score: {ScoreManager.Instance.TotalScore}");
                }

                // Create impact effect at hit point
                if (impactEffect != null)
                {
                    Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                }
            }
            else
            {
                Debug.Log("Raycast didn't hit anything.");
            }

            // Reduce ammo and start cooldown
            currentAmmoInMag--;
            shootTimer = shootCooldown;

            // End animations and light after effects
            StartCoroutine(EndAnimations());
            StartCoroutine(EndLight());
            StartCoroutine(CanSwitchShoot());
        }
        else
        {
            Debug.Log("Cannot shoot: No ammo or cooldown active.");
        }
    }



    void Reload()
    {
        switchCooldown -= Time.deltaTime;
        if (isReloading || currentAmmoInStorage <= 0)
            return;

        int bulletsToReload = maxAmmoInMag - currentAmmoInMag;
        if (bulletsToReload > 0)
        {
            gun.SetBool("reload", true);
            StartCoroutine(EndAnimations());

            int bulletsAvailable = Mathf.Min(bulletsToReload, currentAmmoInStorage);
            currentAmmoInMag += bulletsAvailable;
            currentAmmoInStorage -= bulletsAvailable;

            Debug.Log("Reloaded " + bulletsAvailable + " bullets");
            StartCoroutine(ReloadCooldown());
        }
        else
        {
            Debug.Log("Cannot reload");
        }
    }

    IEnumerator ReloadCooldown()
    {
        isReloading = true;
        canShoot = false;
        canSwitch = false;

        yield return new WaitForSeconds(reloadCooldown);

        isReloading = false;
        canShoot = true;
        canSwitch = true;
    }

    IEnumerator EndAnimations()
    {
        yield return new WaitForSeconds(.1f);
        gun.SetBool("shoot", false);
        gun.SetBool("reload", false);
    }

    IEnumerator EndLight()
    {
        yield return new WaitForSeconds(.1f);
        muzzleFlashLight.SetActive(false);
    }

    IEnumerator CanSwitchShoot()
    {
        yield return new WaitForSeconds(shootCooldown);
        canSwitch = true;
    }
}
