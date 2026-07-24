using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private int health = 100;

    private EnemyList enemyList;

    private bool invulnerable = false;

    void Start()
    {
        enemyList = FindFirstObjectByType<EnemyList>();
    }

    public void Damage(int amount)
    {
        if (amount < 0 || invulnerable || !enemyList.IsHighestNumber(GetComponent<EnemyNumber>().GetNumber()))
        {
            return;
        }

        health -= amount;

        if (health <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        enemyList.RemoveEnemyFromList(GetComponent<EnemyNumber>());
        Destroy(gameObject);
    }

    public void SetInvulnerability(bool value)
    {
        invulnerable = value;
    }
}
