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

    void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Round(health);
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
                if (anim.GetNextAnimatorStateInfo(0).IsName("attack"))
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
        attackBox.SetActive(true);
    }
    void DisableAttackDamage()
    {
        attackBox.SetActive(false);
    }

    IEnumerator SpeedPostAttack()
    {
        speed /= 2;
        yield return new WaitForSeconds(0.5f);
        speed *= 2;
    }
}
