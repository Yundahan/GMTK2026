using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Vector3 cameraOffset;
    private PlayerMovement player;

    private float sideScrollDistance = 2f;
    [SerializeField]
    private float smoothTime = 1;
    [SerializeField]
    private Vector3 velocity = new Vector3(0, 0, 5);
    private Vector3 targetPos;


    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x - transform.position.x < -sideScrollDistance)
        {
            targetPos = new Vector3(player.transform.position.x + sideScrollDistance, player.transform.position.y, cameraOffset.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        }
        else if (player.transform.position.x - transform.position.x > sideScrollDistance)
        {
            targetPos = new Vector3(player.transform.position.x - sideScrollDistance, player.transform.position.y, cameraOffset.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        }
        else if (player.transform.position.y - transform.position.y < -sideScrollDistance)
        {
            targetPos = new Vector3(cameraOffset.x, player.transform.position.y + sideScrollDistance, cameraOffset.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        }
        else if (player.transform.position.y - transform.position.y > sideScrollDistance)
        {
            targetPos = new Vector3(cameraOffset.x, player.transform.position.y - sideScrollDistance, cameraOffset.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        }

    }
}
