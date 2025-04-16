using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

/**************************************************
 * Player UI for scoreboard and timer
 * Attached to PlayerUI
 * 
 * Sebastian Balakier
 * 3/21/25 Version 1.0
 **************************************************/

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text[] uiTextBoxes = new TMP_Text[5];
    private float Countdown = 480f;
    private float secondAccumulator = 0f;
    private int[] score = new int[4];

    // Start is called before the first frame update
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Load UI when changing levels
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Unload UI when changing levels
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded");
    }

    // Update is called once per frame - timer deduction and game end
    void Update()
    {
        if (Countdown > 0f)

        {
            secondAccumulator += Time.deltaTime;

            if (secondAccumulator >= 1f)
            {
                Countdown -= 1f;
                secondAccumulator = 0f;
                UpdateTimerDisplay();
            }
        }

        if (Countdown <= 0f)
        {
            Countdown = 0f;
            Time.timeScale = 0f;
            uiTextBoxes[4].text = "Time is up!";
            Debug.Log("TIme is up");
        }
    }

    //timer format and text
    private void UpdateTimerDisplay()
    {
        float clampedTime = Mathf.Max(Countdown, 0f);
        TimeSpan time = TimeSpan.FromSeconds(clampedTime);
        string formattedTime = time.ToString(@"mm\:ss");
        uiTextBoxes[4].text = "Time: " + formattedTime;
    }
}
