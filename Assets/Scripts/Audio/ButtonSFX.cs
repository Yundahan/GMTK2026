using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonSFX : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlaySFXOnClick);
    }

    void PlaySFXOnClick()
    {
        FindFirstObjectByType<BGMManager>().ButtonPressed();
    }
    
}
