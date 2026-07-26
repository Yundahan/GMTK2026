using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;

    [SerializeField]
    private AudioSource bgmAudioSource;

    [SerializeField]
    private AudioSource godSFX;

    [SerializeField]
    private AudioMixer mixer;

    private Dictionary<string, string> sceneToBGMMapping = new Dictionary<string, string>
        {
          {"default", "Sound/Bilderbuchabenteuer" },
          {"AndrikScene", "Sound/AhriTheme" },
          {"DirkScene4", "Sound/Zahlentaifun" },
          {"DirkScene5", "Sound/Zahlentaifun" },
          {"Level1", "Sound/Tutanchatorial" }


        };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            bgmAudioSource = GetComponent<AudioSource>();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        this.transform.position = Camera.main.transform.position;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string activeSceneName = SceneLoader.Instance().GetActiveSceneName();
        if (sceneToBGMMapping.ContainsKey(activeSceneName))
        {
            if (!IsTrackCurrentlyPlaying(sceneToBGMMapping[activeSceneName]))
            {
                AudioClip clip = Resources.Load<AudioClip>(sceneToBGMMapping[activeSceneName]);
                bgmAudioSource.clip = clip;
                bgmAudioSource.Play();
            }
        }
        else
        {
            if (!IsTrackCurrentlyPlaying(sceneToBGMMapping["default"]))
            {
                AudioClip clip = Resources.Load<AudioClip>(sceneToBGMMapping["default"]);
                bgmAudioSource.clip = clip;
                bgmAudioSource.Play();
            }
        }


    }

    /// <summary>
    /// Changes the stored BGM file for a scene. If the scene is currently loaded, the BGM will be changed accordingly.
    /// </summary>
    /// <param name="sceneName">Name of the scene.</param>
    /// <param name="bgmFilePath">File path of the new BGM.</param>
    public void SetBGMForScene(string sceneName, string bgmFilePath)
    {
        if (sceneToBGMMapping.ContainsKey(sceneName))
        {
            sceneToBGMMapping[sceneName] = bgmFilePath;
        }
        else
        {
            sceneToBGMMapping["default"] = bgmFilePath;
        }

        if (SceneLoader.Instance().GetActiveSceneName() == sceneName
            && !IsTrackCurrentlyPlaying(bgmFilePath))
        {
            bgmAudioSource.clip = Resources.Load<AudioClip>(bgmFilePath);
        }
    }

    /// <summary>
    /// Checks if the BGM from a given audio file is currently playing.
    /// </summary>
    /// <param name="bgmFilePath">Path of the BGM file.</param>
    public bool IsTrackCurrentlyPlaying(string bgmFilePath)
    {


        string[] pathArray = bgmFilePath.Split('/');
        string fileName = pathArray[pathArray.Length - 1];
        return bgmAudioSource.clip != null && fileName == bgmAudioSource.clip.name;
    }

    public void PlayLevelTransition()
    {
        AudioClip clip = Resources.Load<AudioClip>("Sound/SFX/NextLevelSFX");
        godSFX.PlayOneShot(clip);
    }

    public static BGMManager Instance()
    {
        if (instance == null)
        {
            instance = new BGMManager();
        }

        return instance;
    }

    public void ToggleMuffle()
    {
        mixer.GetFloat("Wet", out float wetness);
        if (wetness == 0)
        {
            mixer.SetFloat("Wet", -80f);

        }
        else
        {
            mixer.SetFloat("Wet", 0);
        }
    }
    

}