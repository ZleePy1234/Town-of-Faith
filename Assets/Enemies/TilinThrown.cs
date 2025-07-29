using UnityEngine;

public class TilinThrown : MonoBehaviour
{
    private Rigidbody rb;
    public float forwardSpeed = 10f;
    public float upwardSpeed = 3f;
    private PlayerMainScript playerScript;
    public int explosionDamage = 10;
    public float explosionRadius = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerMainScript>();
        
        rb.AddForce(transform.forward * forwardSpeed + transform.up * upwardSpeed, ForceMode.Impulse);
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

        }
        else
        {
            Explode();
            Debug.Log("Tilin collided with " + other.gameObject.name + ", triggering explosion.");
            Destroy(gameObject);
        }
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
