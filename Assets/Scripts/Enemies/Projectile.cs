using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float lifeSpan = 5f;
    private const int GROUND_LAYER = 6;
    private const int WALL_LAYER = 7;

    private Vector3 direction;
    private float spawnTime = 0f;

    void FixedUpdate()
    {
        if (spawnTime + lifeSpan < Time.time)
        {
            Destroy(gameObject);
        }

        transform.position += Time.fixedDeltaTime * direction;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == GROUND_LAYER || collider.gameObject.layer == WALL_LAYER)
        {
            Destroy(gameObject);
        }
    }

    public void Init(Vector3 direction)
    {
        this.direction = direction;
        this.spawnTime = Time.time;
    }

    public void PlayerDamaged()
    {
        Destroy(gameObject);
    }
}
