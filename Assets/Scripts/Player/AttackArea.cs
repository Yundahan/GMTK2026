using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 5;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Health>() != null)
        {
            Health health = collider.GetComponent<Health>();
            health.Damage(damage);
        }
    }
}
