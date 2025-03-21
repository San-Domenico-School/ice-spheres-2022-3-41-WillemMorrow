using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
