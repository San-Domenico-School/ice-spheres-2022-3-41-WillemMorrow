using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private int numberOfPlayers = 4;
    private int[] playerScores;
    private TextMeshProUGUI scoreUI;
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
            scoreUI = GameObject.Find("Score_" + (i + 1)).GetComponent<TextMeshProUGUI>();
        }
    }

    private void Start()
    {
        
    }

    public object UpdateScore(int playerIndex, int points)
    {
        if (playerIndex >= 0)
        {
            return playerScores[playerIndex] += points;
        }
        else
        {
            // Decide on a proper fallback value, like 0 or -1
            return 0;
        }
        playerScores[playerIndex] += points;

        if (scoreUI != null)
        {
            scoreUI(playerIndex).text = "Player" + playerIndex.ToString() + ": " + playerScores[playerIndex].ToString();
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
