using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeHitboxScript : MonoBehaviour
{
    [Header("Hitbox Settings")]
    [Space(5)]    
    [Range(0, 10)]
    [SerializeField] private int damage;
    [Range(0, 10)]
    [SerializeField] private int knockbackForce;
    [Range(0, 1)]
    [SerializeField] private float stunDuration;

    [Tooltip("Hitbox duration in seconds")]

    [SerializeField] private float hitboxDuration_Setter;
    public float hitboxDuration { get; private set; }
    public enum DamageType{Arcane, Blunt, Piercing, Slashing, None}
    public DamageType damageType;
    [SerializeField] private LayerMask enemyLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        hitboxDuration = hitboxDuration_Setter;
        yield return new WaitForSeconds(hitboxDuration);
        Destroy(this.gameObject);
    }
    private void OnEnable()
    {
        hitboxDuration = hitboxDuration_Setter;

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("AreaLock"))
        {
            Debug.Log("Area Lock Triggered");
            // Get the EnemyHealthScript component from the collided object
            collision.GetComponent<Animator>().SetTrigger("Destroyed");
        }
    }
}
