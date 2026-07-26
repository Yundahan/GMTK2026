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
    [SerializeField]
    private float knockbackDuration = 0.1f;
    [SerializeField]
    private float knockbackHeight = 1f;
    [SerializeField]
    private Animator animator;
    private UIManager uiManager;
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;
    private float lastDamageTime = -10000f;
    private PlayerSFX playerSFX;
    private PlayerMovement playerMovement;
    private Rigidbody2D rigidBody;
    private Vector3 enemyPos;
    private float knockbackTimer = -10000f;


    void Start()
    {
        uiManager = FindFirstObjectByType<UIManager>();
        playerSFX = GetComponent<PlayerSFX>();
        playerMovement = GetComponent<PlayerMovement>();
        rigidBody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (knockbackTimer + knockbackDuration < Time.time)
        {
            // allow controls
            playerMovement.SetControlActive(true);
            animator.SetBool("isKnockedback", false);
        }
        else
        {
            // move player
            Vector3 direction = (transform.position - enemyPos).normalized;
            Vector3 directionPlusY = new Vector3(direction.x, direction.y + knockbackHeight, direction.z);
            transform.Translate(knockBackForce * Time.deltaTime * directionPlusY);
            animator.SetBool("isKnockedback", true);
        }
    }

    public void UpdateHealth(int delta, bool knockback)
    {
        if (lastDamageTime + invulnerabilityTimer < Time.time)
        {
            if (delta < 0)
            {
                lastDamageTime = Time.time;
            }
            if (knockback)
            {
                Knockback();
            }

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

    private void Knockback()
    {
        // stop movement and don't allow Inputs
        playerMovement.SetControlActive(false);
        rigidBody.linearVelocityX = 0f;
        rigidBody.linearVelocityY = 0f;
        knockbackTimer = Time.time;
    }

    public void SetEnemyPos(Vector3 enemyPos) => this.enemyPos = enemyPos;
}
