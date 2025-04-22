using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions.Must;

/***************************************
 * Manages the spawning of objects
 * 
 * Component of: SpawnManager
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

    [Header("Power-Up Fields")]
    [SerializeField] private int powerUpFirstAppearence;
    [SerializeField] private float powerUpByWaveProbability;
    [SerializeField] private float powerUpByWaveDuration;

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

        if (GameManager.Singleton.debugSpawnPortal)
        {
            portalByWaveDuration = 99;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if ((FindObjectsOfType<IceSphereControler>().Length == 0.0f) && (GameObject.Find("player")))
        {
            SpawnIceWave();
        }

        if ((waveNumber > portalFirstAppearence || GameManager.Singleton.debugSpawnPortal) && (portalActive == false))
        {
            SetObjActive(portal, portalByWaveProbability);
        }

        if (powerUp != null)
        {
            if (waveNumber > powerUpFirstAppearence || GameManager.Singleton.debugSpawnPowerUp && (powerUp.activeInHierarchy == false))
            {
                SetObjActive(powerUp, powerUpByWaveProbability);
            }
        }

    }

    private void SpawnIceWave()
    {
        for (int i = 0; i < (waveNumber + increaseEachWave);)
        {
            Instantiate(iceSphere, SetRandomPosition(0), iceSphere.transform.rotation);

            if (1 <= maximumWave)
            {
                i++;
            }
        }

        waveNumber++;
    }

    private void SetObjActive(GameObject obj, float waveProbability)
    {
        if (Random.value < waveNumber * waveProbability * Time.deltaTime || GameManager.Singleton.debugSpawnPortal)
        {
            obj.transform.position = SetRandomPosition(obj.transform.position.y);
            StartCoroutine(CountdownTimer(obj.tag));
        }
    }

    private Vector3 SetRandomPosition(float posY)
    {
        float posX = Random.Range(-(islandSize.x/3), (islandSize.x/3));
        float posZ = Random.Range(-(islandSize.z/3), (islandSize.z/3));

        return new Vector3(posX, posY, posZ);
    }

    IEnumerator CountdownTimer(string objectTag)
    {
        float byWaveDuration = 10;

        switch (objectTag)
        {
            case "Portal":
                portal.SetActive(true);
                portalActive = true;
                byWaveDuration = portalByWaveDuration;
                break;

            case "PowerUp":
                powerUp.SetActive(true);
                powerUpActive = true;
                byWaveDuration = powerUpByWaveDuration;
                break;
        }

        yield return new WaitForSeconds(waveNumber * byWaveDuration);

        switch (objectTag)
        {
            case "Portal":
                portal.SetActive(false);
                portalActive = false;
                break;

            case "PowerUp":
                powerUp.SetActive(false);
                powerUpActive = false;
                break;
        }

    }
}
