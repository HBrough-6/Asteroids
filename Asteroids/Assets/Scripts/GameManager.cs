using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // holds all the asteroids in scene
    private List<GameObject> _asteroidsInScene;
    // holds the current level that the player is on
    private int currentLevel;
    private int asteroidCount;
    // the maximum number of asteroids that will spawn at the start of each level
    private int MaxAsteroidCount = 4;
    // holds the current Saucer in the scene
    private GameObject SaucerInScene;
    // how long the delay between saucers spawning can be
    private float saucerSpawnDelay = 5f;
    [SerializeField]
    private GameObject BigAsteroidRef;

    // the two opposite corers that asteroids can spawn between.
    private Vector2 AsteroidSpawnBoundsBottom = new Vector2(-10.25f, -5f);
    private Vector2 AsteroidSpawnBoundsTop = new Vector2(10.25f, 5f);

    private void Awake()
    {
        currentLevel = 1;
        asteroidCount = 0;
    }

    private void Start()
    {
        SpawnAsteroids();
    }

    // starts the next level
    public void NextLevel()
    {
        // increase level number
        currentLevel++;
        // spawn new asteroids
        SpawnAsteroids();
    }

    public void ResetGame()
    {

    }

    private void SpawnSaucer()
    {
        // spawn a saucer if there no saucer on screen and the saucer spawn cooldown is done
    }

    private void SpawnAsteroids()
    {
        for (int asteroid = 0; asteroid < MaxAsteroidCount + currentLevel; asteroid++)
        {
            // create random coordinates and rotation for the asteroids to spawn at
            float xCoordinate = UnityEngine.Random.Range(AsteroidSpawnBoundsBottom.x, AsteroidSpawnBoundsTop.x);
            float yCoordinate = UnityEngine.Random.Range(AsteroidSpawnBoundsBottom.y, AsteroidSpawnBoundsTop.y);
            Quaternion rotation = UnityEngine.Random.rotation;
            // limit the rotation to just the z axis
            rotation = Quaternion.Euler(0, 0, rotation.z * 360);

            // create the asteroid and add it to the list
            AddAsteroidToList(Instantiate(BigAsteroidRef, new Vector3(xCoordinate, yCoordinate, 0), rotation));

        }
        // spawn maxAsteroidCount + currentLevel asteroids
        // add each asteroid to asteroidsInScene
    }

    /// <summary>
    /// adds the specified asteroid to the list and increases the asteroid count by 1
    /// </summary>
    /// <param name="asteroid"></param>
    public void AddAsteroidToList(GameObject asteroid)
    {
        _asteroidsInScene.Add(asteroid);
        asteroidCount++;
    }

    /// <summary>
    /// remove the specified asteroid from the list and scene and reduce the asteroid count by 1
    /// </summary>
    /// <param name="asteroid"></param>
    public void RemoveAsteroidFromList(GameObject asteroid)
    {
        _asteroidsInScene.Remove(asteroid);
        Destroy(asteroid);
        asteroidCount--;
    }
}
