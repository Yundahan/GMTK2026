using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public enum SfxType
    {
        JUMP,
        DOUBLE_JUMP,
        LAND,
        ONHIT,
        ONKILL
    }

    private AudioSource audioSource;

    private Dictionary<SfxType, SFXData> clipData = new Dictionary<SfxType, SFXData>
    {
        { SfxType.JUMP, new SFXData(12, "Sound/SFX/Jump", 1f) },
        { SfxType.DOUBLE_JUMP, new SFXData(10, "Sound/SFX/DoubleJump", 1f) },
        { SfxType.LAND, new SFXData (8, "Sound/SFX/Land", 0.3f)},
        { SfxType.ONHIT, new SFXData (7, "Sound/SFX/OnHit", 1f)},
        { SfxType.ONKILL, new SFXData(9, "Sound/SFX/OnKillGeneric", 1f) }
    };

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioClip(SfxType type)
    {
        SFXData sfxData = clipData[type];

        if (Random.Range(0f, 1f) > sfxData.GetClipPlayChance())
        {
            return;
        }

        int randomCount = Random.Range(1, sfxData.GetNumberOfClips());
        string clipName = sfxData.GetClipBaseName() + randomCount;
        AudioClip clip = Resources.Load<AudioClip>(clipName);
        audioSource.clip = clip;
        audioSource.volume = 0.8f;
        audioSource.Play();
    }
}
