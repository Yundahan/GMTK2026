using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private bool damageActive = true;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (damageActive)
        {
            playerHealth.UpdateHealth(-damage);
        }
    }

    public void SetDamageActive(bool value)
    {
        damageActive = value;
    }
}
