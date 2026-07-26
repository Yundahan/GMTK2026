using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class MessageArea : MonoBehaviour
{
    [SerializeField]
    private float messageDuration = 5f;

    private Canvas canvas;

    private bool messageShown = false;
    private float messageTime = -10000f;

    void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
    }

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
            canvas.gameObject.SetActive(true);
        }
    }

    protected void HideMessage()
    {
        canvas.gameObject.SetActive(false);
    }
}
