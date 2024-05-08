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

    // Start is called before the first frame update
    void Start()
    {
        fireRateTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        fireRateTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0) && fireRateTimer >= 0)
        {
            Shoot();
            fireRateTimer = 0;
        }
    }

    void Shoot(){
        fireRateTimer = 0;

        for (int i = 0; i < projectilesPerShot; i ++)
        {
            GameObject Instatiatedprojectile = Instantiate(projectile, shootingPos.position, shootingPos.rotation);
            Rigidbody projectileRb = Instatiatedprojectile.GetComponent<Rigidbody>();

            if (projectileRb != null)
            {
                projectileRb.velocity = shootingPos.forward * projectileVelocity;
            }
        }

         Debug.Log("Fired " + projectilesPerShot + " projectiles."); // Log the firing event
    }
}