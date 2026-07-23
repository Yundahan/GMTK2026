using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private int health = 100;

    private bool invulnerable = false;

    public void Damage(int amount)
    {
        if (amount < 0 || invulnerable)
        {
            return;
        }

        this.health -= amount;

        if (health <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        Debug.Log("I am Dead!");
        Destroy(gameObject);
    }

    public void SetInvulnerability(bool value)
    {
        invulnerable = value;
    }
}
