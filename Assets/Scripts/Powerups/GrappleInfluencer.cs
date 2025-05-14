using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UIElements;

/*************************************************
 * literally just a copy of the playermagnet but only acting on the player
 * 
 * component of the playerMagnet influencer prefab.
 * 
 * Pacifica Morrow
 * 04.01.2025
 * **********************************************/


public class GrappleInfluencer : PowerUpBase
{
    [Header("Light Fields")]
    [SerializeField] private float lightOffIntensity;
    [SerializeField] private float lightOnIntensity;

    /*
    [Header("Thruster Fields")]
    [SerializeField] private float speedOn;
    [SerializeField] private float speedOff;
    */

    [Header("Magnet Power Fields")]
    [SerializeField] private int magnetStrength;
    [SerializeField] private float otherMagnetMult;

    private GameObject playerParent;
    private Rigidbody playerRb;
    private ParticleSystem thrusterParticles;

    private bool magnetEnabled;
    private Light magnetLight;

    // Start initializes fields and sets up the powerup, on its instantiation.
    void Start()
    {
        PlayerContainer playerContainer = GetComponentInParent<PlayerContainer>();
        playerParent = playerContainer.GetPlayer(0);
        playerRb = playerParent.GetComponent<Rigidbody>();
        //thrusterParticles = GetComponentInChildren<ParticleSystem>();
        
        magnetLight = GetComponent<Light>();
        Color lightColor = playerContainer.GetPlayerColor();
        magnetLight.color = lightColor;

        //Material particlesMaterial = thrusterParticles.GetComponent<Renderer>().material;
        //particlesMaterial.color = lightColor;
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
        // if the magnet is on already
        if (magnetEnabled)
        {
            magnetEnabled = false;
            magnetLight.intensity = lightOnIntensity;

            /*
            var particleMain = thrusterParticles.main.startSpeed;
            particleMain = (speedOff);
            Debug.Log(particleMain);
            */
        }

        // if the magnet is off
        else
        {
            magnetEnabled = true;
            
            magnetLight.intensity = lightOffIntensity;

            /*
            var particleMain = thrusterParticles.main.startSpeed;
            particleMain = speedOn;
            */
        }
    }

    // acts every frame object(s) are colliding with any trigger of the object.
    private void OnTriggerStay(Collider other)
    {
        //applies move force to (player) & (powerup or other player)
        if ((other.CompareTag("Player") || (other.CompareTag("PowerUp")) || (other.CompareTag("Scoreable"))) && (magnetEnabled))
        {
            Rigidbody otherRb = other.GetComponent<Rigidbody>();

            Vector3 pushDirection = other.transform.position - transform.position;

            playerRb.AddForce(pushDirection * magnetStrength * Time.deltaTime);
            otherRb.AddForce(-pushDirection * magnetStrength * otherMagnetMult * Time.deltaTime);
        }
    }
}
