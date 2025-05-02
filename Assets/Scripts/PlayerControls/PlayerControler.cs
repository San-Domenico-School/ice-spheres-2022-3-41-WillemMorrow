using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

/*******************************************
 * Class responsible for controling the player & administering powerups.
 * 
 * component of the Player.
 * 
 * 01.10.2025
 * Pacifica Morrow
 * ****************************************/

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private float moveForceMagnitude;
    [SerializeField] private Transform focalPoint;
    [SerializeField] private Light powerUpIndicator;
    [SerializeField] private int[] portalIgnoredLayers;

    private GameObject windmall;

    private Rigidbody rb;
    private GameObject player;
    private SphereCollider playerCollider;

    private PlayerInputActions playerInputActions;
    private Death death;
    private PlayerContainer containerClass;
    private PlayerScore playerScore;

    private Vector2 moveVector;
    private Vector3 startPos;    

    public bool hasPowerUp {  get; private set; }
    private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        windmall = GameObject.Find("Windmall");
        hasPowerUp = GameManager.Singleton.debugPowerUpRepel;
        startPos = transform.position;

        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<SphereCollider>();
        powerUpIndicator = GetComponent<Light>();
        death = GetComponentInParent<Death>();
        containerClass = GetComponent<PlayerContainer>();
        playerScore = GetComponent<PlayerScore>();

        playerCollider.material.bounciness = 0.4f;

        player = this.gameObject;
        player.SetActive(false);

        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    // OnDisable is called when the player is disabled, aka when it "dies".
    private void OnDisable()
    {
        transform.position = startPos;
        rb.velocity = Vector3.zero;
    }

    private void OnEnable()
    {
        AssignLevelValues();
    }


    // called by the Player Input component of the player, sets the player's moveVector to the value of the movement keys via calling SetMoveVector.
    public void OnInputAction(InputAction.CallbackContext ctx) => SetMoveVector(ctx.ReadValue<Vector2>());

    private void SetMoveVector(Vector2 value)
    {
        moveVector = value;
    }

    // assigns the player's values with those of the GameManager for the current level
    private void AssignLevelValues()
    {
        transform.localScale = GameManager.Singleton.playerScale;
        rb.mass = GameManager.Singleton.playerMass;
        rb.drag = GameManager.Singleton.playerDrag;
        moveForceMagnitude = (GameManager.Singleton.playerMoveForce) * 10;

    }

    private void Move()
    {
        if (focalPoint != null)
        {
            rb.AddForce((focalPoint.forward.normalized * moveVector.y) * moveForceMagnitude * Time.deltaTime);
            rb.AddForce((focalPoint.right.normalized * moveVector.x) * moveForceMagnitude * Time.deltaTime);
        }

        else
        {
            focalPoint = GameObject.Find("FocalPoint").transform;
            Debug.Log($"focalpoint = {focalPoint.name}");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AssignLevelValues();
    }

    /*
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            windmall.layer = LayerMask.NameToLayer("Default");
            AssignLevelValues();
            playerCollider.material.bounciness = GameManager.Singleton.playerBounce;
        }
    }*/

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            SwitchGrounded(true);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            SwitchGrounded(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Portal") && !(portalIgnoredLayers.Contains<int>(gameObject.layer)))
        {
            Debug.Log(gameObject.layer);

            gameObject.layer = LayerMask.NameToLayer("Portal");
        }

        /*
        if (other.gameObject.CompareTag("PowerUp"))
        {
            PowerUpControler otherPowerUpControler = other.GetComponent<PowerUpControler>();
            float otherPowerUpCooldown = otherPowerUpControler.GetCooldown();

            
            StartCoroutine(powerUpCooldown(otherPowerUpCooldown));

            other.gameObject.SetActive(false);
        }*/

        if (other.gameObject.CompareTag("KillPlane"))
        {
            death.PlayerDeath();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if the gameobject which the player is colldiign with is a portal, and the player's CURRENT layer isnt one that can enter the portal.
        if (other.gameObject.CompareTag("Portal") && !(portalIgnoredLayers.Contains<int>(gameObject.layer)))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");

            // If, when the player leaves the portal, it is under the portal, IE it has gone through the portal,
            if (transform.position.y < -1.0f)
            {
                //get a reference to the playercontorler
                PortalController portalController = other.GetComponent<PortalController>();

                //give the portal's destination to the islandmanager, and load that scene.
                string portalDestination = portalController.GetDestination();
                IslandManager.Singleton.SwitchLevels(portalDestination);
                
                // reset the player's position and velocity.
                rb.velocity = Vector3.zero;
                transform.position = Vector3.up * 25;

                //add the respective score to the player.
                int portalScoreAdded = portalController.GetScore();
                playerScore.OnPortalEnter(portalScoreAdded);
            }
        }
    }

    private void SwitchGrounded(bool onGround)
    {
        grounded = onGround;

    }

    /* coroutine "PowerUpCooldown" (handled by PowerUpManager now)       
    private IEnumerator powerUpCooldown(float cooldown)
    {
        hasPowerUp = true;
        powerUpIndicator.intensity = 5.0f;

        yield return new WaitForSeconds(cooldown);

        hasPowerUp = false;
        powerUpIndicator.intensity = 0.0f;
    }*/
}
