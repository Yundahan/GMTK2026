using UnityEditor.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private Collider2D hitbox;
    [SerializeField]
    private float invulnerabilityTimer = 1f;
    [SerializeField]
    private float knockBackForce = 10f;

    private UIManager uiManager;

    private int maxHealth = 100;
    private int currentHealth = 100;
    private float lastDamageTime = -10000f;

    private PlayerSFX playerSFX;
    private Rigidbody2D rigidBody;
    private Vector3 enemyPos;

    void Start()
    {
        uiManager = FindFirstObjectByType<UIManager>();
        playerSFX = GetComponent<PlayerSFX>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void UpdateHealth(int delta)
    {
        if (lastDamageTime + invulnerabilityTimer < Time.time)
        {
            if (delta < 0)
            {
                lastDamageTime = Time.time;
            }
            Knockback(GetEnemyPos());
            currentHealth = Mathf.Min(currentHealth + delta, maxHealth);
            float healthFraction = (float)currentHealth / (float)maxHealth;
            healthFraction = Mathf.Clamp01(healthFraction);
            uiManager.SetHealthBar(healthFraction);
            playerSFX.PlayAudioClip(PlayerSFX.SfxType.ONHIT);
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public Collider2D GetHitbox()
    {
        return hitbox;
    }

    private void Die()
    {
        uiManager.ActivateDeathMenu();
    }

    private void Knockback(Vector3 enemyPos)
    {
        rigidBody.AddForce((rigidBody.transform.position - enemyPos) * knockBackForce);

    }

    public void SetEnemyPos(Vector3 enemyPos) => this.enemyPos = enemyPos;
    private Vector3 GetEnemyPos()
    {
        return enemyPos;
    }
}
