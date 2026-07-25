using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Vector3 cameraOffset;
    private Camera cam;
    private PlayerMovement player;
    [SerializeField]
    private Collider2D bounds;



    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>();
        cam = GetComponent<Camera>();
    }
    // Update is called once per frame
    private void LateUpdate()
    {

        (float camX, float camY) = calculateCameraBounds();
        cam.transform.position = new Vector3(camX + cameraOffset.x, camY + cameraOffset.y, cam.transform.position.z);
    }

    private (float camX, float camY) calculateCameraBounds()
    {
        float camHorizontalExtent = cam.orthographicSize;
        float camVerticalExtent = camHorizontalExtent / cam.aspect;
        Vector3 minBounds = bounds.bounds.min;
        Vector3 maxBounds = bounds.bounds.max;
        float leftBound = minBounds.x + camHorizontalExtent + cameraOffset.x;
        float rightBound = maxBounds.x - camHorizontalExtent - cameraOffset.x;
        float bottomBound = minBounds.y + camVerticalExtent + cameraOffset.y;
        float topBound = maxBounds.y - camVerticalExtent - cameraOffset.y;

        float camX = Mathf.Clamp(player.transform.position.x, leftBound, rightBound);
        float camY = Mathf.Clamp(player.transform.position.y, bottomBound, topBound);

        return (camX, camY);
    }
}
