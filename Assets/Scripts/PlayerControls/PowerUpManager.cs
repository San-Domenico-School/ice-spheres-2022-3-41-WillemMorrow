using System.Collections;
using System.Collections.Generic;
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


public abstract class PowerUpManager : MonoBehaviour
{
    private GameObject PowerupPrefab;
    private GameObject currentPowerup;
    private int powerupCooldown;
    private bool hasPowerUp;


    //method called by pressing the fire button
    public void OnFire(InputAction.CallbackContext context)
    {
        if (currentPowerup != null)
        {
            InvokePowerUp();
        }
    }

    private void InvokePowerUp()
    {
        //gets a reference to all the powerups in the child gameObjects.
        PowerUpBase[] powerUps = GetComponentsInChildren<PowerUpBase>();

        foreach (var powerUp in powerUps)
        {
            MethodInfo[] methods = GetType().GetMethods(BindingFlags.Public);

            foreach (var method in methods)
            {
                if (method.Name == "OnPowerUpActivate")
                {
                    method.Invoke(powerUp, null);
                }
            }
        }
    }

    // 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            PowerUpControler powerUpControler = other.GetComponent<PowerUpControler>();

            PowerupPrefab = powerUpControler.GetPrefab();
            powerupCooldown = powerUpControler.GetCooldown();

            setPowerUp();
        }
    }

    // 
    private void setPowerUp()
    {
        hasPowerUp = true;
        currentPowerup = Instantiate(PowerupPrefab, transform.position, transform.rotation, this.transform);

        PowerupCooldown();
    }

    // 
    private IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(1);

        Destroy(PowerupPrefab);

        currentPowerup = null;
        PowerupPrefab = null;
        hasPowerUp = false;
    }
}
