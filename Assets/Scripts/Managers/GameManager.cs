using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;

    [Header("Player Fields")]
    public Vector3 playerScale;
    public float playerMass;
    public float playerDrag;
    public float playerMoveForce;
    public float playerRepelForce;
    public float playerBounce;

    [Header("Scene Fields")]
    [SerializeField] private GameObject[] waypoints;

    [Header("Debug Fields")]
    [SerializeField] private bool debugSpawnWaves;
    [SerializeField] private bool debugSpawnPortal;
    [SerializeField] private bool debugSpawnPowerUp;
    [SerializeField] private bool debugPowerUpRepel;

    public bool switchLevels { get; private set; }
    public bool gameOver { get; private set; }
    public bool playerHasPowerUp { get; private set; }


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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnablePlayer()
    {

    }

    private void SwitchLevels()
    {

    }
}
