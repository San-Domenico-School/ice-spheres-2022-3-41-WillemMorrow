using UnityEngine;

/***************************************
 * Pushes Player on collision 
 * 
 * Component of: WindmillBlades
 * 
 * Gleb
 * 04.19.2025
 * ************************************/
public class WindmillBlade : MonoBehaviour
{
    
    public float rotationSpeed = 50f;
    public float pushForce = 10f;
    public Vector3 pushDirection = new Vector3(1f, 0.5f, 0f);

    void Update()
    {
        // Rotate around the local Z-axis to push player in right deriction
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
               
                rb.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
            }
        }
    }
}



