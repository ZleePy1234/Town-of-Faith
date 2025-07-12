using UnityEditor.EditorTools;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int attackDamage;
    [Tooltip("melee, ranged or environment")]
    public string attackType;
    [Tooltip("Check if damage is dark damage from boss / strong rare attacks, these reset player armor to 0 and deal double damage")]
    public bool isDarkDamage;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMainScript player = other.GetComponentInParent<PlayerMainScript>();
            player.DamagePlayer(attackDamage, attackType, isDarkDamage);
        }
    }
}
