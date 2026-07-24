using UnityEngine;

public static class TransformUtils
{

  public static void FlipScale(Transform target)
  {
    Vector3 localScale = target.localScale;
    localScale.x *= -1f;
    target.localScale = localScale;
  }

}