using UnityEngine;

public class EnemyNumber : MonoBehaviour
{
    [SerializeField]
    private int number = 1;

    public int GetNumber()
    {
        return number;
    }
}
