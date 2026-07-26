using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyList : MonoBehaviour
{
    [SerializeField]
    private float levelEndDelay = 0.6f;
    [SerializeField]
    private string nextLevel = "Scene1";

    private List<EnemyNumber> allEnemies = new();

    private bool allEnemiesDead = false;
    private float allEnemiesDeadTime = -10000f;

    private PlayerSFX playerSFX;

    void Awake()
    {
        playerSFX = GetComponent<PlayerSFX>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if (allEnemiesDead && Time.unscaledTime - allEnemiesDeadTime > levelEndDelay)
        {
            Simulation.Instance().ToggleSimulation();
            SceneLoader.Instance().LoadScene(nextLevel);
        }
    }

    public void RemoveEnemyFromList(EnemyNumber enemy)
    {
        allEnemies.Remove(enemy);
        playerSFX.PlayAudioClip(PlayerSFX.SfxType.ONKILL);

        if (allEnemies.Count == 0)
        {
            allEnemiesDead = true;
            allEnemiesDeadTime = Time.time;
            Simulation.Instance().ToggleSimulation();
        }
    }

    public bool IsHighestNumber(int number)
    {
        if (!allEnemies.Any())
        {
            return true;
        }

        return number >= allEnemies[^1].GetNumber();
    }

    public int GetHighestNumber()
    {
        if (!allEnemies.Any())
        {
            return 0;
        }

        return allEnemies[^1].GetNumber();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        allEnemies = FindObjectsByType<EnemyNumber>(FindObjectsSortMode.None).ToList();
        allEnemies.Sort((x, y) => x.GetNumber().CompareTo(y.GetNumber()));
    }
}
