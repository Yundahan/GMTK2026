using UnityEngine;

public class EnemyBehaviorFour : MonoBehaviour
{
    public void OnPlayerDetected()
    {
        GetComponentInChildren<EnemyAttack>().SetDamageActive(true);
        //hier stuff machen für attack animation
    }

    public void OnPlayerLeftDetection()
    {
        GetComponentInChildren<EnemyAttack>().SetDamageActive(false);
        //hier stuff machen für idle animation
    }
}
