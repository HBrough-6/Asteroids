using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

// Brough, Heath
// 2/20/24
// controls spawning of enemies and holds various functions for other classes to use

public class GameManager : Singleton<GameManager>
{
    // holds all the asteroids in scene
    [SerializeField]
    private List<GameObject> _asteroidsInScene;
    // holds the current level that the player is on
    public int currentLevel;
    public int asteroidCount;

    // testing purposes
    public bool stopAsteroids = true;


    [SerializeField]
    // the maximum number of asteroids that will spawn at the start of each level
    private int MaxAsteroidCount = 4;
    // tells if a Saucer is in the scene
    private bool SaucerInScene;
    private int SaucersPerLevel = 0;
    // how long the delay between saucers spawning can be
    private GameObject CurrentSaucer;
    [SerializeField]
    private GameObject BigAsteroidRef;
    [SerializeField]
    private GameObject BigSaucerRef;

    // the two opposite corers that asteroids can spawn between.
    private Vector2 _asteroidSpawnBoundsTop = new Vector2(9f, 4f);
    private Vector2 _asteroidSpawnBoundsBottom = new Vector2(-9f, -4f);

    public bool check = false;

    private bool gameEnded = false;

    // accessors for the spawnBounds on screen
    public Vector2 AsteroidSpawnBoundsTop
    {
        get
        {
            return _asteroidSpawnBoundsTop;
        }
    }
    public Vector2 AsteroidSpawnBoundsBottom
    {
        get
        {
            return _asteroidSpawnBoundsBottom;
        }
    }

    private void Awake()
    {
        currentLevel = 1;
        asteroidCount = 0;
    }

    private void Start()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (check)
        {
            EmptyField();
            check = false;
        }
    }

    // starts the next level
    public void NextLevel()
    {
        // increase level number
        currentLevel++;
        // spawn new asteroids
        SpawnAsteroids();
    }

    public void GameOver()
    {
        EmptyField();
        UIManager.Instance.EndScreen();
    }


    public void ResetGame()
    {
        currentLevel = 1;
        asteroidCount = 0;

        PlayerData.Instance.ResetStats();
    }

    private IEnumerator SpawnSaucer()
    {
        // not if the saucer is in the scene and the limit of saucers spawned has not passed the maximum
        if (!SaucerInScene && SaucersPerLevel < currentLevel + 1)
        {
            SaucerInScene = true;
            // wait for between 1-6 seconds before creating the saucer
            yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 6f));
            Vector2 newCoords = CreateRandomCoordinates();
            // create the big saucer off screen and give it a random height and rotation
            Instantiate(BigSaucerRef, new Vector3(9.7f, newCoords.y, 0),
            Quaternion.Euler(RandZRotationBased(transform.rotation, 10)));
            // increase the 
            SaucersPerLevel++;
        }
        // once the saucer is spawned
        else if (SaucerInScene)
        {
            // if there is no saucer in the scene
            if (CurrentSaucer == null)
            {
                // set SaucerInScene to false
                SaucerInScene = false;
            }
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(SpawnSaucer());
    }
    
    public void GameStart()
    {
        SpawnAsteroids();
        StartCoroutine(SpawnSaucer());
    }

    private void SpawnAsteroids()
    {
        // reset the saucersPerLevel Counter at the start of each level
        SaucersPerLevel = 0;
        if (!stopAsteroids)
        {
            for (int asteroid = 0; asteroid < MaxAsteroidCount + currentLevel; asteroid++)
            {
                // create random coordinates and rotation for the asteroids to spawn at
                Vector2 randomCoords = CreateRandomCoordinates();
                Quaternion rotation = UnityEngine.Random.rotation;
                // limit the rotation to just the z axis
                rotation = Quaternion.Euler(0, 0, rotation.z * 360);

                // create the asteroid and add it to the list
                AddAsteroidToList(Instantiate(BigAsteroidRef, new Vector3(randomCoords.x, randomCoords.y, 0), rotation) as GameObject);
            }
            // spawn maxAsteroidCount + currentLevel asteroids
            // add each asteroid to asteroidsInScene
        }
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

        if (asteroidCount <= 0 && !gameEnded)
        {
            NextLevel();
        }
    }

    public Vector2 CreateRandomCoordinates()
    {
        float xCoordinate = UnityEngine.Random.Range(_asteroidSpawnBoundsBottom.x, _asteroidSpawnBoundsTop.x);
        float yCoordinate = UnityEngine.Random.Range(_asteroidSpawnBoundsBottom.y, _asteroidSpawnBoundsTop.y);

        return new Vector2(xCoordinate, yCoordinate);
    }

    // create a function that will return a random z rotation that is x degrees between the inputted rotation
    /// <summary>
    /// returns a z angle between rotation.z - angleSize and rotation.z + angleSize
    /// </summary>
    /// <param name="rotation">the z rotation becomes the rotation on which the returning number is centered on</param>
    /// <param name="AngleSize">controls how far the returned value is from the inputted z</param>
    /// <returns></returns>
    public Vector3 RandZRotationBased(Quaternion rotation, float angleSize)
    {
        float z = rotation.eulerAngles.z;
        float newAngle = UnityEngine.Random.Range(0, angleSize);
        Vector3 toReturn = Vector3.zero;

        if ((int)UnityEngine.Random.Range(0f, 1f) >  0.5f)
        {
            toReturn.z = z - newAngle;
        }
        else
        {
            toReturn.z = z + newAngle;
        }

        return toReturn;
    }

    /// <summary>
    /// returns a z angle that is between the range of difference to the rotation of z inputted
    /// </summary>
    /// <param name="rotation">the z rotation becomes the rotation on which the returning number is centered on</param>
    /// <param name="rangeBottom">how much smaller the angle can be</param>
    /// <param name="rangeTop">how much larger the angle can be</param>
    /// <returns></returns>
    /// 
    public Vector3 RandZRotationBased(Quaternion rotation, float rangeBottom, float rangeTop)
    {
        float z = rotation.eulerAngles.z;
        float newAngle = UnityEngine.Random.Range(rangeBottom, rangeTop);
        Vector3 toReturn = Vector3.zero;

        toReturn.z = z + newAngle;

        return toReturn;
    }

    private void EmptyField()
    {
        gameEnded = true;
        for (int asteroid = _asteroidsInScene.Count; asteroid > 0; asteroid--)
        {
            RemoveAsteroidFromList(_asteroidsInScene[0]);
        }
    }
}
