using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    public enum VolumeType
    {
        MasterVolume,
        BGMVolume,
        SFXVolume
    }

    private static VolumeManager instance;

    [SerializeField]
    private AudioMixer audioMixer;

    // These are variables that persist even when the scene changes.
    Dictionary<string, float> volumes = new Dictionary<string, float>
        {
          {VolumeType.MasterVolume.ToString(), 0.1f },
          {VolumeType.BGMVolume.ToString(), 1f},
          {VolumeType.SFXVolume.ToString(), 1f}
        };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        this.SetMasterVolume(this.volumes[VolumeType.MasterVolume.ToString()]);
    }

    public float GetMasterVolume()
    {
        return this.volumes[VolumeType.MasterVolume.ToString()];
    }

    /// <summary>
    /// Sets the master volume to a given value.
    /// </summary>
    /// <param name="masterVolume">The new master volume value.</param>
    public void SetMasterVolume(float masterVolume)
    {
        this.volumes[VolumeType.MasterVolume.ToString()] = masterVolume;
        audioMixer.SetFloat(VolumeType.MasterVolume.ToString(), Mathf.Log10(masterVolume) * 20);
    }

    /// <summary>
    /// Gets the volume for a volume type.
    /// </summary>
    /// <param name="volumeType">The volume type.</param>
    public float GetVolume(VolumeType volumeType)
    {
        return this.volumes[volumeType.ToString()];
    }

    /// <summary>
    /// Sets a specific volume type to a given value.
    /// </summary>
    /// <param name="volumeType">The volume type.</param>
    /// <param name="volume">The new volume value.</param>
    public void SetVolume(VolumeType volumeType, float volume)
    {
        this.volumes[volumeType.ToString()] = volume;
        audioMixer.SetFloat(volumeType.ToString(), Mathf.Log10(volume) * 20);
    }

    /// <summary>
    /// If the given volume type is muted, set to previous non-muted value, otherwise mute it.
    /// </summary>
    /// <param name="volumeType">The volume type.</param>
    public void ToggleMute(VolumeType volumeType)
    {
        audioMixer.GetFloat(volumeType.ToString(), out float volume);

        if (volume != -80)
        {
            audioMixer.SetFloat(volumeType.ToString(), -80);
        }
        else
        {
            audioMixer.SetFloat(volumeType.ToString(), Mathf.Log10(this.volumes[volumeType.ToString()]) * 20);
        }
    }

    public bool GetMute(VolumeType volumeType)
    {
        audioMixer.GetFloat(volumeType.ToString(), out float volume);
        return volume <= -79.9f;
    }

    public static VolumeManager Instance()
    {
        if (instance == null)
        {
            instance = new VolumeManager();
        }

        return instance;
    }
}
