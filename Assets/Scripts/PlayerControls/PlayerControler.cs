using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

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

    private Rigidbody rb;
    private SphereCollider playerCollider;
    private Light powerUpIndicator;
    private PlayerInputActions playerInputActions;
    private float moveDirection;
    public bool hasPowerUp {  get; private set; }


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        hasPowerUp = GameManager.Singleton.debugPowerUpRepel;

        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<SphereCollider>();
        powerUpIndicator = GetComponent<Light>();
        playerCollider.material.bounciness = 0.4f;
        powerUpIndicator.intensity = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

        if (transform.position.y < -10)
        {
            IslandManager.Singleton.SwitchLevels("Island1");
            DelayAssignLevelValues();
            
            transform.position = Vector3.up * 25;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Player.Movement.performed += ctx => { SetMoveDirection(ctx.ReadValue<Vector2>()); };
        playerInputActions.Player.Movement.canceled += ctx => { moveDirection = 0.0f; };
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
    }

    private void SetMoveDirection(Vector2 value)
    {
        moveDirection = value.y;

    }

    // assigns the player's values with those of the GameManager for the current level
    private void AssignLevelValues()
    {
        transform.localScale = GameManager.Singleton.playerScale;
        rb.mass = GameManager.Singleton.playerMass;
        rb.drag = GameManager.Singleton.playerDrag;
        moveForceMagnitude = GameManager.Singleton.playerMoveForce;
        focalPoint = (GameObject.Find("FocalPoint").transform);
    }

    private void Move()
    {
        if (focalPoint != null)
        {
            rb.AddForce(focalPoint.forward.normalized * moveDirection * moveForceMagnitude * Time.deltaTime);
            Debug.Log($"Focalpoint = {focalPoint.gameObject.name}; moveDirection = {moveDirection}; moveForceMagnitude = {moveForceMagnitude}");
        }
        
        else
        {
            focalPoint = (GameObject.Find("FocalPoint").transform);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Startup"))
        {
            other.gameObject.tag = ("Ground");
            other.gameObject.layer = (7);
            playerCollider.material.bounciness = GameManager.Singleton.playerBounce;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Portal"))
        {
            gameObject.layer = LayerMask.NameToLayer("Portal");
        }

        if (other.gameObject.CompareTag("PowerUp"))
        {
            PowerUpControler otherPowerUpControler = other.GetComponent<PowerUpControler>();
            float otherPowerUpCooldown = otherPowerUpControler.GetCooldown();

            powerUpCooldown(otherPowerUpCooldown);

            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Portal"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");

            // If, when the player leaves the portal, it is under the portal, IE it has gone through the portal
            if (transform.position.y < -1.0f)
            {
                string portalDestination = other.GetComponent<PortalController>().GetDestination();
                IslandManager.Singleton.SwitchLevels(portalDestination);
                DelayAssignLevelValues();
                
                rb.velocity = Vector3.zero;
                transform.position = Vector3.up * 25;
            }
        }
    }

    private IEnumerator powerUpCooldown(float cooldown)
    {
        hasPowerUp = true;
        powerUpIndicator.intensity = 3.5f;

        yield return new WaitForSeconds(cooldown);

        hasPowerUp = false;
        powerUpIndicator.intensity = 0.0f;
    }

    private IEnumerator DelayAssignLevelValues()
    {
        yield return new WaitForFixedUpdate();

        AssignLevelValues();
    }
}
