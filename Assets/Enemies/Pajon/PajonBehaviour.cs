using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PajonBehaviour : MonoBehaviour
{
    private PlayerMainScript playerScript;
    public GameObject projectilePrefab;

    public Transform firePoint;

    bool canRun = true;

    NavMeshAgent agent;

    void Awake()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerMainScript>();
        agent = GetComponent<NavMeshAgent>();
    }


    void CheckAndFire()
    {
        Debug.Log("Checking if player is in line of sight...");
        Vector3 directionToPlayer = playerScript.transform.position - firePoint.position;
        if (Physics.Raycast(firePoint.position, directionToPlayer.normalized, out RaycastHit hit))
        {
            if (hit.transform == playerScript.transform)
            {
                if (canRun)
                {
                    StartCoroutine(CantMove());
                }
                Debug.Log("Player is in line of sight, firing projectile.");
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(directionToPlayer));
            }
        }
    }
    private IEnumerator CantMove()
    {
        canRun = false;
        agent.isStopped = true;
        yield return new WaitForSeconds(4f);
        canRun = true;
        agent.isStopped = false;
    }
}
