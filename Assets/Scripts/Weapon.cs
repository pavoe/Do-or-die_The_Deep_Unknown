using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3")) // UnityScene\Edit\ProjectSettings\Input - u mnie ustawiony jest lewy Shift
            Shoot();
        //zmienimy to na spację po zmianie poruszania się - po zmianie skakania ze spacji na W lub "up"
    }

    /// <summary>
    /// Shooting mechanism.
    /// </summary>
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); //chcemy jedynie zespawnować pocisk
    }
}
