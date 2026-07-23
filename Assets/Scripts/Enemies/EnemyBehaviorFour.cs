using UnityEngine;

public class EnemyBehaviorFour : MonoBehaviour
{
    public void OnPlayerDetected()
    {
        GetComponentInChildren<EnemyAttack>().SetDamageActive(true);
    }

    public void OnPlayerLeftDetection()
    {
        GetComponentInChildren<EnemyAttack>().SetDamageActive(false);
    }
}
