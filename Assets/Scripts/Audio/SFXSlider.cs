using UnityEngine;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Slider>().value = VolumeManager.Instance().GetSFXVolume();
    }

    public void SetVolume(float sliderValue)
    {
        VolumeManager.Instance().SetVolume(VolumeManager.VolumeType.SFXVolume, sliderValue);
    }
}
