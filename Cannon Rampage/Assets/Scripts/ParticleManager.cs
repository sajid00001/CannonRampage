using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager particleManager;
    private ParticleSystem[] particleSystems;
    private ParticleType[] particleTypes;

    private void Awake()
    {
        if (particleManager != this)
        {
            Destroy(particleManager);
        }

        if (particleManager == null)
        {
            particleManager = this;
        }

        particleSystems = new ParticleSystem[transform.childCount];
        particleTypes = new ParticleType[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            particleSystems[i] = transform.GetChild(i).gameObject.GetComponent<ParticleSystem>();
            particleTypes[i] = transform.GetChild(i).gameObject.GetComponent<ParticleType>();
        }
    }

    public void PlayEffect(ParticleType particleType, Vector3 position)
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            if (particleTypes[i].particleEffect == particleType.particleEffect && particleSystems[i].isPlaying == false)
            {
                particleSystems[i].transform.position = position;
                particleSystems[i].Play();
                break;
            }
        }
    }
}