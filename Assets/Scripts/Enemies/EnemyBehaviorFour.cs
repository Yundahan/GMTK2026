using UnityEngine;

public class EnemyBehaviorFour : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    public void OnPlayerDetected()
    {
        GetComponentInChildren<EnemyAttack>().SetDamageActive(true);
        animator.SetBool("isAttacking", true);
        animator.SetBool("isIdle", false);
        animator.SetBool("isRetracting", false);
    }

    public void OnPlayerLeftDetection()
    {
        GetComponentInChildren<EnemyAttack>().SetDamageActive(false);
        animator.SetBool("isRetracting", true);
        animator.SetBool("isIdle", true);
        animator.SetBool("isAttacking", false);
    }
}
