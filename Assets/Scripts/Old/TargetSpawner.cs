using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    #region Singleton

    public static TargetSpawner Instance;
    private void Awake() => Instance = this;

    #endregion

    public Transform targetsHolder;
    public GameObject targetPrefab;
    
    public void SpawnTargets(string difficultyLevel)
    {
        const int targetCount = 3;

        for (int i = 0; i < targetCount; i++)
        {
            GameObject target = Instantiate(targetPrefab, targetsHolder.parent, true);
            target.GetComponent<Target>().Hit();
        }
    }
}