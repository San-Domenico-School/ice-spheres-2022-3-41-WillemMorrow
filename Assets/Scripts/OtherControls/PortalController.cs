using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************
 * Holds a Portal's destination
 * 
 * Component of Portal.
 * 
 * Pacifica Morrow
 * 2.10.2025
 * *********************************/

public class PortalController : MonoBehaviour
{
    [SerializeField] private string destination;

    public string GetDestination()
    {
        return destination;
    }
}
