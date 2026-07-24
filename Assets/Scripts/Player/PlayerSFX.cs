using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public enum SfxType
    {
        JUMP,
        DOUBLE_JUMP
    }

    private AudioSource audioSource;

    private Dictionary<SfxType, int> trackCount = new Dictionary<SfxType, int>
    {
        { SfxType.JUMP, 12 },
        { SfxType.DOUBLE_JUMP, 10 }
    };

    private Dictionary<SfxType, string> trackBaseName = new Dictionary<SfxType, string>
    {
        { SfxType.JUMP, "Sound/SFX/Jump" },
        { SfxType.DOUBLE_JUMP, "Sound/SFX/DoubleJump" }
    };

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioClip(SfxType type)
    {
        int randomCount = Random.Range(1, trackCount[type]);
        string clipName = trackBaseName[type] + randomCount;
        AudioClip clip = Resources.Load<AudioClip>(clipName);
        audioSource.clip = clip;
        audioSource.Play();
    }
}
