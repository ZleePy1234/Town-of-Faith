using UnityEngine;

public class TilinBehaviour : MonoBehaviour
{
    private PlayerMainScript playerScript;
    public int explosionDamage = 10;
    public float explosionRadius = 5f;


    void Awake()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerMainScript>();
    }

    void Explode()
    {
        if (Vector3.Distance(transform.position, playerScript.transform.position) <= explosionRadius)
        {
            Debug.Log("Player is within explosion radius, applying damage.");
            playerScript.DamagePlayer(explosionDamage, "ranged", false);
        }
        Debug.Log("Tilin exploded, damaging player if within range.");
        Destroy(gameObject);
    }
}
