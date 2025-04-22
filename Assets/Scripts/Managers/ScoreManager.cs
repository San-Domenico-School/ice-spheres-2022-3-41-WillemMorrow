using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

/**************************************************
 * Adding, saving, and loading player scores
 * Attached to PlayerController
 * 
 * Sebastian Balakier
 * 4/22/25 Version 1.0
 **************************************************/

//ScoreManager.Instance(UpdateScene) (player,change)

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private int numberOfPlayers = 4;
    private int[] playerScores;
    private TextMeshProUGUI[] scoreUI;
    public int playerIndex;
    

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null || Instance != this)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        int[] playerScores;
        playerScores = new int[numberOfPlayers];
        TextMeshProUGUI[] scoreUI;
        scoreUI = new TextMeshProUGUI[numberOfPlayers];

        for (int i = 0; i < numberOfPlayers; i++) 
        {
            scoreUI[i] = GameObject.Find("Score_" + (i + 9)).GetComponent<TextMeshProUGUI>();
        }
    }

    private void Start()
    {
        
    }

    public void UpdateScore(int playerIndex, int points)
    {
        playerScores[playerIndex] += points;

        if (scoreUI != null)
        {
            scoreUI[playerIndex].text = "Player" + playerIndex.ToString() + ": " + playerScores[playerIndex].ToString();
        }
        LoadScore();
    }

    private void SaveScore()
    {

    }

    private void LoadScore()
    {
        if (PlayerPrefs.HasKey("PlayerScore"))
        {
            string savedString = PlayerPrefs.GetString("PlayerScore");
            string[] scores = savedString.Split('|');

            int length = Mathf.Min(playerScores.Length, scores.Length);

            for (int i = 0; i < length; i++)
            {
                playerScores[i] = int.Parse(scores[i]);
            }
        }
        else
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                UpdateScore(0, i);
            }
        }
    }

    public void AddScore()
    {
        
    }
}
