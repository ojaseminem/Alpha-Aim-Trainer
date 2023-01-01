using UnityEngine;

namespace Menu
{
    public class MainMenuCameraController : MonoBehaviour
    {
        public Transform cameraHolder;
        private float _verticalLookRotation;

        private void Update()
        {
            transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * .1f);
            _verticalLookRotation -= Input.GetAxisRaw("Mouse Y") * .1f;
            _verticalLookRotation = Mathf.Clamp(_verticalLookRotation, -80f, 80f);
            cameraHolder.localEulerAngles = new Vector3(_verticalLookRotation, 0, 0);
        }
    }
}