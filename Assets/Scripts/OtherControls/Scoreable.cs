using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***********************************
 * holds the score earned from collecting the respective scorable.
 * 
 * Component of the scorable collectables
 * 
 * Pacifica Morrow
 * 03.15.2025
 * ********************************/


public class Scoreable : MonoBehaviour
{
    [SerializeField] private int scoreAdded;
    [SerializeField] private float rotationSpeed;

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
    public int GetScore()
    {
        return scoreAdded;
    }
}
