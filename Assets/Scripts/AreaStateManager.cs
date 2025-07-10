using UnityEngine;

public class AreaStateManager : MonoBehaviour
{
    public enum AreaState
    {
        Unlocked,
        Locked,
        Completed
    }

    public AreaState currentState;
    public GameObject enemyCounter;

    private TMPro.TextMeshProUGUI enemyCounterText;
    public int enemyCount;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        enemyCounter = GameObject.FindWithTag("EnemyCounter");
        enemyCounterText = enemyCounter.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        enemyCount = 5;
    }
    void Start()
    {
        enemyCounter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        OnAreaLock();
        UnlockArea();
        UpdateEnemyCounter();
        ChangeEnemyCount();
    }
    void OnAreaLock()
    {
        if (currentState == AreaState.Locked)
        {
            // Logic to handle area lock
            Debug.Log("Area is locked.");
            enemyCounter.SetActive(true);
        }
    }

    public void UnlockArea()
    {
        if (enemyCount <= 0)
        {
            currentState = AreaState.Unlocked;
            enemyCounter.SetActive(false);
            Debug.Log("Area is unlocked.");
        }

    }

    void ChangeEnemyCount()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            enemyCount++;
            Debug.Log("Enemy Count: " + enemyCount);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            enemyCount--;
            Debug.Log("Enemy Count: " + enemyCount);
        }
    }
    
    void UpdateEnemyCounter()
    {
        enemyCounterText.text = enemyCount.ToString();
    }
}
