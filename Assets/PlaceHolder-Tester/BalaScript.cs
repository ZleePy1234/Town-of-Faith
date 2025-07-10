using UnityEngine;
using System.Collections;

public class BalaScript : MonoBehaviour
{
    // This script is a placeholder for testing purposes.
    private Rigidbody rb;
    [SerializeField] private float bulletSpeed;
    public int bulletDamage;
    [SerializeField] private float bulletLifetime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //this.transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
        rb.MovePosition(rb.position + transform.forward * (bulletSpeed * Time.fixedDeltaTime));
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("DPSdummy"))
        {
            DPSdummy dPSdummy = collision.GetComponent<DPSdummy>();
            if (dPSdummy != null)
            {
                dPSdummy.TakeDamage(bulletDamage);
            }
            Destroy(gameObject);
        }
    }
}
