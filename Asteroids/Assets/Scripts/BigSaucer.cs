using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// 2/20/24
// Big Saucer class

public class BigSaucer : Saucer
{
    protected void Start()
    {
        InvokeRepeating("Shoot", 1, 1);
    }
    protected override void Shoot()
    {
        // creates a bullet 
        // shoots bullets randomly
        Instantiate(SaucerBullet, transform.position, Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.rotation.z * 360)));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            PlayerData.Instance.IncreaseScore(points);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Asteroid"))
        {
            Destroy(gameObject);
        }
    }
}
