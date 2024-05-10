using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] float fireRate;
    float fireRateTimer;

    [Header("Projectile Properties")]
    [SerializeField] GameObject projectile;
    [SerializeField] Transform shootingPos;
    [SerializeField] float projectileVelocity;
    [SerializeField] float projectilesPerShot = 1;

    [SerializeField] AudioClip weaponFire;
    AudioSource audioSource;
    WeaponAmmo ammo;

    [Header("Camera Reference")]
    [SerializeField] Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        fireRateTimer = 0;
        audioSource = GetComponent<AudioSource>();
        ammo = GetComponent<WeaponAmmo>();
    }

    // Update is called once per frame
    void Update()
    {
        fireRateTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0) && fireRateTimer >= fireRate && ShouldShoot())
        {
            Shoot();
            fireRateTimer = 0;
        }
        Debug.Log(ammo.currentAmmo);

    }

    void Shoot(){
        fireRateTimer = 0;
        audioSource.PlayOneShot(weaponFire);

        ammo.currentAmmo -= 1;

        for (int i = 0; i < projectilesPerShot; i ++)
        {
            GameObject Instatiatedprojectile = Instantiate(projectile, shootingPos.position, mainCamera.transform.rotation);
            Rigidbody projectileRb = Instatiatedprojectile.GetComponent<Rigidbody>();

            if (projectileRb != null)
            {
                projectileRb.velocity = mainCamera.transform.forward * projectileVelocity;
            }
        }

         Debug.Log("Fired " + projectilesPerShot + " projectiles."); // Log the firing event
    }

    bool ShouldShoot(){
        return ammo.currentAmmo > 0; //returns true when there is ammo left
    }
}