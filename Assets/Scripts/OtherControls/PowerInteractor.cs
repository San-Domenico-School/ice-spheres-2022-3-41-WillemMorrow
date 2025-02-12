using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************
 * class responsible for determining who is pushed; player or IceSphere
 * 
 * component of IceSphere
 * 
 * Pacifica Morrow
 * 01.14.2025
 ***********************************************/

public class PowerInteractor : MonoBehaviour
{
    private float pushForce;
    private Rigidbody iceRb;
    private IceSphereControler iceControler;

    // Start is called before the first frame update
    void Start()
    {
        iceRb = GetComponent<Rigidbody>();
        iceControler = GetComponent<IceSphereControler>();
        pushForce = 10;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            Rigidbody playerRB = player.GetComponent<Rigidbody>();
            PlayerControler playerControlerScript = player.GetComponent<PlayerControler>();
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

            if (playerControlerScript.hasPowerUp)
            {
                iceRb.AddForce(-directionToPlayer * iceRb.mass * GameManager.Singleton.playerRepelForce, ForceMode.Impulse);
            }

            else
            {
                playerRB.AddForce(directionToPlayer * playerRB.mass * GameManager.Singleton.playerRepelForce, ForceMode.Impulse);
            }
        }
    }
}
