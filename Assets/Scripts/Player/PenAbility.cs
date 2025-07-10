using UnityEngine;

public class PenAbility : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private PlayerEquipmentScript playerEquipmentScript;
    private GameObject player;
    public GameObject penHurtBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerEquipmentScript = player.GetComponent<PlayerEquipmentScript>();
        rb.linearVelocity = Vector3.zero;
    }
    void Start()
    {
        rb.linearVelocity = transform.forward * speed;
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Vector3 teleportPosition = collision.contacts[0].point + Vector3.up * 1f;
            player.transform.position = teleportPosition;
            Instantiate(penHurtBox, player.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            // si choca con la pared, no gaste la abilidad y no haga tp
        }
    }
}
