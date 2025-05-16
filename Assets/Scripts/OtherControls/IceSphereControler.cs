using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***********************************************
 * Handles setting the random size of the IceSpheres and melts them.
 * 
 * Component of IceSphere
 * 
 * Pacifica Morrow
 * 02.14.2025
 * ********************************************/

public class IceSphereControler : MonoBehaviour
{
    [SerializeField] private float lowerRangeMass;
    [SerializeField] private float upperRangeMass;
    private float startDelay;
    private float reductionEachRepeat;
    private float minimumVolume;
    private Rigidbody iceRb;
    private ParticleSystem iceVFX;

    // Start is called before the first frame update
    void Start()
    {
        startDelay = 3.0f;
        minimumVolume = 1f;
        if (GameManager.Singleton.debugSpawnWaves)
        {
            reductionEachRepeat = .3f;
        }
        else
        {
            reductionEachRepeat = .975f;
        }

        iceRb = GetComponent<Rigidbody>();
        iceVFX = GetComponent<ParticleSystem>();

        RandomizeSizeAndMass();

        InvokeRepeating("Melt", startDelay, 0.4f);

        RandomEnemyType();
    }

    private void RandomizeSizeAndMass()
    {
        float randSizeAndMass = Random.Range(lowerRangeMass, upperRangeMass);

        transform.localScale *= randSizeAndMass;
        iceRb.mass *= randSizeAndMass;
    }

    private void RandomEnemyType()
    {
    
        
    }

    private void Melt()
    {
        transform.localScale *= reductionEachRepeat;
        iceRb.mass *= reductionEachRepeat;

        float volume = 4f / 3f * Mathf.PI * Mathf.Pow(transform.localScale.x, 3);

        if (volume <= minimumVolume)
        {
            Melted();
        }
    }
    private void Melted()
    {
        iceVFX.Stop();
        Destroy(gameObject);
    }

}
