using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Slider>().value = VolumeManager.Instance().GetBGMVolume();
    }

    public void SetVolume(float sliderValue)
    {
        VolumeManager.Instance().SetVolume(VolumeManager.VolumeType.BGMVolume, sliderValue);
    }
}
