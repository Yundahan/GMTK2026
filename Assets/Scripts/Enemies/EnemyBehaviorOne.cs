using System;
using UnityEngine;

public class EnemyBehaviorOne : MonoBehaviour
{
    private PlayerMovement player;

    [SerializeField]
    private float detectionRange = 10f;
    [SerializeField]
    private float speed = 5f;

    private Vector3 targetPosition;
    private bool attacking = false;

    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>();
    }

    void Update()
    {
        if (!attacking && Vector3.Distance(transform.position, player.transform.position) < detectionRange)
        {
            attacking = true;
            targetPosition = player.transform.position;
        }
    }

    void FixedUpdate()
    {
        if (attacking)
        {
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
            Vector3 movementVector = targetPosition - transform.position;
            movementVector.Normalize();
            movementVector *= speed * Time.fixedDeltaTime;

            if (movementVector.magnitude > distanceToTarget)
            {
                transform.position = targetPosition;
                attacking = false;
            } else
            {
                transform.position += movementVector;
            }
        }
    }
}
