using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

/******************************************
 * class responsible for managing the player's prefabs.
 * 
 * component of: the player container.
 * 
 * 
 * ****************************************/


public class PowerUpManager : MonoBehaviour
{
    private GameObject PowerupPrefab;
    private GameObject currentPowerup;
    [SerializeField] private int powerupCooldown;
    [SerializeField] private bool hasPowerUp;

    //method called by pressing the fire button
    public void OnFire(InputAction.CallbackContext context)
    {
        if (currentPowerup != null && context.started)
        {
            InvokePowerUp();
        }
    }

    // method to invoke the powerup's onFire method.
    private void InvokePowerUp()
    {
        //gets a reference to all the powerups in the child gameObjects.
        PowerUpBase[] powerUps = GetComponentsInChildren<PowerUpBase>();

        // for every instance of the powerup component,
        foreach (var powerUp in powerUps)
        {
            // creates a list of all of the methods in the component
            var type = powerUp.GetType();
            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            // for every method in the component,
            foreach (var method in methods)
            {
                // if the component's name is OnPowerUpActivate (OnPowerUpActivate()),
                if (method.Name == "OnPowerUpActivate")
                {
                    // invoke that method.
                    method.Invoke(powerUp, null);
                }
            }
        }
    }

    // if the player collides with a powerup, give the player whatever powerup the object is.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp") && (!hasPowerUp))
        {
            PowerUpControler powerUpControler = other.GetComponent<PowerUpControler>();

            PowerupPrefab = powerUpControler.GetPrefab();
            powerupCooldown = powerUpControler.GetCooldown();

            setPowerUp();

            Destroy(other.gameObject);
        }
    }

    // sets up the powerup and calls the coroutine to remvoe it
    private void setPowerUp()
    {
        hasPowerUp = true;
        currentPowerup = Instantiate(PowerupPrefab, transform.position, transform.rotation, this.transform);

        StartCoroutine(PowerupCooldown());
    }

    // coroutine responsible for "turning off" the powerup.
    private IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerupCooldown);

        Destroy(currentPowerup);

        currentPowerup = null;
        PowerupPrefab = null;
        hasPowerUp = false;
    }
}
