using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawnManager : MonoBehaviour
{
    [Header("portal fields")]
    [SerializeField] private string portalDestination;
    [SerializeField] private float spawnHeight;
    [SerializeField] GameObject portal;

    [Header("spawn requirements")]
    [SerializeField] int scoreReq;
    [SerializeField] int minTimeReq;
    private bool portalSpawned;

    [Header("island")]
    [SerializeField] GameObject island;
    private Vector3 islandSize;

    private void Start()
    {
        // initializes the size of the island
        islandSize = island.GetComponent<MeshCollider>().bounds.size;
    }

    // Update is called once per frame
    private void Update()
    {
        if (portalSpawned) { return; }

        CheckPortals();
    }

    private void CheckPortals()
    {
        // check to see if any of the scores are above the required score for the level.
        foreach (int score in Scorekeeper.Singleton.playerScores)
        {
            if (score > scoreReq)
            {
                SpawnPortal();
                portalSpawned = true;
                return;
            }
        }

        // if the time is under the time threshold for spawning a portal,
        int gameTime = ((GameTimer.Singleton.time / 60) + 1);

        if (gameTime < minTimeReq)
        {
            SpawnPortal();
            portalSpawned = true;
        }
    }

    private void SpawnPortal()
    {
        // instantiate the portal
        GameObject spawnedPortal = Instantiate(portal, SetRandomPosition(spawnHeight), portal.transform.rotation);
        PortalController portalScript = spawnedPortal.GetComponent<PortalController>();

        portalScript.SetDestination(portalDestination);
    }

    // returns a random location on the island
    private Vector3 SetRandomPosition(float posY)
    {
        float posX = Random.Range(-(islandSize.x / 3), (islandSize.x / 3));
        float posZ = Random.Range(-(islandSize.z / 3), (islandSize.z / 3));

        return new Vector3(posX, posY, posZ);
    }
}
