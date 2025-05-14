using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/*****************************************
 * class responsible for the size increase powerup.
 * 
 * component of the size increase powerup INFLUENCER
 * 
 * Pacifica Morrow
 * 28.04.2025
 * **************************************/

public class SizeIncreasePwrUp : PowerUpBase
{
    [SerializeField] private int sizeIncrease;
    [SerializeField] private float PowerUpTimer;
    [SerializeField] private int jumpForce;
    [SerializeField] private int massIncrease;
    private GameObject playerParent;
    private GameObject playerModel;

    private SphereCollider playerCollider;
    private Vector3 playerSize;
    private float playerMass;
    private float colliderRadius;

    private Rigidbody playerRb;

    private void Start()
    {
        PlayerContainer playerContainer = GetComponentInParent<PlayerContainer>();
        playerParent = playerContainer.GetPlayer(0); //gets the Player gameobject inside the PlayerContainer; the object handling player collisions.
        playerModel = playerContainer.GetPlayer(1); //gets the player's model.
        playerCollider = playerParent.GetComponent<SphereCollider>();

        playerSize = playerModel.transform.localScale;
        playerRb = playerParent.GetComponent<Rigidbody>();
        playerMass = playerRb.mass;
        colliderRadius = playerCollider.radius;

        Vector3 adjustedSize = playerModel.transform.localScale * sizeIncrease;
        float adjustedMass = playerRb.mass * massIncrease;
        float adjustedRadius = ((playerCollider.radius) * (sizeIncrease));

        playerCollider.radius = adjustedRadius;
        playerModel.transform.localScale = adjustedSize;
        playerRb.mass = adjustedMass;

        playerParent.layer = 10;

        StartCoroutine("PowerUpCntDwn");
    }

    // class that will be called when the player fires. 
    override public void OnPowerUpActivate()
    {
        playerRb.AddForce((Vector3.up * jumpForce), ForceMode.Impulse);
    }

    private IEnumerator PowerUpCntDwn()
    { 
        yield return new WaitForSeconds(PowerUpTimer);

        playerCollider.radius = colliderRadius;
        playerParent.layer = 06;
        playerModel.transform.localScale = playerSize;
        playerRb.mass = playerMass;
    }
}
