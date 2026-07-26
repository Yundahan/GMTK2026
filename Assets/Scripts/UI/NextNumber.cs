using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextNumber : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textComponent;
    [SerializeField]
    private Image targetImage;
    [SerializeField]
    private Sprite n1;
    [SerializeField]
    private Sprite n2;
    [SerializeField]
    private Sprite n3;
    [SerializeField]
    private Sprite n4;
    [SerializeField]
    private Sprite n5;
    [SerializeField]
    private Sprite n6;
    [SerializeField]
    private Sprite n7;
    [SerializeField]
    private Sprite n8;
    [SerializeField]
    private Sprite n9;

    private EnemyList enemyList;
    Dictionary<int, Sprite> spriteDict = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyList = FindFirstObjectByType<EnemyList>();
        spriteDict = new Dictionary<int, Sprite>()
        {
            {1, n1},
            {2, n2},
            {3, n3},
            {4, n4},
            {5, n5},
            {6, n6},
            {7, n7},
            {8, n8},
            {9, n9},
        };
    }

    // Update is called once per frame
    void Update()
    {
        changeSprite(enemyList.GetHighestNumber().ToString());
    }

    private void changeSprite(string number)
    {
        int numberAsInt = int.Parse(number);
        targetImage.sprite = spriteDict[numberAsInt];
    }
}
