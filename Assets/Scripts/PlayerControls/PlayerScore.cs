using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private string playerName;

    private void Start()
    {
        playerName = transform.parent.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scoreable"))
        {
            Scoreable scoreable = other.GetComponent<Scoreable>();
            int scoreAdded = scoreable.GetScore();
            
            // UIManager.Singleton.AddPoints(playerName, scoreAdded);
        }
    }
}
