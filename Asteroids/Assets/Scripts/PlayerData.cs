using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// 2/20/24
// Holds the player stats and manages deaths and telling the Gamemanager the state of the game 

public class PlayerData : Singleton<PlayerData>
{
    private int lives;
    private int score;

    public int Lives
    {
        get
        {
            return lives;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }
    }

    private GameObject PlayerRef;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        score = 0;

        // reference the player
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
       // store the start distance
        startPos = PlayerRef.transform.position;
    }

    // called when the player takes damage
    public void TakeDamage()
    {
        lives--;
        // if the player has no lives left
        if (lives <= 0)
        {
            // end the game
            GameManager.Instance.GameOver();
        }
        // player still has lives left
        else
        {
            // reset the player position
            PlayerRef.transform.position = startPos;
            StartCoroutine(PlayerRef.GetComponent<PlayerController>().GoInvincible());
        }
        // update the UI
        UIManager.Instance.UpdateUI();
    }

    // increases the score of the player
    public void IncreaseScore(int points)
    {
        // increase the points
        score += points;
        // Update the UI
        UIManager.Instance.UpdateUI();
    }

    // resets the player stats and position
    public void ResetStats()
    {
        // reset score and lives
        score = 0;
        lives = 3;
        // reset pos and rotation
        PlayerRef.transform.position = startPos;
        PlayerRef.transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
