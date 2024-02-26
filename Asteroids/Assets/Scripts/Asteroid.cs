using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Brough, Heath
// 2/20/24
// Moves asteroids and deals with spawning new asteroids after it is destroyed

public class Asteroid : MonoBehaviour
{
    // holds the reference for the smaller asteroid reference
    [SerializeField]
    private GameObject smallerAsteroidRef;
    [SerializeField]
    // how fast the asteroid is going
    private float currentSpeed = 1.5f;

    private float startSpeed = 1.5f;
    // equal to startSpeed * 2
    private float maxSpeed;
    private int health = 2;
    // how far away the asteroids can be looking when they are spawned from an asteroid being destroyed
    [SerializeField]
    private float spawnAngle = 30;

    private bool ScreenWrappedTimerActive = false;

    // keeps track of how many times the asteroid has hit the edge of the screen
    private int timesScreenWrapped = 0;

    private float increaseSpeedDelay = 2;

    [SerializeField]
    // how many points the asteroid is worth
    private int _points;  


    private void Start()
    {
        currentSpeed = startSpeed;
        StartCoroutine(IncreaseSpeed());
    }

    private void Update()
    {
        Move();
    }

    // moves the asteroids forwards
    private void Move()
    {
        transform.Translate(transform.up * Time.deltaTime * currentSpeed);
    }

    // spawns 2 smaller asteroids if possible, and then destroys the asteroid
    public void AsteroidBreak(Quaternion rotation, int damage)
    {
        health -= damage;
        // if the asteroid loses all of it's health
        if (health <= 0)
        {
            PlayerData.Instance.IncreaseScore(_points);
            // if the asteroidref is not null
            if (smallerAsteroidRef != null)
            {
                // create 2 smaller asteroids 
                for (int numAsteroids = 0; numAsteroids < 2; numAsteroids++)
                {
                    // create the asteroid
                    GameObject newAsteroid = Instantiate(smallerAsteroidRef, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z)
                    , Quaternion.Euler(GameManager.Instance.RandZRotationBased(rotation, spawnAngle)));
                    // add the new asteroid to the asteroid list
                    GameManager.Instance.AddAsteroidToList(newAsteroid);
                }
            }
            
            // destroy the asteroid and remove it from the list of asteroids
            GameManager.Instance.RemoveAsteroidFromList(gameObject);
        }
    }

    // increases the speed of the asteroid every increaseSpeedDelay seconds
    private IEnumerator IncreaseSpeed()
    {
        // increases the speed of the asteroid until it reaches the maximum speed
        while (this.enabled && currentSpeed < maxSpeed)
        {
            yield return new WaitForSeconds(increaseSpeedDelay);
            currentSpeed += 0.07f;
        }
        maxSpeed = currentSpeed;
    }

    /// <summary>
    /// function is called every time this asteroid is screenwrapped, stops the asteroid from being stuck off screen
    /// </summary>
    public IEnumerator ScreenWrapped()
    {
        // increase the counter
        timesScreenWrapped++;

        // if there is no timer going, start one
        if (!ScreenWrappedTimerActive)
        {
            // timer is active
            ScreenWrappedTimerActive = true; 
            yield return new WaitForSeconds(2);
            timesScreenWrapped = 0;
            ScreenWrappedTimerActive = false;
        }
        
        // if the timer is active
        if (ScreenWrappedTimerActive)
        {
            // and the times that the asteroid has screenwrapped is greater than 10
            if (timesScreenWrapped > 10)
            {
                // give it a new position
                Vector2 newPos = GameManager.Instance.CreateRandomCoordinates();
                transform.position = new Vector3(newPos.x, newPos.y, 0);
                // reset the timesScreenWrapped to 0
                timesScreenWrapped = 0;
                Debug.Log("Unstuck asteroid");
            }
        }
        
    }
}
