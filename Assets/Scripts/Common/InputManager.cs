using System.Collections;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    private PlayerMovement player;
    private UIManager uiManager;

    private InputAction moveAction;

    string guid = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log(Simulation.Instance().IncreaseECount());
        }
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            uiManager.ToggleMenu();
            Simulation.Instance().ToggleSimulation();
        }
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            guid = uiManager.ShowTextInGameGUI(new Vector2(0f, 1f), new Vector2(250f, -120f), "hier ist ein text");
        }
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            uiManager.RemoveTextInGameGUI(guid);
        }
        if (Keyboard.current.vKey.wasPressedThisFrame)
        {
            uiManager.RemoveAllTextsInGameGUI();
        }

        if (player != null)
        {
            player.Move(moveAction.ReadValue<Vector2>().x);

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                player.Jump();
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindFirstObjectByType<PlayerMovement>();
        uiManager = GameObject.FindFirstObjectByType<UIManager>();
    }

    public static InputManager Instance()
    {
        if (instance == null)
        {
            instance = new InputManager();
        }

        return instance;
    }
}
