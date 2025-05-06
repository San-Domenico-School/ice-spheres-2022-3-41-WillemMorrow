using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

/**************************************
 * class responsible for handling the player leaving/joining.
 * handles player death, respawn timer, and respawning.
 * 
 * component of the playerContainer prefab.
 * 
 * Pacifica Morrow
 * 03.06.2025
 * **************************************/

public class PlayerContainer : MonoBehaviour
{
    [Header("Editable Fields")]
    [SerializeField] private GameObject player;
    public int respawnCooldown;

    [Header("Debug Fields; do not edit.")]
    [SerializeField] private int respawnCountdownRemaining;

    private bool onRespawnCooldown;
    
    // color fields
    private bool colorChosen;
    private int playerColor = 4;

    private ColorPicker colorPicker;


    private void Start()
    {
        colorPicker = GetComponent<ColorPicker>();
        respawnCountdownRemaining = respawnCooldown;
        DontDestroyOnLoad(gameObject);
    }

    //called by Player Input component, sends vector2 to SetColorVector.
    public void OnColorAction(InputAction.CallbackContext ctx) => SetColorVector(ctx.ReadValue<Vector2>());

    ///outdated -- string approach
    // called by PlayerInput component, sends a string containing the button path (which button is pressed) to the SetColor method
    //public void OnColorAction(InputAction.CallbackContext ctx) => SetColor(ctx.control.path);


    public void OnStart(InputAction.CallbackContext ctx)
    {
        if (colorChosen)
        {
            SpawnPlayer();
        }

        else
        {
            StartPlayer();

            colorChosen = true;
        }
    }

    /*private void SetColorVector(Vector2 compositeXYAB)
    {
        if (!colorChosen)
        {
            if (compositeXYAB == Vector2.up) //if the butten pressed is north
            {
                playerColor = 0; //the color is the first one in the ColorPicker component.
            }

            else if (compositeXYAB == Vector2.down) //if the butten pressed is south
            {
                playerColor = 1; //the color is the second one in the ColorPicker component.
            }

            else if (compositeXYAB == Vector2.left) //if the button pressed is west
            {
                playerColor = 2; //the color is the third one in the ColorPicker component.
            }

            else if (compositeXYAB == Vector2.right) //if the button pressed is east
            {
                playerColor = 3; //the color is the fourth one in the ColorPicker component.
            }

            colorChosen = true;
        }
    }*/

    // SWITCH VECTOR2 APPROACH
    private void SetColorVector(Vector2 compositeXYAB)
    {
        if (!colorChosen && compositeXYAB != Vector2.zero)
        {
            Debug.Log(compositeXYAB.ToString());

            switch (compositeXYAB.ToString())
            {
                case ("(0.00, 1.00)"):
                    // the player is the Senior's.
                    // player color is 0, aka the first one listed in the ColorPicker component.
                    playerColor = 0;
                    gameObject.name = "Senior";
                    break;
                case ("(0.00, -1.00)"):
                    // the player is the Freshman's
                    // player color is 3, aka the fourth one listed in the ColorPicker component.
                    playerColor = 3;
                    gameObject.name = "Freshman";
                    break;
                case ("(-1.00, 0.00)"):
                    // the player is the Junior's
                    // player color is 1, aka the second one listed in the ColorPicker component.
                    playerColor = 1;
                    gameObject.name = "Junior";
                    break;
                case ("(1.00, 0.00)"):
                    // the player is the Soph's
                    // player color is 3, aka the fourth one listed in the ColorPicker component.
                    playerColor = 2;
                    gameObject.name = "Sophomore";
                    break;

                // UNEXPECTED BUTTON INPUT.
                default:
                    // playercolor is white; default.
                    Debug.LogWarning("PlayerContainer_130 Unexpected Input!");
                    playerColor = 4;
                    break;
            }
        }
    }

    /*
     * BUTTON PATH STRING APPROACH
    // assigns a color based on what button was pressed. 
    // see switch cases for which button is which color.
    private void SetColor(string buttonPath)
    {
        if (!colorChosen)
        {
            Debug.Log(buttonPath);

            switch (buttonPath)
            {
                // CLASS CONTROLLER
                case "/DualShock4GamepadHID/buttonNorth":
                    // the player is the Senior's.
                    // player color is 0, aka the first one listed in the ColorPicker component.
                    playerColor = 0;
                    break;

                case "/DualShock4GamepadHID/buttonSouth":
                    // the player is the Freshman's
                    // player color is 3, aka the fourth one listed in the ColorPicker component.
                    playerColor = 3;
                    break;

                case "/DualShock4GamepadHID/buttonWest":
                    // the player is the Junior's
                    // player color is 1, aka the second one listed in the ColorPicker component.
                    playerColor = 1;
                    break;

                case "/DualShock4GamepadHID/buttonEast":
                    // the player is the Soph's
                    // player color is 3, aka the fourth one listed in the ColorPicker component.
                    playerColor = 2;
                    break;

                // UNEXPECTED BUTTON INPUT.
                default:
                    // playercolor is white; default.
                    Debug.LogWarning("Unexpected Input!");
                    playerColor = 4;
                    break;
            }
        }
    } 
    */
    
    // enables the player without changing the color
    private void SpawnPlayer()
    {
        if (!onRespawnCooldown)
        {
            player.SetActive(true);
        }
    }

    // enables the player and sets its color to whatever colour they selecred.
    private void StartPlayer()
    {
        player.SetActive(true);

        Renderer renderer = player.GetComponentInChildren<Renderer>();
        renderer.material.color = GetComponent<ColorPicker>().GetColor(playerColor);
    }

    // Called by the player, when it falls too far off the map. 
    private void PlayerDeath()
    {
        // disables the player, simulating death.
        player.SetActive(false);

        // sets onRespawnCooldown to indicate the player cannot respawn.
        onRespawnCooldown = true;

        // invokes respawnCountdown() every second.
        InvokeRepeating("respawnCountdown", 0, 1);
    }

    // subtracts 1 from the player's respawn timer every time its envoked, until the respawn timer is 0. 
    private void respawnCountdown()
    {
        // subtract from the respawn cooldown if it is above 0.
        if (respawnCountdownRemaining > 0)
        {
            respawnCountdownRemaining--;
        }

        // if countdown below zero: cancel the counting down, reset the timer, and cancelinvoke the method.
        else
        {
            CancelInvoke("respawnCountdown");

            onRespawnCooldown = false;
            respawnCountdownRemaining = respawnCooldown;
        }

    }

}