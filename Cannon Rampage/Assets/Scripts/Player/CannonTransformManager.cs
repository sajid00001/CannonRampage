using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTransformManager : MonoBehaviour
{
    public Transform followTarget;

    private void FixedUpdate()
    {
        Vector3 direction = followTarget.transform.position - transform.position;
        //direction.x = Mathf.Clamp(direction.x, -35f, -6f);

        transform.forward = direction;
    }
}
