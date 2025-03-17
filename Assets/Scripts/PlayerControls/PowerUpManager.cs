using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PowerUpManager : MonoBehaviour
{



    public abstract void onPowerupActivate();

    public void onFire(InputAction.CallbackContext context)
    {
        
    }
}
