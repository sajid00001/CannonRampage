using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Vehicle : MonoBehaviour
{
    private BoxCollider boxCollider;
    private VehicleBody truckBody;
    private ParticleType particleType;

    private void Awake()
    {
        particleType = GetComponent<ParticleType>();
        boxCollider = GetComponent<BoxCollider>();
        truckBody = transform.GetChild(0).gameObject.GetComponent<VehicleBody>();
    }

    public void Hitted()
    {
        if (gameObject.CompareTag("wrecked"))
            return;

        ParticleManager.particleManager.PlayEffect(particleType, transform.position);
        boxCollider.isTrigger = true;
        gameObject.tag = "wrecked";
        truckBody.EnablePartsPhyics();
        Invoke(nameof(Disable), Random.Range(2f, 3f));
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
