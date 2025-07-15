using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEquipmentScript : MonoBehaviour
{
    [Header("Hand System")]
    [Space(5)]
    [SerializeField] private KeyCode leftHandKey;
    [SerializeField] private KeyCode rightHandKey;
    [SerializeField] private KeyCode handSwitchModifierKey;
    public enum LeftHandMode { melee, gun, equipment };
    public LeftHandMode leftHandMode;
    public enum RightHandMode { melee, gun, equipment };
    public RightHandMode rightHandMode;
    [Space(10)]
    #region Melee System

    [Header("Melee System")]
    [Space(5)]
    public GameObject attackSpawn;
    public bool canAttackLeft = true;
    public bool canAttackRight = true;

    [Tooltip("0: Screwdriver, 1: Cleaver, 2: Hammer")]
    public int equippedMeleeLeft;
    public enum SelectedMeleeLeft { Empty, Screwdriver, Cleaver, Hammer };
    public SelectedMeleeLeft selectedMeleeLeft;
    [SerializeField] private int equippedMeleeRight;
    [SerializeField] private enum SelectedMeleeRight { Empty, Screwdriver, Cleaver, Hammer };
    [SerializeField] private SelectedMeleeRight selectedMeleeRight;
    public bool[] meleeObtained = new bool[4] { true, false, false, false };

    [Space(10)]

    [Header("Melee Hitboxes")]
    [Space(5)]
    [SerializeField] private GameObject[] meleeHitboxArray = new GameObject[4];
    public GameObject selectedWeaponHitboxLeft;
    public GameObject selectedWeaponHitboxRight;
    [Space(10)]
    #endregion

    #region Gun System
    [Header("Gun System")]
    [Space(5)]
    [SerializeField] private int equippedGunLeft;
    [SerializeField] private enum SelectedGunLeft { Empty, Pistol, Smg, Shotgun, Revolver };
    [SerializeField] private SelectedGunLeft selectedGunLeft;
    public int equippedGunRight;
    public enum SelectedGunRight { Empty, Pistol, Smg, Shotgun, Revolver };
    public SelectedGunRight selectedGunRight;
    public bool[] gunObtained = new bool[5] { true, false, false, false, false };

    #region Gun Data
    public struct WeaponData
    {
        public string weaponName;
        public int weaponMagSize;
        public int weaponStartingReserveAmmo;
        public float weaponFireRate;
        public float weaponReloadTime;
        public int weaponID;
        public WeaponData(string weaponName, int weaponMagSize, int weaponStartingReserveAmmo, float weaponFireRate, float weaponReloadTime, int weaponID)
        {
            this.weaponName = weaponName;
            this.weaponMagSize = weaponMagSize;
            this.weaponStartingReserveAmmo = weaponStartingReserveAmmo;
            this.weaponFireRate = weaponFireRate;
            this.weaponReloadTime = weaponReloadTime;
            this.weaponID = weaponID;
        }
    }
    // Weapon Structs

    static WeaponData emptyData = new WeaponData("Empty", 0, 0, 0.0f, 0.0f, 0);
    static WeaponData pistolData = new WeaponData("Enforcer 9mm", 12, 9999, 0.5f, 1.5f, 1);
    static WeaponData smgData = new WeaponData("Typewriter", 50, 250, 0.1f, 2.0f, 2);
    static WeaponData shotgunData = new WeaponData("Boomstick", 3, 15, 1.0f, 3.0f, 3);
    static WeaponData revolverData = new WeaponData(".500 Revenant", 5, 15, 2.5f, 4.0f, 4);
    public WeaponData currentWeaponDataLeft;
    public WeaponData currentWeaponDataRight;



    [SerializeField] private GameObject[] weaponBulletPrefabs = new GameObject[5];
    [SerializeField] private int[] weaponAmmoLeft = new int[5];
    [SerializeField] private int[] weaponAmmoRight = new int[5];
    public int[] weaponReserveAmmo = new int[5];
    [SerializeField] private WeaponData[] weaponDataArray = new WeaponData[5];

    private bool handSwitchActive = false;


    #endregion
    #endregion
    void Awake()
    {
        WeaponTesting();
    }

    void WeaponTesting()
    {
        weaponDataArray[1] = pistolData;
        weaponDataArray[2] = smgData;
        weaponDataArray[3] = shotgunData;
        weaponDataArray[4] = revolverData;

        /*for (int i = 0; i < weaponIconsLeft.Length; i++)
        {
            weaponIconsLeft[i].SetActive(false);
            weaponIconsRight[i].SetActive(false);
        }
        weaponIconsLeft[equippedGunLeft].SetActive(true);
        weaponIconsRight[equippedGunRight].SetActive(true);*/

    }

    void WeaponSwitch()
    {
        if (Input.GetKeyDown(leftHandKey) && handSwitchActive == false)
        {
            if (leftHandMode == LeftHandMode.gun)
            {
                equippedGunLeft += 1;
                if (equippedGunLeft >= weaponDataArray.Length)
                {
                    equippedGunLeft = 0;
                }
                SwitchGunLeft();
            }
            else if (leftHandMode == LeftHandMode.melee)
            {
                equippedMeleeLeft += 1;
                if (equippedMeleeLeft > 3)
                {
                    equippedMeleeLeft = 0;
                }
                SwitchMeleeLeft();
            }
            else
            {
                Debug.LogWarning("Left hand mode is not set to anything!");
            }
        }
        if (Input.GetKeyDown(rightHandKey) && handSwitchActive == false)
        {
            if (rightHandMode == RightHandMode.gun)
            {
                equippedGunRight += 1;
                if (equippedGunRight >= weaponDataArray.Length)
                {
                    equippedGunRight = 0;
                }
                SwitchGunRight();
            }
            else if (rightHandMode == RightHandMode.melee)
            {
                equippedMeleeRight += 1;
                if (equippedMeleeRight > 3)
                {
                    equippedMeleeRight = 0;
                }
                SwitchMeleeRight();
            }
            else
            {
                Debug.LogWarning("Right hand mode is not set to anything!");
            }
        }
    }

    void HandModeSwitch()
    {
        if (handSwitchActive == true && Input.GetKey(leftHandKey) && canSwitchLeft == true)
        {
            if (leftHandMode == LeftHandMode.melee)
            {
                leftHandMode = LeftHandMode.gun;
            }
            else if (leftHandMode == LeftHandMode.gun)
            {
                leftHandMode = LeftHandMode.melee;
            }
            StartCoroutine(canSwitchLeftMode());
        }
        if (handSwitchActive == true && Input.GetKey(rightHandKey) && canSwitchRight == true)
        {
            if (rightHandMode == RightHandMode.melee)
            {
                rightHandMode = RightHandMode.gun;
            }
            else if (rightHandMode == RightHandMode.gun)
            {
                rightHandMode = RightHandMode.melee;
            }
            StartCoroutine(canSwitchRightMode());
        }
    }
    private bool canSwitchLeft = true;
    private bool canSwitchRight = true;
    private IEnumerator canSwitchLeftMode()
    {
        canSwitchLeft = false;
        yield return new WaitForSeconds(0.15f);
        canSwitchLeft = true;
    }
    private IEnumerator canSwitchRightMode()
    {
        canSwitchRight = false;
        yield return new WaitForSeconds(0.15f);
        canSwitchRight = true;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MeleeUpdate();
        WeaponSwitch();
        HandModeSwitch();
        HandModifier();
        ShootPen();
    }


    #region Melee System Methods
    void MeleeUpdate()
    {
        SpawnAttack();
        //MeleeWeaponCycle();
    }
    private IEnumerator MeleeCooldownLeft(float cooldown)
    {
        Debug.Log(selectedWeaponHitboxLeft.GetComponent<MeleeHitboxScript>().hitboxDuration);
        canAttackLeft = false;
        yield return new WaitForSeconds(cooldown);
        Debug.Log("cooldown2");
        canAttackLeft = true;
    }
    private IEnumerator MeleeCooldownRight(float cooldown)
    {
        canAttackRight = false;
        yield return new WaitForSeconds(cooldown);
        canAttackRight = true;
    }

    void HandModifier()
    {
        if (Input.GetKeyDown(handSwitchModifierKey))
        {
            handSwitchActive = true;
        }
        if (Input.GetKeyUp(handSwitchModifierKey))
        {
            handSwitchActive = false;
        }
    }
    public void SwitchMeleeLeft()
    {
        switch (equippedMeleeLeft)
        {
            default:
                selectedMeleeLeft = SelectedMeleeLeft.Empty;
                selectedWeaponHitboxLeft = meleeHitboxArray[0];
                return;
            case 0:
                if (meleeObtained[0] == false)
                {
                    Debug.Log("No melee weapon obtained");
                    Debug.Log("Switching to nexrt melee weapon");
                    equippedMeleeLeft++;
                    SwitchMeleeLeft();
                    return;
                }
                else
                {
                    selectedMeleeLeft = SelectedMeleeLeft.Empty;
                    selectedWeaponHitboxLeft = meleeHitboxArray[0];
                    return;
                }
            case 1:
                if (meleeObtained[1] == false)
                {
                    Debug.Log("No melee weapon obtained");
                    Debug.Log("Switching to cleaver");
                    equippedMeleeLeft++;
                    SwitchMeleeLeft();
                    return;
                }
                else
                {
                    selectedMeleeLeft = SelectedMeleeLeft.Screwdriver;
                    selectedWeaponHitboxLeft = meleeHitboxArray[1];
                    return;
                }
            case 2:
                if (meleeObtained[2] == false)
                {
                    Debug.Log("No melee weapon obtained");
                    Debug.Log("Switching to hammer");
                    equippedMeleeLeft++;
                    SwitchMeleeLeft();
                    return;
                }
                else
                {
                    selectedMeleeLeft = SelectedMeleeLeft.Cleaver;
                    selectedWeaponHitboxLeft = meleeHitboxArray[2];
                    return;
                }
            case 3:
                if (meleeObtained[3] == false)
                {
                    Debug.Log("No melee weapon obtained");
                    Debug.Log("Switching to empty");
                    equippedMeleeLeft++;
                    SwitchMeleeLeft();
                    return;
                }
                else
                {
                    selectedMeleeLeft = SelectedMeleeLeft.Hammer;
                    selectedWeaponHitboxLeft = meleeHitboxArray[3];
                    return;
                }
        }
    }
    void SwitchMeleeRight()
    {
        switch (equippedMeleeRight)
        {
            default:
                selectedMeleeRight = SelectedMeleeRight.Empty;
                selectedWeaponHitboxRight = meleeHitboxArray[0];
                return;
            case 0:
                if (meleeObtained[0] == false)
                {
                    equippedMeleeRight++;
                    SwitchMeleeRight();
                    return;
                }
                else
                {
                    selectedMeleeRight = SelectedMeleeRight.Empty;
                    selectedWeaponHitboxRight = meleeHitboxArray[0];
                    return;
                }
            case 1:
                if (meleeObtained[1] == false)
                {
                    equippedMeleeRight++;
                    SwitchMeleeRight();
                    return;
                }
                else
                {
                    selectedMeleeRight = SelectedMeleeRight.Screwdriver;
                    selectedWeaponHitboxRight = meleeHitboxArray[1];
                    return;
                }
            case 2:
                if (meleeObtained[2] == false)
                {
                    equippedMeleeRight++;
                    SwitchMeleeRight();
                    return;
                }
                else
                {
                    selectedMeleeRight = SelectedMeleeRight.Cleaver;
                    selectedWeaponHitboxRight = meleeHitboxArray[2];
                    return;
                }
            case 3:
                if (meleeObtained[3] == false)
                {
                    equippedMeleeRight = 0;
                    SwitchMeleeRight();
                    return;
                }
                else
                {
                    selectedMeleeRight = SelectedMeleeRight.Hammer;
                    selectedWeaponHitboxRight = meleeHitboxArray[3];
                    return;
                }
        }
    }

    void FireLeftGun()
    {
        GameObject bulletFired = Instantiate(weaponBulletPrefabs[(int)selectedGunLeft], attackSpawn.transform.position, attackSpawn.transform.rotation);
    }
    void FireRightGun()
    {
        GameObject bulletFired = Instantiate(weaponBulletPrefabs[(int)selectedGunRight], attackSpawn.transform.position, attackSpawn.transform.rotation);
    }

    private bool canShootLeft = true;
    private bool canShootRight = true;

    private bool reloadPrepared = false;

    void SpawnAttack()
    {
        //Left Hand Attack
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttackLeft && leftHandMode == LeftHandMode.melee && selectedWeaponHitboxLeft != meleeHitboxArray[0])
        {
            Debug.Log("atacando");
            GameObject attack = Instantiate(selectedWeaponHitboxLeft, attackSpawn.transform.position, attackSpawn.transform.rotation);
            attack.transform.parent = attackSpawn.transform;
            //attack.transform.localPosition = new Vector3(0, -0.3f, 1.4f);
            StartCoroutine(MeleeCooldownLeft(attack.GetComponent<MeleeHitboxScript>().hitboxDuration));
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttackLeft && leftHandMode == LeftHandMode.gun && selectedGunLeft != SelectedGunLeft.Empty)
        {
            Debug.Log("Disparando arma izquierda");
            InvokeRepeating(nameof(FireLeftGun), 0.0f, currentWeaponDataLeft.weaponFireRate);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && leftHandMode == LeftHandMode.gun)
        {
            Debug.Log("Dejando de disparar arma izquierda");
            CancelInvoke(nameof(FireLeftGun));
        }
        //Right Hand Attack
        if (Input.GetKeyDown(KeyCode.Mouse1) && canAttackRight && rightHandMode == RightHandMode.melee && selectedWeaponHitboxRight != meleeHitboxArray[0])
        {
            Debug.Log("atacando");
            GameObject attack = Instantiate(selectedWeaponHitboxRight, attackSpawn.transform.position, attackSpawn.transform.rotation);
            attack.transform.parent = attackSpawn.transform;
            //attack.transform.localPosition = new Vector3(0, -0.3f, 1.4f);
            StartCoroutine(MeleeCooldownRight(attack.GetComponent<MeleeHitboxScript>().hitboxDuration));
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && canAttackRight && rightHandMode == RightHandMode.gun && selectedGunRight != SelectedGunRight.Empty)
        {
            Debug.Log("Disparando arma derecha");
            InvokeRepeating(nameof(FireRightGun), 0.0f, currentWeaponDataRight.weaponFireRate);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) && rightHandMode == RightHandMode.gun)
        {
            Debug.Log("Dejando de disparar arma derecha");
            CancelInvoke(nameof(FireRightGun));
        }
    }
    #endregion

    #region Gun System Methods

    void SwitchGunLeft()
    {
        switch (equippedGunLeft)
        {
            default:
                selectedGunLeft = SelectedGunLeft.Empty;
                currentWeaponDataLeft = emptyData;
                return;
            case 0:
                if (gunObtained[0] == false)
                {
                    Debug.Log("No gun obtained");
                    Debug.Log("Switching to next gun");
                    equippedGunLeft++;
                    SwitchGunLeft();
                    return;
                }
                else
                {
                    selectedGunLeft = SelectedGunLeft.Empty;
                    currentWeaponDataLeft = emptyData;
                    return;
                }
            case 1:
                if (gunObtained[1] == false)
                {
                    Debug.Log("No gun obtained");
                    Debug.Log("Switching to next gun");
                    equippedGunLeft++;
                    SwitchGunLeft();
                    return;
                }
                else
                {
                    selectedGunLeft = SelectedGunLeft.Pistol;
                    currentWeaponDataLeft = pistolData;
                    return;
                }
            case 2:
                if (gunObtained[2] == false)
                {
                    Debug.Log("No gun obtained");
                    Debug.Log("Switching to next gun");
                    equippedGunLeft++;
                    SwitchGunLeft();
                    return;
                }
                else
                {
                    selectedGunLeft = SelectedGunLeft.Smg;
                    currentWeaponDataLeft = smgData;
                    return;
                }
            case 3:
                if (gunObtained[3] == false)
                {
                    Debug.Log("No gun obtained");
                    Debug.Log("Switching to next gun");
                    equippedGunLeft++;
                    SwitchGunLeft();
                    return;
                }
                else
                {
                    selectedGunLeft = SelectedGunLeft.Shotgun;
                    currentWeaponDataLeft = shotgunData;
                    return;
                }
            case 4:
                if (gunObtained[4] == false)
                {
                    Debug.Log("No gun obtained");
                    Debug.Log("Switching to next gun");
                    equippedGunLeft = 0;
                    SwitchGunLeft();
                    return;
                }
                else
                {
                    selectedGunLeft = SelectedGunLeft.Revolver;
                    currentWeaponDataLeft = revolverData;
                    return;
                }
        }
    }
    public void SwitchGunRight()
    {
        switch (equippedGunRight)
        {
            default:
                selectedGunRight = SelectedGunRight.Empty;
                currentWeaponDataRight = emptyData;
                return;
            case 0:
                if (gunObtained[0] == false)
                {
                    Debug.Log("No gun obtained");
                    Debug.Log("Switching to next gun");
                    equippedGunRight++;
                    SwitchGunRight();
                    return;
                }
                else
                {
                    selectedGunRight = SelectedGunRight.Empty;
                    currentWeaponDataRight = emptyData;
                    return;
                }
            case 1:
                if (gunObtained[1] == false)
                {
                    Debug.Log("No gun obtained");
                    Debug.Log("Switching to next gun");
                    equippedGunRight++;
                    SwitchGunRight();
                    return;
                }
                else
                {
                    selectedGunRight = SelectedGunRight.Pistol;
                    currentWeaponDataRight = pistolData;
                    return;
                }
            case 2:
                if (gunObtained[2] == false)
                {
                    Debug.Log("No gun obtained");
                    Debug.Log("Switching to next gun");
                    equippedGunRight++;
                    SwitchGunRight();
                    return;
                }
                else
                {
                    selectedGunRight = SelectedGunRight.Smg;
                    currentWeaponDataRight = smgData;
                    return;
                }
            case 3:
                if (gunObtained[3] == false)
                {
                    Debug.Log("No gun obtained");
                    Debug.Log("Switching to next gun");
                    equippedGunRight++;
                    SwitchGunRight();
                    return;
                }
                else
                {
                    selectedGunRight = SelectedGunRight.Shotgun;
                    currentWeaponDataRight = shotgunData;
                    return;
                }
            case 4:
                if (gunObtained[4] == false)
                {
                    Debug.Log("No gun obtained");
                    Debug.Log("Switching to next gun");
                    equippedGunRight = 0;
                    SwitchGunRight();
                    return;
                }
                else
                {
                    selectedGunRight = SelectedGunRight.Revolver;
                    currentWeaponDataRight = revolverData;
                    return;
                }
        }
    }


    #endregion


    public GameObject pen;
    public float penCooldown = 1.0f;
    private bool canThrowPen = true;

    public void ShootPen()
    {
        if (Input.GetKeyDown(KeyCode.G) && canThrowPen)
        {
            StartCoroutine(PenCD());
            GameObject penInstance = Instantiate(pen, attackSpawn.transform.position, attackSpawn.transform.rotation);
            penInstance.transform.parent = attackSpawn.transform;
        }
    }
    private IEnumerator PenCD()
    {
        canThrowPen = false;
        yield return new WaitForSeconds(penCooldown);
        canThrowPen = true;
    }
}
