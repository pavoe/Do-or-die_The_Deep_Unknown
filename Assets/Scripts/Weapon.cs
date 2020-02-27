using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private float fireDelay = 0.2f;
    private float timeSinceLastShot;
    public int Ammo { get; private set; } = 6;
    public int MaxAmmo { get; private set; } = 6;
    private float reloadTime = 2f;
    private float currentReloadTime=2f;
    public bool Reloading { get; private set; } = false;

    // Update is called once per frame
    void Update()
    {
        if (timeSinceLastShot < fireDelay)
        {
            timeSinceLastShot += Time.deltaTime;
        }
        if (Reloading)
        {
           
            currentReloadTime += Time.deltaTime;
            if (currentReloadTime >= reloadTime)
            {
                Ammo = MaxAmmo;
                Reloading = false;
                if (gameObject.Equals(GameController.gameController.PC))
                {
                    GameController.gameController.SwitchAmmoDisplayState(false);
                    GameController.gameController.RefreshCharacterAmmoDisplay();
                }
                else
                {
                    gameObject.transform.Find("ReloadArt").GetComponent<Renderer>().enabled = false;
                }
            }
        }

    }

    /// <summary>
    /// Shooting mechanism.
    /// </summary>
    public void Shoot()
    {
        if (timeSinceLastShot >= fireDelay&&Ammo>0&&!Reloading)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, gameObject.transform); //chcemy jedynie zespawnować pocisk
            timeSinceLastShot = 0f;
            Ammo--;
            if (gameObject.Equals(GameController.gameController.PC))
            {
                GameController.gameController.RefreshCharacterAmmoDisplay();
            }
        }
      
    }

    public void Reload()
    {
        if (Ammo < MaxAmmo)
        {
            if (gameObject.Equals(GameController.gameController.PC))
            {
                GameController.gameController.SwitchAmmoDisplayState(true);
            }
            else
            {
                gameObject.transform.Find("ReloadArt").GetComponent<Renderer>().enabled = true;
            }
            Reloading = true;
            currentReloadTime = 0f;
        }
       
    }
}
