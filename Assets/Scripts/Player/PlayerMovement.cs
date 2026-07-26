using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerSFX))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Collider2D feetCollider;
    [SerializeField]
    private float SPEED = 7f;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float wallSlidingSpeed = 3f;
    private const int maxJumps = 2;
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
    private PlayerSFX playerSFX;
    private PlayerHealth playerHealth;

    private Vector3 velocity = Vector3.zero;
    private Vector2 spawnPoint;
    // The direction in which the character moves, 0 if no movement
    private float move;
    private bool isFalling = false;
    private bool controlsActive = true;

    private bool wallJumpUsed = false;

    private bool wallrideSoundIsPlaying = false;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerSFX = GetComponent<PlayerSFX>();
        playerHealth = GetComponent<PlayerHealth>();
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
            animator.SetBool("isFalling", true);
            animator.SetBool("isJumping", false);
            animator.SetBool("isSliding", false);
        }
        else
        {
            // Player was falling last frame, but isnt anymore, so we landed
            if (isFalling)
            {
                playerSFX.PlayAudioClip(PlayerSFX.SfxType.LAND);
                isFalling = false;
                animator.SetBool("isFalling", false);
                animator.SetBool("isSliding", false);
                animator.SetBool("isJumping", false);
               
            }
        }

        if (IsTouchingWall() && !IsGrounded())
        {

            if (!wallrideSoundIsPlaying)
            {
                playerSFX.PlayAudioClip(PlayerSFX.SfxType.WALLRIDE);
                wallrideSoundIsPlaying = true;
            }
     
            rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocityX, Mathf.Clamp(rigidBody.linearVelocityY, -wallSlidingSpeed, float.MaxValue));
            animator.SetBool("isSliding", true);
            animator.SetBool("isFalling", false);

           
        }

        if (!IsTouchingWall())
        {
            playerSFX.StopWallride();
            wallrideSoundIsPlaying = false;      
            wallJumpUsed = false;
        }

    }

    public void Move(float horizontalAxis)
    {
        if (controlsActive)
        {
            float xSpeed = SPEED * horizontalAxis;
            Vector3 targetVelocity = new Vector3(xSpeed, rigidBody.linearVelocity.y, 0);
            rigidBody.linearVelocity = Vector3.SmoothDamp(rigidBody.linearVelocity, targetVelocity, ref velocity, IsGrounded() ? SMOOTHING : AIR_SMOOTHING);
            if (rigidBody.linearVelocityX > 0) //if moving direction right look right
            {
                TransformUtils.SetTargetDirection(transform, transform.localScale.x);
                animator.SetBool("isWalking", true);
            }
            else if (rigidBody.linearVelocityX < 0) // if moving direction left look left
            {
                TransformUtils.SetTargetDirection(transform, transform.localScale.x * -1);
                animator.SetBool("isWalking", true);
            }

            if (Mathf.Abs(horizontalAxis) <= 0.01f)
            {
                animator.SetBool("isWalking", false);
            }
        }
    }

    public void Jump()
    {
        if (controlsActive)
        {
            if (IsGrounded() && jumpsRemaining > 1)
            {
                rigidBody.AddForce(new Vector3(0, JUMP_FORCE, 0));
                jumpsRemaining--;
                playerSFX.PlayAudioClip(PlayerSFX.SfxType.JUMP);
                animator.SetBool("isJumping", true);
            }
            else if (!IsGrounded() && jumpsRemaining > 1)
            {
                rigidBody.linearVelocityY = 0;
                rigidBody.AddForce(new Vector3(0, JUMP_FORCE, 0));
                playerSFX.PlayAudioClip(PlayerSFX.SfxType.DOUBLE_JUMP);
                jumpsRemaining--;
                animator.SetBool("isJumping", true);
            }
        }
    }

    public void SetControlActive(bool b)
    {
        controlsActive = b;
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
            if (collider.IsTouching(playerHealth.GetHitbox()))
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
        if (collider.gameObject.layer == WALL_LAYER && !wallJumpUsed)
        {
            jumpsRemaining = maxJumps; // reset remaining jumps when hitting a wall
            wallJumpUsed = true;
        }
    }
}
