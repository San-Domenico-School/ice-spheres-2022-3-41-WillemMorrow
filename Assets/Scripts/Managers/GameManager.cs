using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*****************************************************
 * Manages the game.
 * 
 * Component of GameManager singleton
 * 
 * Pacifica Morrow
 * 02.14.2025
 * **************************************************/


public class GameManager : MonoBehaviour
{
    public static GameManager Singleton; //public reference to a level's GameManager.

    [Header("Player Fields")]
    public Vector3 playerScale;
    public float playerMass;
    public float playerDrag;
    public float playerMoveForce;
    public float playerRepelForce;
    public float playerBounce;

    [Header("Scene Fields")]
    public GameObject[] waypoints;

    [Header("Debug Fields")]
    public bool debugSpawnWaves;
    public bool debugSpawnPortal;
    public bool debugSpawnPowerUp;
    public bool debugPowerUpRepel;

    public bool switchLevels { get; private set; }
    public bool gameOver { get; private set; }
    public bool playerHasPowerUp { get; private set; }

    public int totalPlayers;
    public int alivePlayers;

    void Awake()
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
}
