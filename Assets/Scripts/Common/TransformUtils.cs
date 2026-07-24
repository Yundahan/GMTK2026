using UnityEngine;

public static class TransformUtils
{

  public static void FlipScale(Transform target)
  {
    Vector3 localScale = target.localScale;
    localScale.x *= -1f;
    target.localScale = localScale;
  }

  public static void SetTargetDirection(Transform target, float scale)
  {
    Vector3 localScale = target.localScale;
    if (scale > 0)
    {
      localScale.x *= 1f;
    }
    else if (scale < 0)
    {
      localScale.x *= -1;
    }
    target.localScale = localScale;
  }

}