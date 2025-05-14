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
    private string playerName; // the player's name.
    private int indexNum; // the index number the player is.

    private void Start()
    {
        playerName = transform.parent.name;

        // switch statement assigning indexNum
        switch (playerName)
        {
            case ("Senior"):
                indexNum = 0;
                break;
            case ("Junior"):
                indexNum = 1;
                break;
            case ("Soph"):
                indexNum = 2;
                break;
            case ("Fresh"):
                indexNum = 3;
                break;
            default:
                indexNum = 4;
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scoreable"))
        {
            //get the score to be added from the other that was collided with.
            Scoreable scoreable = other.GetComponent<Scoreable>();
            int scoreAdded = scoreable.GetScore();

            //call the method on the scorekeeper to update the score.
            Scorekeeper.Singleton.UpdateScore(indexNum, scoreAdded);

            Destroy(other.gameObject);
        }
    }


    public void OnPortalEnter(int scoreAdded)
    {
        Scorekeeper.Singleton.UpdateScore(indexNum, scoreAdded);
    }
}
