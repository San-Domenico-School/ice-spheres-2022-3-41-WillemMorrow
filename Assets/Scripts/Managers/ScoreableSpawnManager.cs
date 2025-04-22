using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**********************************************
 * class responsible for spawning in scoreables.
 * 
 * component of Scoreable Spawn Manager
 * 
 * Pacifica Morrow
 * 04.22.2025
 * *******************************************/

public class ScoreableSpawnManager : MonoBehaviour
{
    public static ScoreableSpawnManager Singleton;

    [Header("spawn fields")]
    [SerializeField] private GameObject[] scoreablePrefabs; // list of spawnable scorables collectables
    [SerializeField] private int spawnInterval; // the interval between scorable spawns
    [SerializeField] private int spawnDelay; // the delay before the objects begin spawning.
    [SerializeField] private float yPos; // the position the objects spawn at
    [SerializeField] private int maxScoreables; // the max number of scorables collectables in the scene

    [Header("Island")]
    [SerializeField] private GameObject island; // reference to the play area island

    [Header("debug fields")]
    [SerializeField] private bool spawnScoreables; // whether or not powerups can spawn

    private Vector3 islandSize;
    private bool gameStarted; // whether or not to spawn powerups; activated by player.
    [SerializeField] private List<GameObject> scoreablesOnScene;

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

    void Start()
    {
        // initializes the size of the island
        islandSize = island.GetComponent<MeshCollider>().bounds.size;

        // begins spawning powerups
        if (spawnScoreables)
        {
            InvokeRepeating("SpawnScoreable", spawnDelay, spawnInterval);
        }

        // invokes RemovePowerUp every 5 seconds.
        InvokeRepeating("RemovePowerUp", 0, 5);
    }

    // spawns a scoreable on a random point on the map.
    private void SpawnScoreable()
    {
        if ((scoreablesOnScene.Count < maxScoreables) && (GameManager.Singleton.alivePlayers > 0))
        {
            int powerUpIndex = Random.Range(0, scoreablePrefabs.Length);
            GameObject powerUp = scoreablePrefabs[powerUpIndex];

            GameObject instantiatedPowerup = Instantiate(powerUp, SetRandomPosition(yPos), powerUp.transform.rotation);

            scoreablesOnScene.Add(instantiatedPowerup);
        }
    }
    
    // returns a random position on the island.
    private Vector3 SetRandomPosition(float posY)
    {
        float posX = Random.Range(-(islandSize.x / 3), (islandSize.x / 3));
        float posZ = Random.Range(-(islandSize.z / 3), (islandSize.z / 3));

        return new Vector3(posX, posY, posZ);
    }

    // removes the first powerup spawned, in order to cycle those on the scene.
    private void RemovePowerUp()
    {
        if (scoreablesOnScene.Count >= maxScoreables)
        {   
            Destroy(scoreablesOnScene[0]);
        }
    }
}
