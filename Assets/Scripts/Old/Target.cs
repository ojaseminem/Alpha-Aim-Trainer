using UnityEngine;

public class Target : MonoBehaviour
{
    public void Hit()
    {
        transform.position = TargetBounds.Instance.GetRandomPosition();
    }
}