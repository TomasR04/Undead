using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public float detectionRadius = 10f;
    public LayerMask targetLayer;
    public float fieldOfViewAngle = 90f;

    private Transform target;
    private NavMeshAgent agent;
    private Animator animator;
    public bool isDead = false;

    private float distance;
    private byte a = 0;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Náraz" + collision.gameObject.name);
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            target = GameObject.Find("Player").transform;
            GetComponentInChildren<ParticleSystem>().Play();
            
        }
    }
    void Update()
    {
        if (isDead!=true)
        {
            if (target != null)
            {
                ChaseTarget();
            }
            else
            {
                FindTarget();
                animator.SetBool("IsWalking", false);
            }
        }
        else
        {
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            /*if (a <70)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
                a++;
            }*/
            
           
        }
        
    }

    void FindTarget()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, detectionRadius, targetLayer);

        foreach (var potentialTarget in targetsInViewRadius)
        {
            Transform targetTransform = potentialTarget.transform;
            Vector3 directionToTarget = targetTransform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, directionToTarget);
            if (target == null)
            {
                if (angle < fieldOfViewAngle * 0.5f)
                {
                    
                    Vector3 raycastOrigin = transform.position + Vector3.up * 1.5f;

                    RaycastHit hit;
                    if (Physics.Raycast(raycastOrigin, directionToTarget.normalized, out hit, detectionRadius))
                    {
                        if (hit.collider.CompareTag("Player"))
                        {
                            target = targetTransform;
                            return;
                        }
                    }
                }
            }
            else
            {
                Collider[] targetsInViewRadiu = Physics.OverlapSphere(transform.position, detectionRadius, targetLayer);

                if (targetsInViewRadiu.Length > 0)
                {
                    target = targetsInViewRadiu[0].transform;
                    return;
                }
                else
                {
                    target = null;
                }
            }
            
        }

        target = null;
    }

    void ChaseTarget()
    {
        if (isDead)
        {
            agent.isStopped = true;
        }
        else 
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            if (Vector3.Distance(transform.position, target.position) > 1.5f)
            {
                agent.SetDestination(target.position);
                animator.SetBool("IsWalking", true);

            }
            else
            {

                animator.SetBool("IsWalking", false);
                Bite(target);
            }
        }
        
        
    }

    public void Bite(Transform target)
    {
        animator.SetTrigger("Bite");
    }

    public void StartOfBite()
    {
        Debug.Log("Start");
        distance = Vector3.Distance(transform.position, target.position);
    }

    public void AnimBite()
    {
        Debug.Log("Bite");
        if (Vector3.Distance(transform.position, target.position)<=1.5)
        {
            target.gameObject.GetComponent<PlayerStats>().Die();
        }
    }
}
