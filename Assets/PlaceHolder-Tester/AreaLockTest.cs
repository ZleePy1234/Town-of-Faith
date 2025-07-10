using UnityEngine;
using UnityEngine.UIElements;

public class AreaLockTest : MonoBehaviour
{

    private Animator animator;
    public BoxCollider boxCollider;
    public BoxCollider triggerCollider;
    private AreaStateManager areaManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        animator = GetComponent<Animator>();
        areaManager = GameObject.FindWithTag("AreaManager").GetComponent<AreaStateManager>();

    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("Locked");
            Debug.Log("Player triggered lock");
            boxCollider.enabled = true;
            triggerCollider.enabled = false;
            areaManager.currentState = AreaStateManager.AreaState.Locked;
        }
    }

    void Update()
    {
        AreaUnlocked();
    }
    void AreaUnlocked()
    {
        if(areaManager.enemyCount <= 0)
        {
            animator.SetTrigger("Destroyed");
            Debug.Log("Area is unlocked");
            boxCollider.enabled = false;
            triggerCollider.enabled = false;
            areaManager.currentState = AreaStateManager.AreaState.Unlocked;
        }
        
    }
}
