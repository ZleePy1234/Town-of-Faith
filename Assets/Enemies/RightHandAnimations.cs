using UnityEngine;

public class RightHandAnimations : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Moving()
    {
        animator.SetBool("Moving", true);
    }
    public void NotMoving()
    {
        animator.SetBool("Moving", false);
    }
    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
