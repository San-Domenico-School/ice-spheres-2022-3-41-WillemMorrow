using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************
 * manages the spawning and total number of prefabs on the scene. 
 * 
 * Component of the PowerUpSpawnManager
 * 
 * Pacifica Morrow
 * 04.01.2025
 * *********************************************/

public class PowerUpSpawnManager : MonoBehaviour
{
    public static PowerUpSpawnManager Singleton; // static reference to the class

    [Header("spawn fields")]
    [SerializeField] private GameObject[] powerUpPrefabs; // list of spawnable powerup collectables
    [SerializeField] private int spawnInterval; // the interval between powerup spawns
    [SerializeField] private int spawnDelay; // the delay before the objects begin spawning.
    [SerializeField] private float yPos; // the position the objects spawn at
    [SerializeField] private int maxPowerUps; // the max number of powerup collectables in the scene

    [Header("Island")]
    [SerializeField] private GameObject island; // reference to the play area island

    [Header("debug fields")]
    [SerializeField] private bool spawnPowerups; // whether or not powerups can spawn

    private Vector3 islandSize;
    private bool gameStarted; // whether or not to spawn powerups; activated by player.
    [SerializeField] private List<GameObject> powerUpsOnScene;

    // makes sure there is only one PowerUpSpawnManager in the scene.
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

    // Start is called before the first frame update
    void Start()
    {
        // initializes the size of the island
        islandSize = island.GetComponent<MeshCollider>().bounds.size;

        // begins spawning powerups
        if (spawnPowerups)
        {
            InvokeRepeating("SpawnPowerUp", spawnDelay, spawnInterval);
        }

        // invokes RemovePowerUp every 10 seconds.
        InvokeRepeating("RemovePowerUp", 0, 10);
    }

    private void Update()
    {
        // removes all null elements from the list of total powerup collectables on scene, so that the max number of powerups can work properly.
        //if (powerUpsOnScene.Contains(null)) // doesnt work??????
        //{
            powerUpsOnScene.RemoveAll(obj => obj == null);
        //}

        // if there are more powerups than the max on the scene, the first one will be destroyed.
        if (powerUpsOnScene.Count > maxPowerUps)
        {
            Destroy(powerUpsOnScene[0]);
        }


    }

    // spawns a ranodom powerup from the list of powerup prefabs.
    private void SpawnPowerUp()
    {
        if ((powerUpsOnScene.Count < maxPowerUps) && (GameManager.Singleton.alivePlayers > 0))
        {
            int powerUpIndex = Random.Range(0, powerUpPrefabs.Length);
            GameObject powerUp = powerUpPrefabs[powerUpIndex];

            GameObject instantiatedPowerup = Instantiate(powerUp, SetRandomPosition(yPos), powerUp.transform.rotation);

            powerUpsOnScene.Add(instantiatedPowerup);
        }
    }

    // returns a random location on the island
    private Vector3 SetRandomPosition(float posY)
    {
        float posX = Random.Range(-(islandSize.x / 3), (islandSize.x / 3));
        float posZ = Random.Range(-(islandSize.z / 3), (islandSize.z / 3));

        return new Vector3(posX, posY, posZ);
    }

    // removes the first powerup on the list of powerups on scene.
    // purpose is to remove a powerup so that a fresh one can spawn, potentially diversifying the powerup options.
    private void RemovePowerUp()
    {
        if (powerUpsOnScene.Count >= maxPowerUps)
        {
            Destroy(powerUpsOnScene[0]);
        }
    }
}
