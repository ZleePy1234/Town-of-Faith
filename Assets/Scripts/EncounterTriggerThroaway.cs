using UnityEngine;

public class EncounterTriggerThroaway : MonoBehaviour
{
    public GameObject encounter;
    public GameObject enemyCounter;
    private void Awake()
    {
        encounter.SetActive(false);
        enemyCounter = GameObject.FindWithTag("EnemyCounter");
    }
    void Start()
    {
        enemyCounter.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the encounter trigger.");
            enemyCounter.SetActive(true);
            encounter.SetActive(true);
            Destroy(gameObject);
        }
    }
}
