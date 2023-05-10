using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Rigidbody physicsBody;
    public ParticleType onBlastParticles;
    public ParticleSystem fireTrail;
    public AudioClip impactAudio;

    [Range(1.0f, 15.0f)] public float TargetRadius;
    [Range(20.0f, 75.0f)] public float LaunchAngle;
    [Range(0.0f, 10.0f)] public float TargetHeightOffsetFromGround;
    public bool RandomizeHeightOffset;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // update the rotation of the projectile during trajectory motion
        transform.rotation = Quaternion.LookRotation(physicsBody.velocity) * initialRotation;
    }

    // launches the object towards the TargetObject with a given LaunchAngle
    public void Launch(Vector3 targetPosition)
    {
        // think of it as top-down view of vectors: 
        //   we don't care about the y-component(height) of the initial and target position.
        Vector3 projectileXZPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
        Vector3 targetXZPos = new Vector3(targetPosition.x, 0.0f, targetPosition.z);

        // rotate the object to face the target
        transform.LookAt(targetXZPos);

        // shorthands for the formula
        float R = Vector3.Distance(projectileXZPos, targetXZPos);
        float G = Physics.gravity.y;
        float tanAlpha = Mathf.Tan(LaunchAngle * Mathf.Deg2Rad);
        float H = (targetPosition.y) - transform.position.y;

        // calculate the local space components of the velocity 
        // required to land the projectile on the target object 
        float Vz = Mathf.Sqrt(G * R * R / (2.0f * (H - R * tanAlpha)));
        float Vy = tanAlpha * Vz;

        // create the velocity vector in local space and get it in global space
        Vector3 localVelocity = new Vector3(0f, Vy, Vz);
        Vector3 globalVelocity = transform.TransformDirection(localVelocity);

        // launch the object by setting its initial velocity and flipping its state
        physicsBody.velocity = globalVelocity;

        fireTrail.Play();
    }

    private void OnDisable()
    {
        physicsBody.velocity = Vector3.zero;
        physicsBody.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3f);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.CompareTag("Vehicle"))
            {
                Vehicle vehicle = colliders[i].gameObject.GetComponent<Vehicle>();
                vehicle.Hitted();
            }
            else if (colliders[i].gameObject.CompareTag("Zombie"))
            {
                ZombieCharacter zombieCharacter = colliders[i].gameObject.GetComponent<ZombieCharacter>();
                zombieCharacter.Hitted();
            }
        }

        ParticleManager.particleManager.PlayEffect(onBlastParticles , transform.position);
        gameObject.SetActive(false);
    }
}
