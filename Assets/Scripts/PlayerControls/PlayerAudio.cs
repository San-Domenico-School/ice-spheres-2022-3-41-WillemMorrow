using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************
 * class handling the audio from the player 
 * (eg. spawning, getting a powerup or score, etc.)
 * 
 * component of the Player Container prefab.
 * 
 * Pacifica Morrow
 * 28.05.2025
 * *************************/

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip spawnSFX;
    [SerializeField] private AudioClip powerUpSFX;
    [SerializeField] private AudioClip scoreSFX;

    public void playAudio(string name)
    {
        switch (name)
        {
            case ("spawnSFX"):
                audioSource.PlayOneShot(spawnSFX);
                break;
            case ("powerUpSFX"):
                audioSource.PlayOneShot(powerUpSFX);
                break;
            case ("scoreSFX"):
                audioSource.PlayOneShot(scoreSFX);
                break;

            default:
                Debug.LogWarning("The audio requested does not exist! Check spelling and capitalisation.");
                break;
        }

    }
}
