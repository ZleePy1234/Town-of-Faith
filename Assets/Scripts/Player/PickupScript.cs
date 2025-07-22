using UnityEngine;

public class PickupScript : MonoBehaviour
{
    private PlayerConsumableScript playerConsumableScript;
    bool playerInRange = false;
    public enum pickupConsumable
    {
        Medkit,
        ArmorPlate,
        AmmoBox,
        Contract,
        none
    }
    public pickupConsumable consumable = pickupConsumable.none;
    public enum pickupGrenade
    {
        Explosive,
        healing,
        none
    }
    public pickupGrenade grenade = pickupGrenade.none;

    void Awake()
    {
        playerConsumableScript = GameObject.Find("Player").GetComponent<PlayerConsumableScript>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player is in range to pick up consumable.");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player is out of range to pick up consumable.");
        }
    }
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            playerConsumableScript.currentConsumable = (PlayerConsumableScript.CurrentConsumable)consumable;
            playerConsumableScript.currentGrenade = (PlayerConsumableScript.CurrentGrenade)grenade;
            Debug.Log("Picked up consumable: " + consumable + " and grenade: " + grenade);
            Destroy(gameObject);
        }
    }
}
