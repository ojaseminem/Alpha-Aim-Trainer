using System.Collections;
using Levels;
using Levels.Targets;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class GunController : MonoBehaviour
    {
        #region Variables
        
        //SCRIPT REFERENCES
        
        [SerializeField] private FlickingTaskGridShotLevelManager flickingTaskGridShotLevelManager;
        private bool _flickingTaskGridShotLevelScene;
        [SerializeField] private PrecisionTaskMicroShotLevelManager precisionTaskMicroShotLevelManager;
        private bool _precisionTaskMicroShotLevelScene;
        [SerializeField] private PrecisionTaskSpiderShotLevelManager precisionTaskSpiderShotLevelManager;
        private bool _precisionTaskSpiderShotLevelScene;
        [SerializeField] private FlickingTaskMicroFlexLevelManager flickingTaskMicroFlexLevelManager;
        private bool _flickingTaskMicroFlexLevelScene;
        [SerializeField] private TrackingTaskStrafeBotLevelManager trackingTaskStrafeBotLevelManager;
        private bool _trackingTaskStrafeBotLevelScene;
        [SerializeField] private FlickingTaskSpiderShot180LevelManager flickingTaskSpiderShot180LevelManager;
        private bool _flickingTaskSpiderShot180LevelScene;
        [SerializeField] private FlickingTaskTileFrenzyLevelManager flickingTaskTileFrenzyLevelManager;
        private bool _flickingTaskTileFrenzyLevelScene;
        [SerializeField] private SwitchingTaskDecisionShotLevelManager switchingTaskDecisionShotLevelManager;
        private bool _switchingTaskDecisionShotLevelScene;
        [SerializeField] private FlickingTaskMotionShotLevelManager flickingTaskMotionShotLevelManager;
        private bool _flickingTaskMotionShotLevelScene;
        [SerializeField] private PrecisionTaskMicroShotSpeedLevelManager precisionTaskMicroShotSpeedLevelManager;
        private bool _precisionTaskMicroShotSpeedLevelScene;
        //Other References and variables
        public bool gameOver;
        private TrackingTaskMotionTrackTargetController _trackingTaskMotionTrackTargetController;
        private TrackingTaskStrafeBotTargetController _trackingTaskStrafeBotTargetController;
        public Camera cam;
        [HideInInspector] public float fireRate = 0.1f;
        
        //Pause and Unpause Variables
        [SerializeField] private GameObject pauseMenu;
        private PlayerController _playerController;
        private bool _paused;

        //Private variables
        private bool _canShoot;

        #endregion

        private void Start()
        {
            _playerController = GetComponent<PlayerController>();
            _canShoot = true;
            gameOver = false;
            CheckCurrentScene();
        }

        private void CheckCurrentScene()
        {
            if (flickingTaskGridShotLevelManager != null) _flickingTaskGridShotLevelScene = true;
            if (precisionTaskMicroShotLevelManager != null) _precisionTaskMicroShotLevelScene = true;
            if (precisionTaskSpiderShotLevelManager != null) _precisionTaskSpiderShotLevelScene = true;
            if (flickingTaskMicroFlexLevelManager != null) _flickingTaskMicroFlexLevelScene = true;
            if (trackingTaskStrafeBotLevelManager != null) _trackingTaskStrafeBotLevelScene = true;
            if (flickingTaskSpiderShot180LevelManager != null) _flickingTaskSpiderShot180LevelScene = true;
            if (flickingTaskTileFrenzyLevelManager != null) _flickingTaskTileFrenzyLevelScene = true;
            if (switchingTaskDecisionShotLevelManager != null) _switchingTaskDecisionShotLevelScene = true;
            if (flickingTaskMotionShotLevelManager != null) _flickingTaskMotionShotLevelScene = true;
            if (precisionTaskMicroShotSpeedLevelManager != null) _precisionTaskMicroShotSpeedLevelScene = true;
        }
        
        private void Update()
        {
            #region Shoot

            if (Input.GetMouseButtonDown(0) && _canShoot)
            {
                _canShoot = false;
                StartCoroutine(Shoot());
            }

            #endregion

            #region PauseAndUnpause

            if(!gameOver)
            {
                if (Input.GetKeyDown(KeyCode.Escape)) _paused = !_paused;
                if (_paused) Pause();
                else { Resume(); }
            }
            #endregion
            
            #region TrackingTaskMotionTrackLevel

            Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if(hit.collider.CompareTag("Target"))
                {
                    if (SceneManager.GetActiveScene().name == "Scene_TrackingTaskMotionTrack")
                    {
                        _trackingTaskMotionTrackTargetController = hit.collider.GetComponent<TrackingTaskMotionTrackTargetController>();
                        _trackingTaskMotionTrackTargetController.Tracking();
                    }
                }
                else { if(_trackingTaskMotionTrackTargetController != null) _trackingTaskMotionTrackTargetController.NotTracking(); }
            }

            #endregion
            
            #region TrackingTaskStrafeBotLevel

            if (SceneManager.GetActiveScene().name == "Scene_TrackingTaskStrafeBot")
            {
                var ray2 = cam.ViewportPointToRay(new Vector3(.5f, .5f));
                if (Physics.Raycast(ray2, out RaycastHit hit2))
                {
                    if (hit2.collider.CompareTag("Target"))
                    {
                        _trackingTaskStrafeBotTargetController = hit2.collider.GetComponent<TrackingTaskStrafeBotTargetController>();
                        _trackingTaskStrafeBotTargetController.Tracking();
                    }
                    else { if (_trackingTaskStrafeBotTargetController != null) _trackingTaskStrafeBotTargetController.NotTracking(); }
                }
            }
            #endregion
        }

        private IEnumerator Shoot()
        {
            DetermineHit();
            
            yield return new WaitForSeconds(fireRate);
            _canShoot = true;
        }

        private void DetermineHit()
        {
            var ray = cam.ViewportPointToRay(new Vector3(.5f, .5f));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Target"))
                {
                    if (_flickingTaskGridShotLevelScene)
                    {
                        hit.collider.transform.position = flickingTaskGridShotLevelManager.GetRandomPosition();
                        flickingTaskGridShotLevelManager.IncrementHits();
                    }
                    if (_precisionTaskMicroShotLevelScene)
                    {
                        hit.collider.transform.position = precisionTaskMicroShotLevelManager.GetRandomPosition();
                        precisionTaskMicroShotLevelManager.IncrementHits();
                    }
                    if (_precisionTaskSpiderShotLevelScene)
                    {
                        hit.collider.transform.position = precisionTaskSpiderShotLevelManager.GetRandomPosition();
                        precisionTaskSpiderShotLevelManager.IncrementHits();
                    }
                    if (_flickingTaskMicroFlexLevelScene)
                    {
                        Destroy(hit.transform.gameObject);
                        flickingTaskMicroFlexLevelManager.IncrementHits();
                    }
                    if (_flickingTaskSpiderShot180LevelScene)
                    {
                        flickingTaskSpiderShot180LevelManager.DecideSpawnBounds();
                        hit.collider.transform.position = flickingTaskSpiderShot180LevelManager.spawnPosition;
                        flickingTaskSpiderShot180LevelManager.IncrementHits();
                    }
                    if (_flickingTaskTileFrenzyLevelScene)
                    {
                        hit.collider.transform.position = flickingTaskTileFrenzyLevelManager.GetRandomPosition();
                        flickingTaskTileFrenzyLevelManager.IncrementHits();
                    }
                    if (_switchingTaskDecisionShotLevelScene)
                    {
                        var targetScript = hit.collider.transform.GetComponent<SwitchingTaskDecisionShotTargetController>();
                        switch (targetScript.id)
                        {
                            case 0:
                                switchingTaskDecisionShotLevelManager.ChangeTarget1Position();
                                switchingTaskDecisionShotLevelManager.ChangeTarget2Position();
                                switchingTaskDecisionShotLevelManager.IncrementMisses();
                                break;
                            case 1:
                                switchingTaskDecisionShotLevelManager.ChangeTarget1Position();
                                switchingTaskDecisionShotLevelManager.ChangeTarget2Position();
                                switchingTaskDecisionShotLevelManager.IncrementHits();
                                break;
                        }
                    }
                    if (_flickingTaskMotionShotLevelScene)
                    {
                        hit.collider.transform.GetComponent<FlickingTaskMotionShotTargetController>().InstantChangePosition();
                        flickingTaskMotionShotLevelManager.IncrementHits();
                    }
                    if (_precisionTaskMicroShotSpeedLevelScene)
                    {
                        hit.collider.transform.position = precisionTaskMicroShotSpeedLevelManager.GetRandomPosition();
                        precisionTaskMicroShotSpeedLevelManager.IncrementHits();
                    }
                }
                else if (hit.collider.CompareTag("Wall"))
                {
                    if (_flickingTaskGridShotLevelScene)
                    {
                        flickingTaskGridShotLevelManager.IncrementMisses();
                    }
                    if (_precisionTaskMicroShotLevelScene)
                    {
                        precisionTaskMicroShotLevelManager.IncrementMisses();
                    }
                    if (_precisionTaskSpiderShotLevelScene)
                    {
                        precisionTaskSpiderShotLevelManager.IncrementMisses();
                    }
                    if (_flickingTaskMicroFlexLevelScene)
                    {
                        flickingTaskMicroFlexLevelManager.IncrementMisses();
                    }
                    if (_flickingTaskSpiderShot180LevelScene)
                    {
                        flickingTaskSpiderShot180LevelManager.IncrementMisses();
                    }
                    if (_flickingTaskTileFrenzyLevelScene)
                    {
                        flickingTaskTileFrenzyLevelManager.IncrementMisses();
                    }
                    if (_switchingTaskDecisionShotLevelScene)
                    {
                        switchingTaskDecisionShotLevelManager.IncrementMisses();
                    }
                    if (_flickingTaskMotionShotLevelScene)
                    {
                        flickingTaskMotionShotLevelManager.IncrementMisses();
                    }
                    if (_precisionTaskMicroShotSpeedLevelScene)
                    {
                        precisionTaskMicroShotSpeedLevelManager.IncrementMisses();
                    }
                    //Call scene respective functions
                }
            }
        }

        #region PauseAndUnpause

        private void Pause()
        {
            pauseMenu.SetActive(true);
            _canShoot = false;

            LockAimEnableCursor();
            
            void LockAimEnableCursor()
            {
                _playerController.canAim = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        private void Resume()
        {
            pauseMenu.SetActive(false);
            _canShoot = true;
            
            UnlockAimDisableCursor();
            
            void UnlockAimDisableCursor()
            {
                _playerController.canAim = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        public void Unpause()
        {
            _paused = false;
        }

        #endregion
    }
}