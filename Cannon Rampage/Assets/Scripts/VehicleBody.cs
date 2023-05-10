using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBody : MonoBehaviour
{
    private Rigidbody[] parts;
    private BoxCollider[] partsColliders;

    private void Awake()
    {
        parts = new Rigidbody[transform.childCount + 1];
        partsColliders = new BoxCollider[transform.childCount + 1];

        for (int i = 0; i < transform.childCount; i++)
        {
            partsColliders[i] = transform.GetChild(i).gameObject.GetComponent<BoxCollider>();
            parts[i] = transform.GetChild(i).gameObject.GetComponent<Rigidbody>();
        }

        partsColliders[partsColliders.Length -1] = gameObject.GetComponent<BoxCollider>();
        parts[parts.Length - 1] = gameObject.GetComponent<Rigidbody>();
    }

    public void EnablePartsPhyics()
    {
        for (int i = 0; i < partsColliders.Length; i++)
        {
            partsColliders[i].isTrigger = false;
        }

        for (int i = 0; i < parts.Length; i++)
        {
            parts[i].useGravity = true;
            parts[i].isKinematic = false;
            parts[i].AddExplosionForce(100f, transform.position, 3f);
        }
    }
}
