using UnityEngine;

public class Trampoline : MonoBehaviour
{
    
    public float bounceForce = 10f;

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
            }
        }
    }
}
