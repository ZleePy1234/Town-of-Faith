using UnityEditor.Animations;
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
        //todo: add GetComponent of specific enemy behaviour script here
    }
    
    void Update()
    {
        
    }

    void StateManager()
    {
        switch (state)
        {
            case State.Idle:
                state = State.Idle;
                break;
            case State.Combat:
                state = State.Combat;
                break;
            case State.Dead:
                state = State.Dead;
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
}
