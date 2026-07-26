using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject attackArea = default;
    [SerializeField]
    private Animator animator;

    private bool attacking = false;
    [SerializeField]
    private float timeToAttack = 0.25f;
    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            timer += Time.deltaTime;

            if (timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
                animator.SetBool("isAttacking", false);
            }
        }
    }

    public void Attack()
    {
        attacking = true;
        attackArea.SetActive(attacking);
        animator.SetBool("isAttacking", true);
    }
}
