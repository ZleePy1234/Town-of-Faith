using UnityEngine;

public class PlayerConsumableScript : MonoBehaviour
{
    private PlayerMainScript playerScript;
    private PlayerEquipmentScript equipmentScript;
    public enum CurrentConsumable
    {
        Medkit,
        ArmorPlate,
        AmmoBox,
        Contract,
        none
    }
    public CurrentConsumable currentConsumable = CurrentConsumable.none;
    public enum CurrentGrenade
    {
        Explosive,
        healing,
        none
    }
    public CurrentGrenade currentGrenade = CurrentGrenade.none;

    public KeyCode useConsumableKey = KeyCode.V;
    public KeyCode useGrenadeKey = KeyCode.C;

    void Awake()
    {
        playerScript = GetComponent<PlayerMainScript>();
        equipmentScript = GetComponent<PlayerEquipmentScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(useConsumableKey))
        {
            UseConsumable();
        }

        if (Input.GetKeyDown(useGrenadeKey))
        {
            UseGrenade();
        }
    }
    void UseConsumable()
    {
        switch (currentConsumable)
        {
            case CurrentConsumable.Medkit:
                playerScript.healthCurrent += 50;
                currentConsumable = CurrentConsumable.none;
                break;
            case CurrentConsumable.ArmorPlate:
                playerScript.armorCurrent += 50;
                currentConsumable = CurrentConsumable.none;
                break;
            case CurrentConsumable.AmmoBox:
                equipmentScript.RefillAmmo();
                currentConsumable = CurrentConsumable.none;
                break;
            case CurrentConsumable.Contract:
                // logica aqui
                currentConsumable = CurrentConsumable.none;
                break;
            case CurrentConsumable.none:
                Debug.Log("No consumable held!");
                break;
        }
    }
    void UseGrenade()
    {
        switch (currentGrenade)
        {
            case CurrentGrenade.Explosive:
                // logica aqui
                currentGrenade = CurrentGrenade.none;
                break;
            case CurrentGrenade.healing:
                // logica aqui
                currentGrenade = CurrentGrenade.none;
                break;
            case CurrentGrenade.none:
                Debug.Log("No grenade held!");
                break;
        }
    }
}
