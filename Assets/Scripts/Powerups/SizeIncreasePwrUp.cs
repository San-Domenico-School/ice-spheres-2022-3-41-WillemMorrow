using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SizeIncreasePwrUp : PowerUpBase
{
    [SerializeField] private int sizeIncrease;
    [SerializeField] private float PowerUpTimer;
    [SerializeField] private int jumpForce;
    [SerializeField] private int massIncrease;
    private GameObject playerParent;

    private Vector3 playerSize;
    private float playerMass;

    private Rigidbody playerRb;

    private void Start()
    {
        PlayerContainer playerContainer = GetComponentInParent<PlayerContainer>();
        playerParent = playerContainer.GetPlayer();

        playerSize = playerParent.transform.localScale;
        playerRb = playerParent.GetComponent<Rigidbody>();
        playerMass = playerRb.mass;

        Vector3 adjustedSize = playerParent.transform.localScale * sizeIncrease;
        float adjustedMass = playerRb.mass * massIncrease;

        playerParent.transform.localScale = adjustedSize;
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

        playerParent.layer = 06;
        playerParent.transform.localScale = playerSize;
        playerRb.mass = playerMass;
    }
}
