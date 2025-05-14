using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Singleton;

    [SerializeField] private TextMeshProUGUI timeTxt;   //reference to the timer UI
    public int time;                                    //the time remaining in the game.

    private void Awake()
    {
        //If instance variable doesn't exist, assign this object to it
        if (Singleton == null)
        {
            Singleton = this;
        }

        //Otherwise, if the instance variable does exist, but it isn't this object, destroy this object.
        else if (Singleton != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        // multiply the time up to 60.
        time *= 60;

        //begin subtracting time.
        InvokeRepeating("SubtractTime", 0, 1);
    }

    // subtract time and send the resultant string to DisplayTime();
    private void SubtractTime()
    {
        if (time <= 0)
        {
            Time.timeScale = 0;
            return;
        }

        time--;

        // convert the total time in seconds to minutes & seconds.
        int timeMinutes = (time / 60);
        int timeSeconds = (time % 60);

        // declare the string fields
        string minutesString = timeMinutes.ToString();
        string secondsString;

        // if theres under 10 seconds, put a 0 in front of the second number.
        if (timeSeconds < 10)
        {
            secondsString = ("0" + timeSeconds.ToString());
        }
        else
        {
            secondsString = timeSeconds.ToString();
        }

        //send the time to DisplayTime
        DisplayTime(minutesString, secondsString);
    }

    //display the time on the timer.
    private void DisplayTime(string minutes, string seconds)
    {
        //set the UI.
        timeTxt.text = (minutes + ":" + seconds);
    }
}
