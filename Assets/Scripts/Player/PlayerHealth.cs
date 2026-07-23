using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private Collider2D hitbox;
    [SerializeField]
    private float invulnerabilityTimer = 1f;

    private UIManager uiManager;

    private int maxHealth = 100;
    private int currentHealth = 100;
    private float lastDamageTime = -10000f;

    void Start()
    {
        uiManager = FindFirstObjectByType<UIManager>();
    }

    public void UpdateHealth(int delta)
    {
        if (lastDamageTime + invulnerabilityTimer < Time.time)
        {
            if (delta < 0)
            {
                lastDamageTime = Time.time;
            }

            currentHealth = Mathf.Min(currentHealth + delta, maxHealth);
            float healthFraction = (float)currentHealth / (float)maxHealth;
            healthFraction = Mathf.Clamp01(healthFraction);
            uiManager.SetHealthBar(healthFraction);
        }
    }

    public Collider2D GetHitbox()
    {
        return hitbox;
    }
}
