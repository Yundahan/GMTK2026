using TMPro;
using UnityEngine;

public class MessageArea : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private float messageDuration = 5f;

    private bool messageShown = false;
    private float messageTime = -10000f;

    void Update()
    {
        if (messageShown && messageTime + messageDuration < Time.time)
        {
            HideMessage();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            ShowMessage();
        }
    }

    protected void ShowMessage()
    {
        if (!messageShown)
        {
            messageShown = true;
            messageTime = Time.time;
            text.gameObject.SetActive(true);
        }
    }

    protected void HideMessage()
    {
        text.gameObject.SetActive(false);
    }
}
