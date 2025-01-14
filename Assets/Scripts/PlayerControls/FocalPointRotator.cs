using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************
 * Rotates the camera around a focal point, the middle of the island. 
 * 
 * component of the FocalPoint.
 * 
 * 01.10.2025
 * Pacifica Morrow
 * ******************************************/

public class FocalPointRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private PlayerInputActions playerInputActions;
    private float moveDirection;

    // Start is called before the first frame update
    void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, moveDirection * rotationSpeed * Time.deltaTime * (-1.0f));
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Player.Movement.performed += ctx => { CameraRotate(ctx.ReadValue<Vector2>()); };
        playerInputActions.Player.Movement.canceled += ctx => { moveDirection = 0.0f; };
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
    }

    private void CameraRotate(Vector2 value)
    {
        moveDirection = value.x;
    }
}
