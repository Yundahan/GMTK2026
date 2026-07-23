using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private Collider2D damageArea;
    [SerializeField]
    private int damage = 1;

    private PlayerHealth playerHealth;
    private bool damageActive = true;

    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (damageActive && damageArea.IsTouching(playerHealth.GetHitbox()))
        {
            playerHealth.UpdateHealth(-damage);
        }
    }

    public void SetDamageActive(bool value)
    {
        damageActive = value;
    }
}
