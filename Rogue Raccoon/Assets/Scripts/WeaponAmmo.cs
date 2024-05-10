using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public int baseAmmoCount;
    public int spareAmmoCount;
    public int currentAmmo;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = baseAmmoCount;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public void Reload()
    {
        if (spareAmmoCount >= baseAmmoCount)
        {
            int ammoReloadAmt = baseAmmoCount - currentAmmo;
            spareAmmoCount -= ammoReloadAmt; //subtracts the spare ammo ammount from the reload amount
            currentAmmo += ammoReloadAmt;   //adds from the reload amount to the current ammo count
        } else if (spareAmmoCount > 0)
        {
            if (spareAmmoCount + currentAmmo > baseAmmoCount)
            {
                int leftOverAmmo = spareAmmoCount + currentAmmo - baseAmmoCount;
                spareAmmoCount = leftOverAmmo;
                currentAmmo = baseAmmoCount;
            }
            else 
            {
                currentAmmo += spareAmmoCount;
                spareAmmoCount = 0;
            }
        }
    }
}
