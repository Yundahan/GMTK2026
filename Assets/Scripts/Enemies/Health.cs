using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;

    private SpriteRenderer[] spriteRenderers;
    private EnemyList enemyList;

    private const float minOpacity = 0.2f;

    private int maxHealth;
    private bool invulnerable = false;

    void Start()
    {
        enemyList = FindFirstObjectByType<EnemyList>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        maxHealth = health;
    }

    public void Damage(int amount)
    {
        if (amount < 0 || invulnerable || !enemyList.IsHighestNumber(GetComponent<EnemyNumber>().GetNumber()))
        {
            return;
        }

        health -= amount;
        float opacity = 1f - Mathf.Clamp01((float)health / (float)maxHealth) * (1f - minOpacity);

        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.color = new Color(1f, 1f, 1f, opacity);
        }

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
