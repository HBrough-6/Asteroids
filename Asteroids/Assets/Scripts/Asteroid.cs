using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum AsteroidSize
{
    Big = 1,
    Medium,
    Small
}

public  class Asteroid : MonoBehaviour
{
    /// <summary>
    /// creates an asteroid object of size Big,Medium,or Small
    /// </summary>
    /// <param name="size">the size of the asteroid</param>
    public Asteroid(AsteroidSize size)
    {
        currentAsteroidSize = size;
    }
    public Asteroid()
    {
        currentAsteroidSize = AsteroidSize.Big;
    }

    // holds the reference for the smaller asteroid reference
    [SerializeField]
    private GameObject[] smallerAsteroidRef;

    private AsteroidSize currentAsteroidSize;
    // how fast the asteroid is going
    private float speed;

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


    private void Move()
    {

    }

    private void AsteroidBreak()
    {

    }
}
