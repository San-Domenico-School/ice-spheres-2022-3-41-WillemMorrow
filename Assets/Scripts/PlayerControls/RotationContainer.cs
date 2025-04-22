using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotationContainer : MonoBehaviour
{
    [SerializeField] private GameObject playerParent;
    private float aimDirection;
    private Transform focalPoint;
    
    // sets the object's position to sit upon the player.
    private void Update()
    {
        transform.position = playerParent.transform.position;
    }

    // invokes the method everytime the right stick moves.
    public void OnAim(InputAction.CallbackContext context)
    {
        Vector2 aimVector = context.ReadValue<Vector2>();

        // if conditional prevents the game from resetting the rotation when the player drops the right stick.
        if (aimVector.x != 0 || aimVector.y != 0)
        {
            if (focalPoint != null)
            {
                Vector3 localAimDirection = focalPoint.TransformDirection(aimVector.normalized);

                aimDirection = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg * -1;
                //Debug.Log($"aimdirection = {aimDirection}");

                transform.rotation = focalPoint.rotation * (Quaternion.Euler(new Vector3(0, aimDirection, 0)));
            }

            else
            {
                focalPoint = GameObject.Find("FocalPoint").transform;
                //Debug.Log($"focalpoint = {focalPoint.name}");
            }
        }
    }

    /*
    private void setMoveVector(Vector2 vector)
    {
        aimDirection = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg * -1;
    }*/
}
