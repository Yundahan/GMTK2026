using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyList : MonoBehaviour
{
    private List<EnemyNumber> allEnemies = new ();

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void RemoveEnemyFromList(EnemyNumber enemy)
    {
        allEnemies.Remove(enemy);
    }

    public bool IsHighestNumber(int number)
    {
        if (!allEnemies.Any())
        {
            return true;
        }

        return number >= allEnemies[^1].GetNumber();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        allEnemies = FindObjectsByType<EnemyNumber>(FindObjectsSortMode.None).ToList();
        allEnemies.Sort((x, y) => x.GetNumber().CompareTo(y.GetNumber()));
    }
}
