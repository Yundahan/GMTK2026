using UnityEngine;

public class DetectionArea : MonoBehaviour
{
    private Collider2D playerCollider;

    void Start()
    {
        playerCollider = FindFirstObjectByType<PlayerHealth>().GetHitbox();
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider == playerCollider)
        {
            transform.parent.BroadcastMessage("OnPlayerDetected", this, SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider == playerCollider)
        {
            transform.parent.BroadcastMessage("OnPlayerLeftDetection", this, SendMessageOptions.DontRequireReceiver);
        }
    }
}
