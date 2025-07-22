using UnityEngine;
using System.Collections;

public class PajonBala : MonoBehaviour
{
    private PlayerMainScript playerScript;
    private Rigidbody rb;
    [SerializeField] private float bulletSpeed;
    public int bulletDamage;
    [SerializeField] private float bulletLifetime;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(bulletLifetime);
        Destroy(gameObject);
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxLinearVelocity = 0;
        rb.maxAngularVelocity = 0;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        playerScript = GameObject.Find("Player").GetComponent<PlayerMainScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.forward * (bulletSpeed * Time.fixedDeltaTime));
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerScript.DamagePlayer(bulletDamage, "ranged", false);
            Destroy(gameObject);
        }
    }
}