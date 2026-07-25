using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public string sceneName = "Level01";

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadLevel);
    }

    public void LoadLevel()
    {
        //Simulation between lvl changes
        SceneLoader.Instance().LoadScene(sceneName);
    }
}