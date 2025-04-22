using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*****************************************************
 * class responsible for notifying the scorekeeper that the player has gained score.
 * 
 * component of the moveable player.
 * 
 * 03.25.2025
 * **************************************************/

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

            Destroy(other.gameObject);
        }
    }

    public void OnPortalEnter(int scoreAdded)
    {
        //UIManager.Singleton.AddPoints(playerName, scoreAdded);
    }
}
