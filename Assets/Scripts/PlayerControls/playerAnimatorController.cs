using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************
 * class responsible for managing the playerControler.
 * 
 * Component of the Player.
 * 
 * Pacifica Morrow
 * 24.04.2025
 * **********************************************/


public class playerAnimatorController : MonoBehaviour
{
    [SerializeField] private bool animateFalling;
    [SerializeField] private Animator playerAnimator;
    
    [SerializeField] private bool grounded = false;
    private int speed;

    private Rigidbody playerRb;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        InvokeRepeating("setSpeed", 0, 0.1f);
    }

    private void setSpeed()
    {
        //speed = (int)playerRb.velocity.magnitude;
        Vector3 velocity = playerRb.velocity;
        speed = (int)Mathf.Sqrt((Mathf.Pow(velocity.x, 2)) + 0 + (Mathf.Pow(velocity.z, 2)));
        playerAnimator.SetInteger("SpeedXY", speed);
    }

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

    // switches whether the player is grounded and sets the animator so that it should be falling.
    private void SwitchGrounded(bool onGround)
    {
        if (animateFalling)
        {
            grounded = onGround;
            playerAnimator.SetBool("Grounded", onGround);
        }
    }
}
