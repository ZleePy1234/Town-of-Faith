using UnityEngine;
using TMPro;

public class DPSdummy : MonoBehaviour
{
    public float damageTimer = 0f;
    public float dpsUpdateInterval = 1f;
    public int damageThisSecond = 0;
    public float lastDPS = 0f;
    public TextMeshProUGUI tmp;

    void Start()
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        if (tmp != null)
            tmp.text = "DPS: 0";
    }

    void Update()
    {
        damageTimer += Time.deltaTime;
        if (damageTimer >= dpsUpdateInterval)
        {
            lastDPS = damageThisSecond / dpsUpdateInterval;
            if (tmp != null)
                tmp.text = $"DPS: {lastDPS:F1}";
            damageThisSecond = 0;
            damageTimer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);
        if (other.CompareTag("Bala"))
        {
            BalaScript balaScript = other.GetComponent<BalaScript>();
            if (balaScript != null)
            {
                TakeDamage(balaScript.bulletDamage);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        damageThisSecond += amount;
    }
}
