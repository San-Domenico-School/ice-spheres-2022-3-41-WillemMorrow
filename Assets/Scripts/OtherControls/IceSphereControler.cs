using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSphereControler : MonoBehaviour
{
    private float startDelay;
    private float reductionEachRepeat;
    private float minimumVolume;
    private Rigidbody iceRb;
    private ParticleSystem iceVFX;

    // Start is called before the first frame update
    void Start()
    {
        startDelay = 3.0f;
        minimumVolume = 0.2f;
        if (GameManager.Singleton.debugSpawnWaves)
        {
            reductionEachRepeat = .5f;
        }
        else
        {
            reductionEachRepeat = .975f;
        }

        iceRb = GetComponent<Rigidbody>();
        iceVFX = GetComponent<ParticleSystem>();

        RandomizeSizeAndMass();

        InvokeRepeating("Melt", startDelay, 0.4f);
    }

    private void RandomizeSizeAndMass()
    {
        float randSizeAndMass = Random.Range(0.5f, 1.0f);

        transform.localScale *= randSizeAndMass;
        iceRb.mass *= randSizeAndMass;
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
