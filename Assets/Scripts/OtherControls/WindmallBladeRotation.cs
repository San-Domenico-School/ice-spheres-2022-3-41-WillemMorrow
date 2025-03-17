using UnityEngine;

public class WindmillBlade : MonoBehaviour
{
    
    public float rotationSpeed = 50f;
    public float pushForce = 10f;
    public Vector3 pushDirection = new Vector3(1f, 0.5f, 0f);

    void Update()
    {
        // Rotate around the local Z-axis (or whichever axis your blades spin around)
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is tagged "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Apply an impulse force in the specified push direction
                rb.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
            }
        }
    }
}



