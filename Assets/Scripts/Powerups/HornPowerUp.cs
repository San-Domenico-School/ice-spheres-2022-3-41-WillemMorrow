using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/******************************************
 * class responsible for making the horn powerup work.
 * 
 * component of the horn powerup influencer prefab.
 * 
 * Pacifica Morrow
 * 04.01.2025
 * ***************************************/

public class HornPowerUp : PowerUpBase
{
    [SerializeField] private int pushForce;
    [SerializeField] private int cooldown;
    [SerializeField] private float lightCooldown;

    [SerializeField] private float lightOffIntensity;

    private float lightIntensity;
    private List<GameObject> applicableObjects;
    private Light indicatorLight;
    private GameObject playerParent;

    private bool onCooldown;


    private void Start()
    {
        PlayerContainer playerContainer = GetComponentInParent<PlayerContainer>();
        playerParent = playerContainer.GetPlayer();

        //change to the playercolor when player model is implimented
        indicatorLight = GetComponent<Light>();
        indicatorLight.color = playerContainer.GetPlayerColor();
        lightIntensity = indicatorLight.intensity;

        applicableObjects = new List<GameObject>();
    }

    public override void OnPowerUpActivate()
    {
        ApplyForce();
    }

    private void ApplyForce()
    {
        if (!onCooldown)
        {
            foreach (var obj in applicableObjects)
            {
                Rigidbody otherRb = obj.GetComponent<Rigidbody>();
                Vector3 otherPos = obj.transform.position;
                Vector3 thisPos = this.transform.position;
                Vector3 forceVector = otherPos - thisPos;

                float distanceMultiplier = (1/(Mathf.Sqrt((Mathf.Pow((otherPos.x - thisPos.x), 2) + (Mathf.Pow((otherPos.y - thisPos.y), 2)) + (Mathf.Pow((otherPos.z - thisPos.z), 2))))));

                otherRb.AddForce(forceVector.normalized * distanceMultiplier * pushForce, ForceMode.Impulse);

                Debug.Log(obj.name);
            }

            indicatorLight.intensity = lightOffIntensity;

            onCooldown = true;
            StartCoroutine("SetCooldown");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((other.CompareTag("Player")) || (other.CompareTag("PowerUp")) || (other.CompareTag("Scoreable"))) && (other.gameObject != playerParent))
        {
            applicableObjects.Add(other.gameObject);

            Debug.Log(other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || (other.CompareTag("PowerUp")) || (other.CompareTag("Scoreable")) && (applicableObjects.Contains(other.gameObject)))
        {
            applicableObjects.Remove(other.gameObject);
        }
    }

    // sets the horn cooldown and adjusts the light
    private IEnumerator SetCooldown()
    {
        yield return new WaitForSeconds(lightCooldown);

        indicatorLight.intensity = lightIntensity;

        yield return new WaitForSeconds(cooldown);
        
        onCooldown = false;
    }
}
