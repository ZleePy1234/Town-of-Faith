using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class WeaponPickup : MonoBehaviour
{
    public string weapon;

    [Range(1, 4)]
    [Tooltip("1: Pistol | 2: SMG | 3: Shotgun | 4: Revolver")]
    public int weaponID;

    public bool isMelee;

    public TextMeshProUGUI text;
    public GameObject textObject;
    
    public PlayerEquipmentScript playerEquipment;
    public PlayerMainScript playerMain;
    public bool inPickupRange = false;
    private KeyCode interactKey;
    void Awake()
    {
        //playerEquipment = GameObject.FindWithTag("Player").GetComponent<PlayerEquipmentScript>();
        //playerMain = GameObject.FindWithTag("Player").GetComponent<PlayerMainScript>();
        interactKey = KeyCode.F; // Default interact key, can be changed in PlayerMainScript
    }

    void Start()
    {
        if (textObject.activeSelf)
        {
            textObject.SetActive(false);
        }
        else
        {

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inPickupRange = true;
            textObject.SetActive(true);
            text.text = "Press F to pick up " + weapon;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inPickupRange = false;
            textObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interactKey) && inPickupRange && isMelee == false)
        {
            playerEquipment.gunObtained[weaponID] = true;
            playerEquipment.selectedGunRight = PlayerEquipmentScript.SelectedGunRight.Pistol; // Default to Pistol
            playerEquipment.equippedGunRight = weaponID;
            playerEquipment.SwitchGunRight();
            playerEquipment.rightHandMode = PlayerEquipmentScript.RightHandMode.gun;
            textObject.SetActive(false);
            inPickupRange = false;
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(interactKey) && inPickupRange && isMelee == true)
        {
            playerEquipment.meleeObtained[weaponID] = true;
            playerEquipment.equippedMeleeLeft = weaponID;
            playerEquipment.SwitchMeleeLeft();
            playerEquipment.leftHandMode = PlayerEquipmentScript.LeftHandMode.melee;
            textObject.SetActive(false);
            inPickupRange = false;
            Destroy(gameObject);
        }
    }
    
}
