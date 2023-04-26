using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Levels.Targets
{
    public class FlickingTaskMotionShotTargetController : MonoBehaviour
    {
        [HideInInspector] public BoxCollider col;
        public float moveSpeed;
        private Vector3 _newPosition;
        private float _remainingDistance;
        
        private void Start()
        {
            FindNewPosition();
        }
        
        private void Update()
        {
            ReachedDestination();
            if (_remainingDistance <= 1) FindNewPosition();
            Move();
        }

        private void FindNewPosition()
        {
            _newPosition = GetRandomPosition();
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, _newPosition, moveSpeed);
        }

        private void ReachedDestination()
        {
            _remainingDistance = Vector3.Distance(transform.position, _newPosition);
        }
        
        public void InstantChangePosition()
        {
            transform.position = GetRandomPosition();
            FindNewPosition();
        }
        
        private Vector3 GetRandomPosition()
        {
            var center = col.center + col.transform.position;

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

            var randomPosition = new Vector3(randomX, randomY, randomZ);

            return randomPosition;
        }
        
        private void OnDrawGizmos()
        {
            Debug.DrawLine(transform.position, _newPosition);
        }
    }
}
