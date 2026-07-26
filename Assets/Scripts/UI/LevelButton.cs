using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "Level1";

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadLevel);
    }

    public void LoadLevel()
    {
        //Simulation between lvl changes
        FindFirstObjectByType<BGMManager>().ToggleMuffle();
        SceneLoader.Instance().LoadScene(sceneName);
    }
}