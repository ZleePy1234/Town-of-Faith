using UnityEngine;

public class EncounterTriggerThroaway : MonoBehaviour
{
    public GameObject encounter;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the encounter trigger.");
            encounter.SetActive(true);
            Destroy(gameObject);
        }
    }
}
