using System.Collections;
using UnityEngine;

public class Simulation
{
    private static Simulation instance;

    /// <summary>
    /// Pauses the simulation if previously unpaused, unpauses the simulation if previously paused.
    /// Only those actions are paused which depend on Time.deltaTime!
    /// </summary>
    public void ToggleSimulation()
    {
        Time.timeScale = 1f -Time.timeScale;
    }

    /// <summary>
    /// Resets the current scene.
    /// </summary>
    public void Reset()
    {
        SceneLoader.Instance().ReloadScene();
    }

    public bool IsSimulating()
    {
        return Time.timeScale > 0.5f;
    }

    public static Simulation Instance()
    {
        if (instance == null)
        {
            instance = new Simulation();
        }

        return instance;
    }
}
