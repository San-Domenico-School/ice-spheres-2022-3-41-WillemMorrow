using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*************************************************
 * rotates the powerup when on the scene, and holds each powerup's cooldown.
 * 
 * component of all powerups.
 * 
 * Pacifica Morrow
 * 02.14.2025
 * **********************************************/

public class PowerUpControler : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    public float GetCooldown()
    {
        return cooldown;
    }
}
