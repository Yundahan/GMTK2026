using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    private Button nextScene;
    [SerializeField]
    private Button restart;
    void Awake()
    {
        nextScene.onClick.AddListener(NextScene);
        restart.onClick.AddListener(Restart);
    }

    private void NextScene()
    {
        SceneLoader.Instance().LoadScene(1 - SceneLoader.Instance().GetActiveSceneBuildIndex());
    }

    private void Restart()
    {
        SceneLoader.Instance().ReloadScene();
    }
}
