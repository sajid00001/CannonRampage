using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZombieCharacter : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private ParticleType particleEffectType;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        particleEffectType = GetComponent<ParticleType>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (navMeshAgent != null)
        {
            if (navMeshAgent.enabled)
            {
                if (navMeshAgent.path == null || navMeshAgent.hasPath == false)
                {
                    // Generate and go to random position to remain in targetable area

                    Vector3 rndDestination = new Vector3(Random.Range(-13f, 13f), transform.position.y, Random.Range(-50, -30));
                    navMeshAgent.SetDestination(rndDestination);
                }
            }

            if (navMeshAgent.velocity.sqrMagnitude > 0)
                animator.SetBool("walk", true);
            else
                animator.SetBool("walk", false);
        }
    }

    public void Hitted()
    {
        gameObject.tag = "dead";
        ParticleManager.particleManager.PlayEffect(particleEffectType, transform.position);
        gameObject.SetActive(false);
    }
}
