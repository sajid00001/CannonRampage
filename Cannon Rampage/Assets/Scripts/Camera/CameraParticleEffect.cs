using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParticleEffect : MonoBehaviour
{
    private ParticleSystem[] winParticles;

    private void Awake()
    {
        winParticles = new ParticleSystem[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            winParticles[i] = transform.GetChild(i).gameObject.GetComponent<ParticleSystem>();
        }

        GameManager.GameIsWon += ShowParticleEffect;
    }

    private void OnDisable()
    {
        GameManager.GameIsWon -= ShowParticleEffect;
    }
    void ShowParticleEffect()
    {
        for (int i = 0; i < winParticles.Length; i++)
        {
            winParticles[i].Play();
        }
    }
}
