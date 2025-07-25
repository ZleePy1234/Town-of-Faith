using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public int enemyCount;
    private TMPro.TextMeshProUGUI counter;
    private void Awake()
    {
        counter = GetComponent<TMPro.TextMeshProUGUI>();
        enemyCount = 0;
    }
    public void Update()
    {
        counter.text = enemyCount.ToString();
    }
}
