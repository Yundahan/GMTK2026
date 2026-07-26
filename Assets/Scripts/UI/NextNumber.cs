using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextNumber : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textComponent;
    private EnemyList enemyList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyList = FindFirstObjectByType<EnemyList>();
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.text = enemyList.GetHighestNumber().ToString();
    }
}
