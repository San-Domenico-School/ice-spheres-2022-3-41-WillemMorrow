using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**********************************************
 * THE TEMPLATE CLASS FOR MAKING A POWERUP.
 * the class must inherit PowerUpBase, itself inheriting Monobehavior.
 * 
 * the class must implement OnPowerUpActivate. this will be called from the PowerUpManager.
 * ********************************************/

public class PowerUpTemplate : PowerUpBase
{
    
    // class that will be called when the player fires. 
    override public void OnPowerUpActivate()
    {
        Debug.Log("Powerup activated!");
    }
}
