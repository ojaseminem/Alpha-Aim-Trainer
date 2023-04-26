using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Singleton

        public static PlayerController Instance;
        private void Awake() => Instance = this;

        #endregion
        
        [SerializeField] private Transform cameraHolder;

        private float _verticalLookRotation;
        public float mouseSens;
        public bool canAim;

        private void Start()
        {
            if (PlayerPrefs.HasKey("MouseSensitivity")) mouseSens = PlayerPrefs.GetFloat("MouseSensitivity");
        }

        private void Update()
        {
            if(canAim) Look();
        }

        private void Look()
        {
            transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSens);
            _verticalLookRotation -= Input.GetAxisRaw("Mouse Y") * mouseSens;
            _verticalLookRotation = Mathf.Clamp(_verticalLookRotation, -80f, 80f);
            cameraHolder.localEulerAngles = new Vector3(_verticalLookRotation, 0, 0);
        }
    }
}