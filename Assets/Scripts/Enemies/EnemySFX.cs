using System.Collections.Generic;
using UnityEngine;

public class EnemySFX : MonoBehaviour
{
    public enum SfxType
    {
       ONKILL
    }

    private AudioSource audioSource;

    private Dictionary<SfxType, SFXData> clipData = new Dictionary<SfxType, SFXData>
    {
        { SfxType.ONKILL, new SFXData(9, "Sound/SFX/OnKillGeneric", 1f) },
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
        audioSource.Play();
    }
}
