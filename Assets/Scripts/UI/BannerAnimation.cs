using UnityEngine;
using UnityEngine.UI;

public class BannerAnimation : MonoBehaviour
{
    [SerializeField]
    private Image bannerLeft;
    [SerializeField]
    private Image bannerMiddle;
    [SerializeField]
    private Image bannerRight;
    [SerializeField]
    private Image bannerHead;


    private float bannerLeftStartPosY;
    private float bannerLeftStartPosX;
    private float bannerMiddleStartPosY;
    private float bannerMiddleStartPosX;
    private float bannerRightStartPosX;
    private float bannerRightStartPosY;
    private float bannerHeadStartPosX;
    private float bannerHeadStartPosY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bannerLeftStartPosY = bannerLeft.rectTransform.anchoredPosition.y;
        bannerLeftStartPosX = bannerLeft.rectTransform.anchoredPosition.x;
        bannerMiddleStartPosY = bannerMiddle.rectTransform.anchoredPosition.y;
        bannerMiddleStartPosX = bannerMiddle.rectTransform.anchoredPosition.x;
        bannerRightStartPosY = bannerRight.rectTransform.anchoredPosition.y;
        bannerRightStartPosX = bannerRight.rectTransform.anchoredPosition.x;
        bannerHeadStartPosY = bannerHead.rectTransform.anchoredPosition.y;
        bannerHeadStartPosX = bannerHead.rectTransform.anchoredPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        //289 Y value on desired banner position (subject to change with actual banner) / works for left/right bc speed is the same
        if (gameObject.activeSelf && bannerLeft.rectTransform.anchoredPosition.y >= 260)
        {
            bannerLeft.rectTransform.anchoredPosition = new Vector2(bannerLeft.rectTransform.anchoredPosition.x, bannerLeft.rectTransform.anchoredPosition.y - 1);
            bannerRight.rectTransform.anchoredPosition = new Vector2(bannerRight.rectTransform.anchoredPosition.x, bannerRight.rectTransform.anchoredPosition.y - 1);
        }
        //289 Y value on desired banner position (subject to change with actual banner)
        if (gameObject.activeSelf && bannerMiddle.rectTransform.anchoredPosition.y >= 538)
        {
            bannerMiddle.rectTransform.anchoredPosition = new Vector2(bannerMiddle.rectTransform.anchoredPosition.x, bannerMiddle.rectTransform.anchoredPosition.y - 2);
        }

        if (gameObject.activeSelf && bannerHead.rectTransform.anchoredPosition.y >= 459)
        {
            bannerHead.rectTransform.anchoredPosition = new Vector2(bannerHead.rectTransform.anchoredPosition.x, bannerHead.rectTransform.anchoredPosition.y - 1);
        }
    }

    public void Reset()
    {
        bannerLeft.rectTransform.anchoredPosition = new Vector2(bannerLeftStartPosX, bannerLeftStartPosY);
        bannerMiddle.rectTransform.anchoredPosition = new Vector2(bannerMiddleStartPosX, bannerMiddleStartPosY);
        bannerRight.rectTransform.anchoredPosition = new Vector2(bannerRightStartPosX, bannerRightStartPosY);
        bannerHead.rectTransform.anchoredPosition = new Vector2(bannerMiddleStartPosX, bannerMiddleStartPosY);
    }

}
