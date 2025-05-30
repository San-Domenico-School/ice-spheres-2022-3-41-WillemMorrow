using UnityEngine;
/***************************************
 * Makes player jump on colision
 * 
 * Component of: Trampoline
 * 
 * Gleb
 * 04.19.2025
 * ************************************/

public class Trampoline : MonoBehaviour
{
    [SerializeField] private AudioClip bounceSFX;
    private AudioSource audioSource;
    public float bounceForce;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                
                Vector3 currentVelocity = rb.velocity;
                currentVelocity.y = 0f;
                rb.velocity = currentVelocity;
                rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);

                audioSource.PlayOneShot(bounceSFX);
            }
            else
            {
                Debug.LogWarning("trampoline's playerRb is null!");
            }
        }
    }
}
