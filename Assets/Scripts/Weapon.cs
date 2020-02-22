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
      
        
    }

    /// <summary>
    /// Shooting mechanism.
    /// </summary>
    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); //chcemy jedynie zespawnować pocisk
    }
}
