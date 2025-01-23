using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

/***************************************
 * Manages the game
 * 
 * Component of: GameManager
 * 
 * Pacifica Morrow
 * 01.16.2025
 * ************************************/


public class SpawnManager : MonoBehaviour
{
    [Header("Objects To Spawn")]
    [SerializeField] GameObject iceSphere;
    [SerializeField] GameObject portal;
    [SerializeField] GameObject powerUp;

    [Header("Wave Fields")]
    [SerializeField] private int initialWave;
    [SerializeField] private int increaseEachWave;
    [SerializeField] private int maximumWave;

    [Header("Portal Fields")]
    [SerializeField] private int portalFirstAppearence;
    [SerializeField] private float portalByWaveProbability;
    [SerializeField] private float portalByWaveDuration;

    [Header("Island")]
    [SerializeField] private GameObject island;

    private Vector3 islandSize;
    private int waveNumber;
    private bool portalActive;
    private bool powerUpActive;

    // Start is called before the first frame update
    void Start()
    {
        islandSize = island.GetComponent<MeshCollider>().bounds.size;
        waveNumber = initialWave - 1;
    }

    // Update is called once per frame
    void Update()
    {

        if ((FindObjectsOfType<IceSphereControler>().Length == 0.0f) && (GameObject.Find("Player")))
        {
            SpawnIceWave();
        }
    }

    private void SpawnIceWave()
    {
        for (int i = 0; i < (waveNumber + increaseEachWave);)
        {
            Instantiate(iceSphere, SetRandomPosition(0), iceSphere.transform.rotation);

            Debug.Log("i = " + i + "; waveNumber == " + waveNumber + "; NumSpawned == " + (waveNumber + increaseEachWave));

            if (1 <= maximumWave)
            {
                i++;
            }
        }

        waveNumber++;
    }

    private void SetObjActive(GameObject obj, float waveProbability)
    {

    }

    private Vector3 SetRandomPosition(float posY)
    {
        float posX = Random.Range(-(islandSize.x/2), (islandSize.x/2));
        float posZ = Random.Range(-(islandSize.y/2), (islandSize.y/2));

        return new Vector3(posX, posY, posZ);
    }

    IEnumerator CountdownTimer(string objectTag)
    {
        yield return new WaitForSeconds(1.0f);
    }
}
