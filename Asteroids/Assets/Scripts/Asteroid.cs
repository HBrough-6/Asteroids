using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public  class Asteroid : MonoBehaviour
{
    // holds the reference for the smaller asteroid reference
    [SerializeField]
    private GameObject smallerAsteroidRef;
    [SerializeField]
    // how fast the asteroid is going
    private float speed = 5;

    // how many points the asteroid is worth
    private int _points;
    public int Points
    {
        // accessor for the points of an asteroid
        get
        {
            return Points;
        }
    }

    private float increaseSpeedDelay = 2;

    private void Update()
    {
        Move();
    }

    // moves the asteroids forwards
    private void Move()
    {
        transform.Translate(transform.up * Time.deltaTime * speed);
    }

    // spawns 2 smaller asteroids if possible, and then destroys the asteroid
    public void AsteroidBreak()
    {
        // if the asteroidref is not null
        if (smallerAsteroidRef != null)
        {
            // create 2 smaller asteroids 
            Instantiate(smallerAsteroidRef, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
            Instantiate(smallerAsteroidRef, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), transform.rotation);
        }
        // destroy the asteroid and remove it from the list of asteroids
        GameManager.Instance.RemoveAsteroidFromList(gameObject);
    }

    // increases the speed of the asteroid every increaseSpeedDelay seconds
    private IEnumerator IncreaseSpeed()
    {
        while (this.enabled)
        {
            yield return new WaitForSeconds(increaseSpeedDelay);
            speed += 0.2f;
        }
    }
}
