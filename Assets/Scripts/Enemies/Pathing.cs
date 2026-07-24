using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Pathing : MonoBehaviour
{
    public bool isPathing = true;
    [SerializeField]
    private Vector2 boxSize; //with a 1x1 sized object  x=1 and y=1 
    [SerializeField]
    private float castDist; // with a 1x1 sized object castDist = 1 
    [SerializeField]
    private LayerMask groundLayer;
    public float moveSpeed = 5f;


    void Update()
    {
        if (isPathing)
        {
            WalkPattern();
        }

    }

    private bool IsGroundAhead(Vector3 direction)
    {
        if (Physics2D.BoxCast(transform.position + direction, boxSize, 0, -transform.up, castDist, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void FlipScale()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void WalkPattern()
    {
        if (IsGroundAhead(transform.right) && transform.localScale.x > 0) //check right
        {
            //move right
            transform.Translate(moveSpeed * Time.deltaTime * Vector2.right);
        }
        else if (transform.localScale.x > 0)
        {
            FlipScale();
        }
        if (IsGroundAhead(-transform.right) && transform.localScale.x < 0) // check left
        {
            //move left
            transform.Translate(moveSpeed * Time.deltaTime * -Vector2.right);
        }
        else if (transform.localScale.x < 0)
        {
            FlipScale();
        }
    }
}
