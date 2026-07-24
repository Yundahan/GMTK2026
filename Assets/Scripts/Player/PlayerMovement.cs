using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Collider2D feetCollider;
    [SerializeField]
    private Collider2D frontCollider;
    [SerializeField]
    private float SPEED = 7f;
    [SerializeField]
    private float wallSlidingSpeed = 3f;
    private int maxJumps = 2;
    private int jumpsRemaining;

    private const int GROUND_LAYER = 6;
    private const int WALL_LAYER = 7;
    [SerializeField]
    private float JUMP_FORCE = 200f;
    [SerializeField]
    private float SMOOTHING = 0.1f;
    [SerializeField]
    private float AIR_SMOOTHING = 0.2f;



    private List<Collider2D> groundColliders = new();
    private List<Collider2D> wallColliders = new();
    private Rigidbody2D rigidBody;


    private Vector3 velocity = Vector3.zero;
    private Vector2 spawnPoint;
    // The direction in which the character moves, 0 if no movement
    private float move;
    private bool isFalling = false;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spawnPoint = transform.position;

        foreach (Collider2D collider2D in FindObjectsByType<Collider2D>(FindObjectsSortMode.None))
        {
            if (collider2D.gameObject.layer == GROUND_LAYER)
            {
                groundColliders.Add(collider2D);
            }
            if (collider2D.gameObject.layer == WALL_LAYER)
            {
                wallColliders.Add(collider2D);
            }
        }
    }

    void FixedUpdate()
    {
        Move(move);

        if (rigidBody.linearVelocity.y < 0 && !IsGrounded())
        {
            isFalling = true;
        }
        else
        {
            // Player was falling last frame, but isnt anymore, so we landed
            if (isFalling)
            {
                isFalling = false;
            }
        }

        if (IsTouchingWall() && !IsGrounded())
        {
            rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocityX, Mathf.Clamp(rigidBody.linearVelocityY, -wallSlidingSpeed, float.MaxValue));
        }

    }

    public void Move(float horizontalAxis)
    {
        float xSpeed = SPEED * horizontalAxis;
        Vector3 targetVelocity = new Vector3(xSpeed, rigidBody.linearVelocity.y, 0);
        rigidBody.linearVelocity = Vector3.SmoothDamp(rigidBody.linearVelocity, targetVelocity, ref velocity, IsGrounded() ? SMOOTHING : AIR_SMOOTHING);
        if (rigidBody.linearVelocityX > 0) //if moving direction right look right
        {
            TransformUtils.SetTargetDirection(transform, transform.localScale.x);
        }
        else if (rigidBody.linearVelocityX < 0) // if moving direction left look left
        {
            TransformUtils.SetTargetDirection(transform, transform.localScale.x * -1);
        }

    }

    public void Jump()
    {
        if (IsGrounded() && jumpsRemaining > 1)
        {
            rigidBody.AddForce(new Vector3(0, JUMP_FORCE, 0));
            jumpsRemaining--;
        }
        else if (!IsGrounded() && jumpsRemaining > 1)
        {
            rigidBody.linearVelocityY = 0;
            rigidBody.AddForce(new Vector3(0, JUMP_FORCE, 0));
            jumpsRemaining--;
        }
    }

    public bool IsGrounded()
    {
        foreach (Collider2D collider in groundColliders)
        {
            if (collider.IsTouching(feetCollider))
            {
                jumpsRemaining = maxJumps;
                return true;
            }
        }
        return false;
    }

    public bool IsTouchingWall()
    {
        foreach (Collider2D collider in wallColliders)
        {
            if (collider.IsTouching(frontCollider))
            {
                return true;
            }
        }
        return false;
    }

    public void Reset()
    {
        transform.position = spawnPoint;
        velocity = Vector3.zero;
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.layer == WALL_LAYER)
        {
            jumpsRemaining = maxJumps; // reset remaining jumps when hitting a wall
        }
    }
}
