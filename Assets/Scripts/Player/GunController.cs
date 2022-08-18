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
        public FlickingTaskGridShotLevelManager flickingTaskGridShotLevelManager;
        public PrecisionTaskMicroShotLevelManager precisionTaskMicroShotLevelManager;
        public PrecisionTaskSpiderShotLevelManager precisionTaskSpiderShotLevelManager;
        public FlickingTaskMicroFlexLevelManager flickingTaskMicroFlexLevelManager;
        public TrackingTaskStrafeBotLevelManager trackingTaskStrafeBotLevelManager;
        public FlickingTaskSpiderShot180LevelManager flickingTaskSpiderShot180LevelManager;
        public FlickingTaskTileFrenzyLevelManager flickingTaskTileFrenzyLevelManager;
        public SwitchingTaskDecisionShotLevelManager switchingTaskDecisionShotLevelManager;
        public FlickingTaskMotionShotLevelManager flickingTaskMotionShotLevelManager;
        public PrecisionTaskMicroShotSpeedLevelManager precisionTaskMicroShotSpeedLevelManager;

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

            Ray ray2 = cam.ViewportPointToRay(new Vector3(.5f, .5f));
            if (Physics.Raycast(ray2, out RaycastHit hit2))
            {
                if(hit2.collider.CompareTag("Target"))
                {
                    if (SceneManager.GetActiveScene().name == "Scene_TrackingTaskStrafeBot")
                    {
                        _trackingTaskStrafeBotTargetController = hit2.collider.GetComponent<TrackingTaskStrafeBotTargetController>();
                        _trackingTaskStrafeBotTargetController.Tracking();
                    }
                }
                else { if(_trackingTaskStrafeBotTargetController != null) _trackingTaskStrafeBotTargetController.NotTracking(); }
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
            Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Target"))
                {
                    if (SceneManager.GetActiveScene().name == "Scene_FlickingTaskGridShot")
                    {
                        hit.collider.transform.position = flickingTaskGridShotLevelManager.GetRandomPosition();
                        flickingTaskGridShotLevelManager.IncrementHits();
                    }
                    if (SceneManager.GetActiveScene().name == "Scene_PrecisionTaskMicroShot")
                    {
                        hit.collider.transform.position = precisionTaskMicroShotLevelManager.GetRandomPosition();
                        precisionTaskMicroShotLevelManager.IncrementHits();
                    }
                    if (SceneManager.GetActiveScene().name == "Scene_PrecisionTaskSpiderShot")
                    {
                        hit.collider.transform.position = precisionTaskSpiderShotLevelManager.GetRandomPosition();
                        precisionTaskSpiderShotLevelManager.IncrementHits();
                    }
                    if (SceneManager.GetActiveScene().name == "Scene_FlickingTaskMicroFlex")
                    {
                        Destroy(hit.transform.gameObject);
                        flickingTaskMicroFlexLevelManager.IncrementHits();
                    }
                    if (SceneManager.GetActiveScene().name == "Scene_FlickingTaskSpiderShot180")
                    {
                        flickingTaskSpiderShot180LevelManager.DecideSpawnBounds();
                        hit.collider.transform.position = flickingTaskSpiderShot180LevelManager.spawnPosition;
                        flickingTaskSpiderShot180LevelManager.IncrementHits();
                    }
                    if (SceneManager.GetActiveScene().name == "Scene_FlickingTaskTileFrenzy")
                    {
                        hit.collider.transform.position = flickingTaskTileFrenzyLevelManager.GetRandomPosition();
                        flickingTaskTileFrenzyLevelManager.IncrementHits();
                    }
                    if (SceneManager.GetActiveScene().name == "Scene_SwitchingTaskDecisionShot")
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
                    if (SceneManager.GetActiveScene().name == "Scene_FlickingTaskMotionShot")
                    {
                        hit.collider.transform.GetComponent<FlickingTaskMotionShotTargetController>().InstantChangePosition();
                        flickingTaskMotionShotLevelManager.IncrementHits();
                    }
                    if (SceneManager.GetActiveScene().name == "Scene_PrecisionTaskMicroShotSpeed")
                    {
                        hit.collider.transform.position = precisionTaskMicroShotSpeedLevelManager.GetRandomPosition();
                        precisionTaskMicroShotSpeedLevelManager.IncrementHits();
                    }
                    //Call scene respective functions 
                }
                else if (hit.collider.CompareTag("Wall"))
                {
                    if (SceneManager.GetActiveScene().name == "Scene_FlickingTaskGridShot")
                    {
                        flickingTaskGridShotLevelManager.IncrementMisses();
                    }
                    if (SceneManager.GetActiveScene().name == "Scene_PrecisionTaskMicroShot")
                    {
                        precisionTaskMicroShotLevelManager.IncrementMisses();
                    }
                    if (SceneManager.GetActiveScene().name == "Scene_PrecisionTaskSpiderShot")
                    {
                        precisionTaskSpiderShotLevelManager.IncrementMisses();
                    }
                    if (SceneManager.GetActiveScene().name == "Scene_FlickingTaskMicroFlex")
                    {
                        flickingTaskMicroFlexLevelManager.IncrementMisses();
                    }
                    if (SceneManager.GetActiveScene().name == "Scene_FlickingTaskSpiderShot180")
                    {
                        flickingTaskSpiderShot180LevelManager.IncrementMisses();
                    }
                    if (SceneManager.GetActiveScene().name == "Scene_FlickingTaskTileFrenzy")
                    {
                        flickingTaskTileFrenzyLevelManager.IncrementMisses();
                    }
                    if (SceneManager.GetActiveScene().name == "Scene_SwitchingTaskDecisionShot")
                    {
                        switchingTaskDecisionShotLevelManager.IncrementMisses();
                    }
                    if (SceneManager.GetActiveScene().name == "Scene_FlickingTaskMotionShot")
                    {
                        flickingTaskMotionShotLevelManager.IncrementMisses();
                    }
                    if (SceneManager.GetActiveScene().name == "Scene_PrecisionTaskMicroShotSpeed")
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