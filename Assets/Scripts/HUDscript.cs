using UnityEngine;
using TMPro;

public class HUDscript : MonoBehaviour
{
    private PlayerMainScript playerMainScript;
    private PlayerEquipmentScript playerEquipmentScript;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI armorText;

    [Tooltip("0 Pistol, 1 SMG, 2 Shotgun, 3 Revolver")]
    public TextMeshProUGUI[] ammoText = new TextMeshProUGUI[4];

    void Awake()
    {
        playerMainScript = GameObject.Find("Player").GetComponent<PlayerMainScript>();
        playerEquipmentScript = GameObject.Find("Player").GetComponent<PlayerEquipmentScript>();
    }

    void Update()
    {
        HUDupdate();
    }

    void HUDupdate()
    {
        healthText.text = playerMainScript.healthCurrent.ToString();
        armorText.text = playerMainScript.armorCurrent.ToString();

        ammoText[0].text = playerEquipmentScript.weaponReserveAmmo[0].ToString();
        ammoText[1].text = playerEquipmentScript.weaponReserveAmmo[1].ToString();
        ammoText[2].text = playerEquipmentScript.weaponReserveAmmo[2].ToString();
        ammoText[3].text = playerEquipmentScript.weaponReserveAmmo[3].ToString();

    }
}
