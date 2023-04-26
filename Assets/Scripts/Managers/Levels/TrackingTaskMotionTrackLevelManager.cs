using System.Collections;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Levels
{
    public class TrackingTaskMotionTrackLevelManager : MonoBehaviour
    {
        #region Singleton

        public static TrackingTaskMotionTrackLevelManager Instance;
        private void Awake() => Instance = this;

        #endregion
        
        #region Variables

        //Script Reference
        public PlayerController playerController;
        
        //Collider and Prefab Reference
        public Transform[] targetSpawnPoints;
        public GameObject targetPrefab;
        
        //Text Reference
        public TMP_Text killsText;
        public TMP_Text missesText;
        public TMP_Text countdownText;
        public TMP_Text finalScoreText;
        public TMP_Text accuracyPercentageText;
        
        //Score Variables
        [HideInInspector] public int kills;
        [HideInInspector] public int misses;
        [HideInInspector] public int maxTargetCount;
        
        //UI Reference
        public GameObject countdownWindow;
        public GameObject killCounterWindow;
        public GameObject finalScoreCounterWindow;

        //Target Variables
        private int _currentTargetCount;
        private bool _taskStarted;

        #endregion
        
        private void Start()
        {
            ChangeState(TrackingTaskMotionTrack.PreGame);
            maxTargetCount = 3;
        }

        private void ChangeState(TrackingTaskMotionTrack flickingTaskGridShot)
        {
            switch (flickingTaskGridShot)
            {
                case TrackingTaskMotionTrack.PreGame:
                    SetVariables();
                    UnlockAimDisableCursor();
                    StartCoroutine(StartTask());
                    break;
                case TrackingTaskMotionTrack.Game:
                    SpawnTarget();
                    break;
                case TrackingTaskMotionTrack.PostGame:
                    playerController.transform.GetComponent<GunController>().gameOver = true;
                    LockAimEnableCursor();
                    CalculateScore();
                    break;
            }
        }

        private void UnlockAimDisableCursor()
        {
            playerController.canAim = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        private void LockAimEnableCursor()
        {
            playerController.canAim = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
        private void SetVariables()
        {
            if(PlayerPrefs.GetFloat("MouseSensitivity") == 0f) playerController.mouseSens = 1;
            else { playerController.mouseSens = PlayerPrefs.GetFloat("MouseSensitivity"); }
        }

        private IEnumerator StartTask()
        {
            killCounterWindow.SetActive(false);
            yield return new WaitUntil((() => Input.GetMouseButtonDown(0)));
            countdownText.text = "3";
            yield return new WaitForSeconds(1);
            countdownText.text = "2";
            yield return new WaitForSeconds(1);
            countdownText.text = "1";
            yield return new WaitForSeconds(1);
            _taskStarted = true;
            countdownWindow.SetActive(false);
            killCounterWindow.SetActive(true);
            ResetKillsAndMisses();
            ChangeState(TrackingTaskMotionTrack.Game);
        }

        private void CalculateScore()
        {
            finalScoreCounterWindow.SetActive(true);
            finalScoreText.text = "Score: " + kills;
            
            /*CalculateAccuracy();

            void CalculateAccuracy()
            {
                // %A = 100 - { (Tv-Ov)  / Tv *100 }
                float value = (maxTargetCount - hits);
                value /= maxTargetCount;
                value *= 100;
                var finalValue = 100 - value;
                accuracyPercentageText.text = $"Accuracy : {(int)finalValue} %";
            }*/
        }
        
        public void SpawnTarget()
        {
            StartCoroutine(Spawn());
            
            IEnumerator Spawn()
            {
                yield return new WaitForSeconds(2f);
                var randomPosition = Random.Range(0, targetSpawnPoints.Length);
                Instantiate(targetPrefab, targetSpawnPoints[randomPosition]);
            }
        }

        private void ResetKillsAndMisses()
        {
            kills = 0;
            misses = 0;
            killsText.text = "Kills : ";
            missesText.text = "Misses : ";
        }
        
        public void IncrementKills()
        {
            if(_taskStarted) kills++;
            killsText.text = "Kills : " + kills;
            _currentTargetCount++;
            if(_currentTargetCount >= maxTargetCount) ChangeState(TrackingTaskMotionTrack.PostGame);
        }
        
        /*public void IncrementMisses()
        {
            if(_taskStarted) misses++;
            missesText.text = "Misses : " + misses;
            _currentTargetCount++;
            if(_currentTargetCount >= maxTargetCount) ChangeState(TrackingTaskMotionTrack.PostGame);
        }*/

        public void BackToMenu()
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    public enum TrackingTaskMotionTrack
    {
        PreGame,
        Game,
        PostGame
    }
}
