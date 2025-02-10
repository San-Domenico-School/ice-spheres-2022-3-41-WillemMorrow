using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IslandManager : MonoBehaviour
{
    public static IslandManager Singleton;


    void Awake()
    {
        // Awake is called before any Start methods are called
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

    public void SwitchLevels(string destination)
    {
        SceneManager.LoadScene(destination);
    }
}
