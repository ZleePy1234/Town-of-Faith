using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGenericScript : MonoBehaviour
{
    //todo: go over protection levels afterwards
    [Header("Enemy Stats")]
    public float health;

    public float speed;
    public NavMeshAgent agent;
    public GameObject player;
    private Vector3 spawnPos;
    private Animator anim;
    public float attackRange;
    public Vector3 rbdata;
    private Rigidbody rb;

    //todo: add reference for specific enemy behaviour script here
    public enum State
    {
        Idle,
        Combat,
        Dead
    }

    public State state;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rbdata = rb.linearVelocity;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        spawnPos = transform.position;
        anim = GetComponent<Animator>();
        //todo: add GetComponent of specific enemy behaviour script here
    }

    void Update()
    {
        Combat();
        StateManager();
    }

    void StateManager()
    {
        switch (state)
        {
            case State.Idle:
                state = State.Idle;
                agent.SetDestination(spawnPos);
                break;
            case State.Combat:
                state = State.Combat;
                anim.SetBool("moving", true);
                agent.SetDestination(player.transform.position);
                break;
            case State.Dead:
                state = State.Dead;
                agent.SetDestination(transform.position);
                agent.isStopped = true;
                break;
            default:
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy took damage: " + damage);
        if (state == State.Dead)
        {
            return;
        }
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        state = State.Dead;
        anim.SetTrigger("die");
        agent.isStopped = true;
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        //borrar destroy despues de agregar animaciones y logica extra
        Destroy(gameObject);
    }
    void DetectPlayer()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            if (hit.transform.CompareTag("Player"))
            {
                state = State.Combat;
            }
        }
        else
        {

        }
    }
    void Combat()
    {
        if (state != State.Combat)
        {
            return;
        }
        else
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= attackRange)
            {
                if (anim.GetNextAnimatorStateInfo(0).IsName("Attack"))
                {
                    return;
                }
                else
                {
                    anim.SetTrigger("attack");
                }
            }
        }
    }
    public GameObject attackBox;
    void EnableAttackDamage()
    {
        if (attackBox == null)
        {
            Debug.LogError("Attack box is not assigned or one is not needed");
            return;
        }
        attackBox.SetActive(true);
    }
    void DisableAttackDamage()
    {
        if (attackBox == null)
        {
            Debug.LogError("Attack box is not assigned or one is not needed");
            return;
        }
        attackBox.SetActive(false);
    }

    IEnumerator SpeedPostAttack()
    {
        speed /= 2;
        yield return new WaitForSeconds(0.5f);
        speed *= 2;
    }
}
