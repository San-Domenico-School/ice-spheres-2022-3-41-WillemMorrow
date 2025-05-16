using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**********************************************
 * THE TEMPLATE CLASS FOR MAKING A POWERUP.
 * 
 * The class must inherit PowerUpBase, itself inheriting Monobehavior.
 * 
 * The class must implement OnPowerUpActivate. this will be called from the PowerUpManager, 
 * whenever the player presses the fore button.
 * ********************************************/

public class PowerUpTemplate : PowerUpBase
{
    
    // method that will be called when the player fires. 
    override public void OnPowerUpActivate()
    {
        Debug.Log("Powerup activated!");
    }
}
