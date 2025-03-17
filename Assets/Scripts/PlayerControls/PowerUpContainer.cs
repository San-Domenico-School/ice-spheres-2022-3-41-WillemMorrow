using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerUpContainer : MonoBehaviour
{
    private Vector2 aimVector;

    private void Update()
    {
        // set the rotation to face the direction of the vector2.
    }

    public void OnAim(InputAction.CallbackContext context) => setMoveVector(context.ReadValue<Vector2>());

    private void setMoveVector(Vector2 vector)
    {
        aimVector = vector;
    }

    
}
