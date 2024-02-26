using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// 2/20/24
// Players bullets

public class PlayerBullet : Bullet
{
    private void OnTriggerEnter(Collider other)
    {
        // if the bullet hits an asteroid
        if (other.CompareTag("Asteroid"))
        {
            // pass the rotation and damage through to break the asteroid
            other.GetComponent<Asteroid>().AsteroidBreak(transform.rotation, damage);
            Destroy(gameObject);
        }
    }
}
