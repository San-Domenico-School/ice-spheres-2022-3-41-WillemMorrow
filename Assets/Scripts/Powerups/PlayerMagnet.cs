using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/*************************************************
 * class responsible for making the player magnet work.
 * 
 * component of the playerMagnet influencer prefab.
 * 
 * Pacifica Morrow
 * 04.01.2025
 * **********************************************/


public class PlayerMagnet : PowerUpBase
{
    [Header("Light Fields")]
    [SerializeField] private float lightOffIntensity;
    [SerializeField] private float lightOnIntensity;

    [Header("Magnet Power Fields")]
    [SerializeField] private int magnetStrength;
    [SerializeField] private int playerMagnetMult;

    private GameObject playerParent;
    private Rigidbody playerRb;

    private bool magnetEnabled;
    private Light magnetLight;

    // Start initializes fields and sets up the powerup, on its instantiation.
    void Start()
    {
        PlayerContainer playerContainer = GetComponentInParent<PlayerContainer>();
        playerParent = playerContainer.GetPlayer();
        playerRb = playerParent.GetComponent<Rigidbody>();

        magnetLight = GetComponent<Light>();
        Color lightColor = playerParent.GetComponentInChildren<Renderer>().material.color;
        magnetLight.color = lightColor;
    }

    // gets called by the player pressing the fire button.
    // on controller, it gets called when the player presses the fire button and when they release the fire button.
    public override void OnPowerUpActivate()
    {
        ToggleMagnet();
    }

    // toggles whether the magnet is enabled
    // in practice, turns the magnet on/off when the player is holding down the fire button.
    private void ToggleMagnet()
    {
        if (magnetEnabled)
        {
            magnetEnabled = false;
            magnetLight.intensity = lightOnIntensity;
        }

        else
        {
            magnetEnabled = true;
            magnetLight.intensity = lightOffIntensity;
        }
    }

    // acts every frame object(s) are colliding with any trigger of the object.
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PowerUp") && (magnetEnabled))
        {
            Rigidbody otherRb = other.GetComponent<Rigidbody>();

            Vector3 pushDirection = other.transform.position - transform.position;

            otherRb.AddForce(-pushDirection * magnetStrength * Time.deltaTime);
        }

        else if ((other.CompareTag("Player")) && magnetEnabled)
        {
            Rigidbody otherRb = other.GetComponent<Rigidbody>();

            Vector3 pushDirection = other.transform.position - transform.position; // a vector pointing in the direction of other

            playerRb.AddForce(pushDirection * magnetStrength * playerMagnetMult * Time.deltaTime);
            otherRb.AddForce(-pushDirection * magnetStrength * Time.deltaTime);
        }

        //outdated? 
        //applies move force to (player) & (powerup or other player)
        /*if ((other.CompareTag("Player") || (other.CompareTag("PowerUp"))) && (magnetEnabled))
        {
            Rigidbody otherRb = other.GetComponent<Rigidbody>();

            Vector3 pushDirection = other.transform.position - transform.position;

            playerRb.AddForce(pushDirection * magnetStrength * Time.deltaTime);
            otherRb.AddForce(-pushDirection * magnetStrength * playerMagnetMult * Time.deltaTime);
        }*/
    }
}
