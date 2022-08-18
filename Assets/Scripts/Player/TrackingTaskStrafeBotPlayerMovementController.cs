using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class TrackingTaskStrafeBotPlayerMovementController : MonoBehaviour
    {
        #region Variables

        //Movement Variables
        [SerializeField] private BoxCollider moveArea;
        [SerializeField] private float moveSpeed;
        private Vector3 _newPosition;
        private bool _findNewPosition;
        private float _remainingDistance;

        #endregion
        
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

        private Vector3 GetRandomPosition()
        {
            var center = moveArea.center + moveArea.transform.position;

            var size = moveArea.size;
            
            float minX = center.x - size.x / 2f;
            float maxX = center.x + size.x / 2f;
            float minZ = center.z - size.z / 2f;
            float maxZ = center.z + size.z / 2f;

            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);

            var randomPosition = new Vector3(randomX, 0, randomZ);

            return randomPosition;
        }
        
        private void OnDrawGizmos()
        {
            Debug.DrawLine(transform.position, _newPosition);
        }
    }
}
