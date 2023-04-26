using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Levels.Targets
{
    public class TrackingTaskMotionTrackTargetController : MonoBehaviour
    {
        #region Variables

        public Image health;
        public float healthAmount;
        
        private Color _currentColor;
        private bool _destroyCurrent;
        
        //Movement Variables
        private NavMeshAgent _navMeshAgent;
        private BoxCollider _col;
        private Vector3 _newPosition;
        private Vector3 _currentPosition;
        private Vector3 _distanceToNewPosition;
        private bool _findNewPosition;

        #endregion
        
        private void Start()
        {
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            _currentColor = transform.GetComponent<MeshRenderer>().material.color;
            healthAmount = 1;

            _col = GameObject.Find("MoveArea").transform.GetComponent<BoxCollider>();

            _findNewPosition = true;
        }

        private void Update()
        {
            health.fillAmount = healthAmount;
            
            if (_navMeshAgent.remainingDistance <= 1) _findNewPosition = true;
            if(_findNewPosition) FindNewPos();
            Move();
        }

        private void Move()
        {
            _navMeshAgent.SetDestination(_newPosition);
        }

        private void FindNewPos()
        {
            _findNewPosition = false;
            _currentPosition = transform.position;
            _newPosition = GetRandomPosition();
            _newPosition.y = _currentPosition.y;
        }
        
        private Vector3 GetRandomPosition()
        {
            var center = _col.center + _col.transform.position;

            var size = _col.size;
            
            float minX = center.x - size.x * 4f;
            float maxX = center.x + size.x * 4f;
            float minY = center.y - size.y * 4f;
            float maxY = center.y + size.y * 4f;
            float minZ = center.z - size.z * 4f;
            float maxZ = center.z + size.z * 4f;

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

        public void Tracking()
        {
            healthAmount -= 0.0002f;
            if (healthAmount <= 0.001f)
            {
                TrackingTaskMotionTrackLevelManager.Instance.IncrementKills();
                TrackingTaskMotionTrackLevelManager.Instance.SpawnTarget();
                Destroy(transform.parent.gameObject);
            }
            transform.GetComponent<MeshRenderer>().material.color = Color.green;
        }

        public void NotTracking()
        {
            transform.GetComponent<MeshRenderer>().material.color = _currentColor;
        }
    }
}