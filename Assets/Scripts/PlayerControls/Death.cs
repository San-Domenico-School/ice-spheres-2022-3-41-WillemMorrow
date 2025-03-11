using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private PlayerContainer playerContainer;
    [SerializeField] private GameObject playerModel;
    
    private bool onRespawnCooldown;
    private int respawnCountdown;
    
    
    // Start is called before the first frame update
    // Called by the player, when it falls too far off the map. 
    private void PlayerDeath()
    {
        // disables the player, simulating death.
        playerModel.SetActive(false);

        // sets onRespawnCooldown to indicate the player cannot respawn.
        onRespawnCooldown = true;

        // invokes respawnCountdown() every second.
        InvokeRepeating("RespawnCountdown", 0, 1);
    }

    // subtracts 1 from the player's respawn timer every time its envoked, until the respawn timer is 0. 
    private void RespawnCountdown()
    {
        // subtract from the respawn cooldown if it is above 0.
        if (respawnCountdown > 0)
        {
            respawnCountdown--;
        }

        // if countdown below zero: cancel the counting down, reset the timer, and cancelinvoke the method.
        else
        {
            CancelInvoke("respawnCountdown");

            onRespawnCooldown = false;
            respawnCountdown = playerContainer.respawnCooldown;
        }

    }
}
