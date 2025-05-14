using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

/***************************************************************************
 * Scorekeeper.cs
 * 
 * Description:
 * This script is a component of the Scoreboard system.
 * It tracks and updates player scores within the current scene and 
 * persists scores across scenes using PlayerPrefs.
 * 
 * Usage:
 * Attach this script to a Scoreboard GameObject in your scene.
 * UI elements must be named "Score_9", "Score_10", etc., corresponding
 * to each player's index + 9.
 * 
 * Author: Bruce Gustin
 * Date: April 6, 2025
 ***************************************************************************/

public class Scorekeeper : MonoBehaviour
{
    public static Scorekeeper Singleton;

    private int numberOfPlayers = 4;
    public int[] playerScores { get; private set; } // Tracks the player's score
    private GameObject[] scoreUI;

    private void Awake()
    {
        // Ensures only one instance of Scorekeeper exists in the scene.
        // If another instance is already assigned to Singleton, this one is destroyed.
        if (Singleton == null || Singleton != this)
        {
            Singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Initializes the playerScores array and finds corresponding UI elements by name.
    // Then loads previously saved scores from PlayerPrefs (if available).
    void Start()
    {
        playerScores = new int[numberOfPlayers];
        scoreUI = new GameObject[numberOfPlayers];
        for(int i = 0; i < numberOfPlayers; i++)
        {
            scoreUI[i] = GameObject.Find("Score_" + (12 - i));  // UI element names: Score_9, Score_10, Score_11, Score_12
            //Debug.Log("Scorekeeper_39: " + scoreUI[i]);
        }
        LoadScore();

        //DontDestroyOnLoad(this);
    }

    // Updates the specified player's score by adding the given number of points.
    // Then updates the corresponding UI element to reflect the new score.
    // Finally, saves the updated scores to PlayerPrefs to persist across scenes.
    public void UpdateScore(int playerIndex, int points)
    {
        playerScores[playerIndex] += points;

        if (scoreUI[playerIndex] != null)
        {
            TextMeshProUGUI textComponent = scoreUI[playerIndex].GetComponent<TextMeshProUGUI>();
            textComponent.text = ("score: " + playerScores[playerIndex].ToString());
        }

        SaveScore(); // Ensure score is saved after each update
    }

    public void UpdateDeathTimer(int playerIndex, int time)
    {
        if (scoreUI[playerIndex] != null)
        {
            TextMeshProUGUI textComponent = scoreUI[playerIndex].GetComponent<TextMeshProUGUI>();
            if (time > 0)
            {
                
                textComponent.text = ("respawn in: " + time);
            }
            else
            {
                textComponent.text = ("Press Start!");
            }
        }
    }

    // Converts the playerScores array into a "|" delimited string and saves it to PlayerPrefs.
    // This ensures scores can be retrieved even after changing scenes or restarting the game.
    private void SaveScore()
    {
        //Combines all elements in the playerScores list/array into a single string, separating them with a "|" character.  
        string joinedString = string.Join("|", playerScores);

        //Saves the combined string into PlayerPrefs under the key "PlayerScore" so it can be retrieved later.
        PlayerPrefs.SetString("PlayerScore", joinedString);

        //Ensures that the saved data is written to storage immediately, preventing data loss if the game closes.
        PlayerPrefs.Save();
        //Debug.Log("Scorekeeper_71: " + joinedString);
    }

    // Loads player scores from PlayerPrefs if available.
    // If data exists, parses and applies it; otherwise, initializes scores with default values.
    // Updates the UI by calling UpdateScore for each player.
    private void LoadScore()
    {
        TextMeshProUGUI[] scoreText = new TextMeshProUGUI[numberOfPlayers];
        if ((PlayerPrefs.HasKey("PlayerScore")) && (!(SceneManager.GetActiveScene().name == "Island1")))
        {
            
            
            string savedString = PlayerPrefs.GetString("PlayerScore"); //Gets the data that was saved
            string[] scores = savedString.Split('|');  //Splits the joined data into the individual scores

            int length = Mathf.Min(playerScores.Length, scores.Length); // Makes sure there are not more scores then players (rare)
            for (int i = 0; i < length; i++)
            {
                playerScores[i] = int.Parse(scores[i]);  //Converts the string into an int
                UpdateScore(i, playerScores[i]);             
            }
        }
        else
        {
            PlayerPrefs.DeleteKey("PlayerScore");

            for (int i = 0; i < numberOfPlayers; i++)
            {
                UpdateScore(i, 0);
            }
        }
        
    }
}