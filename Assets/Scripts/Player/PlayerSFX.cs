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
        ONKILL,
        ATTACK
    }
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioSource audioSource2;

    private Dictionary<SfxType, SFXData> clipData = new Dictionary<SfxType, SFXData>
    {
        { SfxType.JUMP, new SFXData(12, "Sound/SFX/Jump", 0.7f, false)},
        { SfxType.DOUBLE_JUMP, new SFXData(10, "Sound/SFX/DoubleJump", 0.8f, false) },
        { SfxType.LAND, new SFXData (3, "Sound/SFX/Land", 1f, false)},
        { SfxType.ONHIT, new SFXData (7, "Sound/SFX/OnHit", 1f, false)},
        { SfxType.ONKILL, new SFXData(20, "Sound/SFX/OnKillGeneric", 1f, true) },
        { SfxType.ATTACK, new SFXData(1, "Sound/SFX/Attack", 1f, false) }

    };

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
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

        if (sfxData.IsQuote())
        {
            audioSource2.clip = clip;
            audioSource2.volume = 1f;
            audioSource2.Play();
        }
        else
        {
            audioSource.clip = clip;
            audioSource.volume = 0.8f;
            audioSource.Play();
        }


    }
}
