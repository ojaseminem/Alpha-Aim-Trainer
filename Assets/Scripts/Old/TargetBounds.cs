using UnityEngine;

public class TargetBounds : MonoBehaviour
{
    #region Singleton

    public static TargetBounds Instance;
    private void Awake() => Instance = this; 

    #endregion

    [SerializeField] private BoxCollider col;

    public Vector3 GetRandomPosition()
    {
        Vector3 center = col.center + transform.position;

        var size = col.size;
        float minX = center.x - size.x / 2f;
        float maxX = center.x + size.x / 2f;
        float minY = center.y - size.y / 2f;
        float maxY = center.y + size.y / 2f;
        float minZ = center.z - size.z / 2f;
        float maxZ = center.z + size.z / 2f;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        float randomZ = Random.Range(minZ, maxZ);

        Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);

        return randomPosition;
    }
}