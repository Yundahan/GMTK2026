using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerBehaviour : MonoBehaviour
{
    private float SPEED = 5f;

    public void Move(Vector2 input)
    {
        float newX = this.transform.position.x + SPEED * input.x * Time.deltaTime;
        float newY = this.transform.position.y + SPEED * input.y * Time.deltaTime;
        this.transform.position = new Vector3(newX, newY, 0);
    }
}
