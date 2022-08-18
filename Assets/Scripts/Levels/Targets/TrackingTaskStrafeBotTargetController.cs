using UnityEngine;
using UnityEngine.UI;

namespace Levels.Targets
{
    public class TrackingTaskStrafeBotTargetController : MonoBehaviour
    {
        #region Variables

        public Image health;
        public float healthAmount;
        
        private Color _currentColor;
        private bool _destroyCurrent;

        #endregion
        
        private void Start()
        {
            _currentColor = transform.GetComponent<MeshRenderer>().material.color;
            healthAmount = 1;
        }

        private void Update()
        {
            health.fillAmount = healthAmount;
        }
        
        public void Tracking()
        {
            healthAmount -= 0.0005f;
            if (healthAmount <= 0.001f)
            {
                TrackingTaskStrafeBotLevelManager.Instance.IncrementHits();
                TrackingTaskStrafeBotLevelManager.Instance.SpawnTarget();
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
