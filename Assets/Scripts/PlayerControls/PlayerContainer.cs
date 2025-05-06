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
 * 04.22.2025
 * **************************************/

public class PlayerContainer : MonoBehaviour
{
    [Header("Editable Fields")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerModel;
    

    public bool onRespawnCooldown; // if the player can respawn or not; if false, player can respawn.
    private bool playerAlive; // whether the player is alive.
    
    // color fields
    private bool colorChosen; // whether or not the color has been chosen, aka if the plaer has started.
    private int playerColor = 4; //defaults the player color to 4, or silver. see this.SetColorVector2() for details.

    private ColorPicker colorPicker; // reference to the colorpicker.
    private MaterialPicker materialPicker; // reference to the materialPicker of the player.

    private Color playerColorColor; // the player's class color expressed as a type of Color.

    // start.
    private void Start()
    {
        colorPicker = GetComponent<ColorPicker>();
        materialPicker = GetComponent <MaterialPicker>();
        DontDestroyOnLoad(gameObject);
        GameManager.Singleton.totalPlayers++;
    }

    // update()
    private void Update()
    {
        if (player.activeInHierarchy)
        {
            playerAlive = true;
        }
        
        else
        {
            playerAlive = false;
        }
    }

    //called by Player Input component, sends vector2 to SetColorVector.
    public void OnColorAction(InputAction.CallbackContext ctx) => SetColorVector(ctx.ReadValue<Vector2>());


    // Called by pressing the Start button; Start on controller, space on keyboard.
    public void OnStart(InputAction.CallbackContext ctx)
    {
        if (!playerAlive)
        {
            playerAlive = true;
            GameManager.Singleton.alivePlayers++;

            // if the color is already chosen,
            if (colorChosen)
            {
                SpawnPlayer();
            }

            // if the color hasnt been chosen,
            else
            {
                StartPlayer();

                colorChosen = true;
            }
        }
    }


    // SWITCH VECTOR2 APPROACH -- **FINAL**
    // gets a vector2 from xyab, uses the vector2 to apply the color.
    private void SetColorVector(Vector2 compositeXYAB)
    {
        if (!colorChosen && compositeXYAB != Vector2.zero)
        {
            Debug.Log(compositeXYAB.ToString());

            // switch statement deciding what to do given a Vector2.ToString (a vector2 expressed as a string)
            // click arrow to open
            switch (compositeXYAB.ToString())
            {
                case ("(0.00, 1.00)"):
                    // the player is the Senior's.
                    // player color is 0, aka the first one listed in the ColorPicker component.
                    playerColor = 0;
                    this.gameObject.name = ("Senior");
                    player.name = ("SeniorPlayer");
                    break;
                case ("(0.00, -1.00)"):
                    // the player is the Freshman's
                    // player color is 3, aka the fourth one listed in the ColorPicker component.
                    playerColor = 3;
                    this.gameObject.name = ("Fresh");
                    player.name = ("FreshPlayer");
                    break;
                case ("(-1.00, 0.00)"):
                    // the player is the Junior's
                    // player color is 1, aka the second one listed in the ColorPicker component.
                    playerColor = 1;
                    this.gameObject.name = ("Junior");
                    player.name = ("JuniorPlayer");
                    break;
                case ("(1.00, 0.00)"):
                    // the player is the Soph's
                    // player color is 3, aka the fourth one listed in the ColorPicker component.
                    playerColor = 2;
                    this.gameObject.name = ("Soph");
                    player.name = ("SophPlayer");
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

    /* [OUTDATED] BUTTON PATH STRING APPROACH
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

    /* [OUTDATED] IF STATEMENT APPROACH
    private void SetColorVector(Vector2 compositeXYAB)
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

    // enables the player without changing the color;
    // called by OnStart if player HAS already selected a color
    private void SpawnPlayer()
    {
        if (!onRespawnCooldown)
        {
            player.SetActive(true);
            playerModel.SetActive(true);
            Debug.Log("Player Spawned!");

            Scorekeeper.Singleton.UpdateScore(playerColor, 0);
        }
    }

    // enables the player and sets its colour to whatever colour they selected.
    // called by OnStart if player HASN'T selected a color
    private void StartPlayer()
    {
        player.SetActive(true);

        // sets the player's color.
        Renderer renderer = GetComponentInChildren<Renderer>();
        playerColorColor = colorPicker.GetColor(playerColor);
        renderer.material = materialPicker.GetMaterial(playerColor);
    }

    public GameObject GetPlayer(int index)
    {
        // switch statement getting 
        switch (index.ToString())
        {
            case ("0"):
                return player;
            case ("1"):
                return playerModel;
            default:
                return null;
        }

    }

    public Color GetPlayerColor()
    {
        return playerColorColor;
    }
}