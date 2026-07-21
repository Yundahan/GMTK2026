using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [SerializeField]
    private Sprite onSprite;
    [SerializeField]
    private Sprite offSprite;

    [SerializeField]
    private VolumeManager.VolumeType volumeType;

    void Start()
    {
        if (VolumeManager.Instance().GetMute(volumeType))
        {
            GetComponent<Image>().sprite = offSprite;
        }
        else
        {
            GetComponent<Image>().sprite = onSprite;
        }

        GetComponent<Button>().onClick.AddListener(ToggleMute);
    }

    public void ToggleMute()
    {
        VolumeManager.Instance().ToggleMute(volumeType);
        UpdateSprite();
    }

    public void UpdateSprite()
    {
        if (GetComponent<Image>().sprite == onSprite)
        {
            GetComponent<Image>().sprite = offSprite;
        }
        else
        {
            GetComponent<Image>().sprite = onSprite;
        }
    }
}
