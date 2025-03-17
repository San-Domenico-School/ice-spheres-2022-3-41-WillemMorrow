using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [Header("Editable Fields")]
    [SerializeField] private PlayerContainer playerContainer;
    [SerializeField] private GameObject playerModel; 
    public int respawnCooldown; // the full ORIGINAL cooldown, set before starting the game. 

    [Header("Debug Fields; do not edit.")]
    [SerializeField] private int respawnCountdown; // the respawn cooldown that gets counted down.

    // sets fields upon the player joining.
    private void Start()
    {
        respawnCountdown = respawnCooldown;
    }

    // Start is called before the first frame update
    // Called by the player, when it falls too far off the map. 
    public void PlayerDeath()
    {
        // disables the player, simulating death.
        playerModel.SetActive(false);

        // sets onRespawnCooldown to indicate the player cannot respawn.
        playerContainer.onRespawnCooldown = true;

        // invokes respawnCountdown() every second.
        InvokeRepeating("RespawnCountdown", 0, 1);

        Debug.Log($"Player {playerModel.name} has Died!");
    }
    
    // subtracts 1 from the player's respawn timer every time its envoked, until the respawn timer is 0. 
    private void RespawnCountdown()
    {
        Debug.Log("RespawnCountdown() called!");

        // subtract from the respawn cooldown if it is above 0.
        if (respawnCountdown > 0)
        {
            // IMPLIMENT UPDATING THE UI

            respawnCountdown--;
        }

        // if countdown below zero: cancel the counting down, reset the timer, and cancelinvoke the method.
        else
        {
            CancelInvoke("RespawnCountdown");

            respawnCountdown = respawnCooldown;

            // sets the onRespawnCooldown in the PLAYER CONTAINER to false. Player can now respawn.
            playerContainer.onRespawnCooldown = false; 
        }

    }
}
