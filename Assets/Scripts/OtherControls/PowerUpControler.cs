using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpControler : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private float cooldown;

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
