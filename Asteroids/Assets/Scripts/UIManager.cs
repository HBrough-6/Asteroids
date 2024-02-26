using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Brough, Heath
// 2/20/24
// handles updating UI, transitioning between games, and quitting the game

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject lifeText;
    [SerializeField]
    private GameObject scoreText;
    [SerializeField]
    private GameObject InGameUIRef;
    [SerializeField]
    private GameObject GameStartRef;
    [SerializeField]
    private GameObject GameEndRef;


    // Start is called before the first frame update
    void Start()
    {
        // updates the UI at the start of the game to default values
        UpdateUI();
    }
    
    // updates the in game UI
    public void UpdateUI()
    {
        // update lives and score
        lifeText.GetComponent<TMP_Text>().text = PlayerData.Instance.Lives.ToString();
        scoreText.GetComponent<TMP_Text>().text = PlayerData.Instance.Score.ToString();
    }

    // starts the game by unpausing time, disables the startScreen and enables the in game UI
    public void StartGame()
    {
        // unpause time
        Time.timeScale = 1;
        // enable the InGameUI
        InGameUIRef.SetActive(true);
        // disable the start screen
        GameStartRef.SetActive(false);

        GameManager.Instance.GameStart();
    }

    // starts the game again
    public void PlayAgain()
    {
        // reset the game
        GameManager.Instance.ResetGame();
        // enable the start menu
        GameStartRef.SetActive(true);
        // disable the end menu
        GameEndRef.SetActive(false);
    }


    // pulls up the end screen and pauses time
    public void EndScreen()
    {
        // disable the InGameUI
        InGameUIRef.SetActive(false);
        // enable the Play again screen
        GameEndRef.SetActive(true);
        // pause time
        Time.timeScale = 0;

        // get the final score and display it
        GameEndRef.transform.GetChild(0).GetComponent<TMP_Text>().text = PlayerData.Instance.Score.ToString();
    }
    //quits the game
    public void Quit()
    {
        Application.Quit();
    }

}

