using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // holds all the asteroids in scene
    private GameObject[] _asteroidsInScene;
    // holds the current level that the player is on
    private int currentLevel;
    private int asteroidCount;
    // the maximum number of asteroids that will spawn at the start of each level
    private int MaxAsteroidCount;
    // holds the current Saucer in the scene
    private GameObject SaucerInScene;
    // how long the delay between saucers spawning can be
    private float saucerSpawnDelay;

    public GameObject[] AsteroidsInScene
    {
        get
        {
            return _asteroidsInScene;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // starts the next level
    public void NextLevel()
    {
        // increase level number
        // spawn new asteroids
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
        // spawn maxAsteroidCount + currentLevel asteroids
        // add each asteroid to asteroidsInScene
    }
}
