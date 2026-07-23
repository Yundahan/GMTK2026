using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Vector3 cameraOffset;
    private PlayerMovement player;
    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + cameraOffset.x, player.transform.position.y + cameraOffset.y, cameraOffset.z);
    }
}
