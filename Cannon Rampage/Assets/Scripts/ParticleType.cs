using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleType : MonoBehaviour
{
    public ParticleEffect particleEffect;
}

public enum ParticleEffect
{
    VehicleWreck,
    Rocket,
    ZombieBlood
}