using UnityEngine;

public class Pathing : MonoBehaviour
{
    public bool isPathing = true;
    [SerializeField]
    private Vector2 boxSize; //with a 1x1 sized object  x=1 and y=1 
    [SerializeField]
    private float castDist; // with a 1x1 sized object castDist = 1 
    [SerializeField]
    public float moveSpeed = 5f;
    [SerializeField]
    private float rayDist = 1f;
    [SerializeField]
    private float initialDirection = 1f;//1f is right, -1f is left
    private int groundLayer;
    private int wallLayer;

    void Start()
    {
        wallLayer = LayerMask.GetMask("Wall");
        groundLayer = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        if (isPathing)
        {
            WalkPattern();
        }
    }

    private void WalkPattern()
    {
        if (IsGroundAhead(transform.right) && !IsWallOrGroundAhead(transform.right) && initialDirection * transform.localScale.x > 0) //check right
        {
            //move right
            transform.Translate(moveSpeed * Time.deltaTime * Vector2.right);
        }
        else if (initialDirection * transform.localScale.x > 0)
        {
            TransformUtils.FlipScale(transform);
        }
        if (IsGroundAhead(-transform.right) && !IsWallOrGroundAhead(-transform.right) && initialDirection * transform.localScale.x < 0) // check left
        {
            //move left
            transform.Translate(moveSpeed * Time.deltaTime * -Vector2.right);
        }
        else if (initialDirection * transform.localScale.x < 0)
        {
            TransformUtils.FlipScale(transform);
        }
    }

    private bool IsGroundAhead(Vector3 direction)
    {
        return Physics2D.BoxCast(transform.position + direction, boxSize, 0, -transform.up, castDist, groundLayer);
    }

    private bool IsWallOrGroundAhead(Vector3 direction)
    {
        return Physics2D.Raycast(transform.position, direction, rayDist, wallLayer) ||
            Physics2D.Raycast(transform.position, direction, rayDist, groundLayer);
    }
}
