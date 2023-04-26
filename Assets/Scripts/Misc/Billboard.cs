using UnityEngine;

namespace OJAS.Tools.Billboard
{
    public class Billboard : MonoBehaviour
    {
        private Camera _cam;
        void Update()
        {
            if (_cam == null) _cam = FindObjectOfType<Camera>();
            if (_cam == null) return;
            
            transform.LookAt(_cam.transform);
            transform.Rotate(Vector3.up * 180);
        }
    }
}
