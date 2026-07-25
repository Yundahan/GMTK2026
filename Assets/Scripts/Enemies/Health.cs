using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;

    private SpriteRenderer[] spriteRenderers;
    private EnemyList enemyList;

    private const float minOpacity = 0.2f;

    private int maxHealth;
    [SerializeField]
    private bool invulnerable = false;

    private EnemySFX enemySFX;


    private void Awake()
    {
        enemySFX= GetComponent<EnemySFX>();
    }

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
        float opacity = minOpacity + Mathf.Clamp01((float)health / (float)maxHealth) * (1f - minOpacity);

        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.color = new Color(1f, 1f, 1f, opacity);
        }

        if (health <= 0)
        {
            enemySFX.PlayAudioClip(EnemySFX.SfxType.ONKILL);
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
