using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using Unity.IO.LowLevel.Unsafe;
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

    [Header("Camera Reference")]
    [SerializeField] Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        fireRateTimer = fireRate;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        fireRateTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0) && fireRateTimer >= fireRate)
        {
            Shoot();
            fireRateTimer = 0;
        }
    }

    void Shoot(){
        fireRateTimer = 0;
        audioSource.PlayOneShot(weaponFire);

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
}