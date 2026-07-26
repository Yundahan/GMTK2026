using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonSFX : MonoBehaviour
{
    private Button button;
    
    [SerializeField]
    private AudioSource audioSource;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlaySFXOnClick);
    }

    void PlaySFXOnClick()
    {
        audioSource.Play();
    }
    
}
