using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float lifeSpan = 5f;

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

    public void Init(Vector3 direction)
    {
        this.direction = direction;
        this.spawnTime = Time.time;
    }
}
