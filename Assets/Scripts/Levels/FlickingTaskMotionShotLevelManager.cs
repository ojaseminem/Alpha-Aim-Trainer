using System.Collections;
using Levels.Targets;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class FlickingTaskMotionShotLevelManager : MonoBehaviour
    {
        #region Variables

        //Script Reference
        public PlayerController playerController;
        //Collider and Prefab Reference
        public BoxCollider col;
        public GameObject targetPrefab;
        //Text Reference
        public TMP_Text hitsText;
        public TMP_Text missesText;
        public TMP_Text countdownText;
        public TMP_Text finalScoreText;
        public TMP_Text accuracyPercentageText;
        //Score Variables
        [HideInInspector] public int hits;
        [HideInInspector] public int misses;
        [HideInInspector] public int maxTargetCount;
        //UI Reference
        public GameObject countdownWindow;
        public GameObject scoreCounterWindow;
        public GameObject finalScoreCounterWindow;

        //Target Variables
        private int _currentTargetCount;
        private bool _taskStarted;
        private const int StartingTargetCount = 2;

        #endregion
        
        private void Start()
        {
            ChangeState(FlickingTaskMotionShot.PreGame);
            maxTargetCount = 31;
        }

        private void ChangeState(FlickingTaskMotionShot flickingTaskMotionShot)
        {
            switch (flickingTaskMotionShot)
            {
                case FlickingTaskMotionShot.PreGame:
                    SetVariables();
                    UnlockAimDisableCursor();
                    StartCoroutine(StartTask());
                    break;
                case FlickingTaskMotionShot.Game:
                    SpawnTargets();
                    break;
                case FlickingTaskMotionShot.PostGame:
                    playerController.transform.GetComponent<GunController>().gameOver = true;
                    LockAimEnableCursor();
                    CalculateScore();
                    break;
            }
        }

        #region Constant Functions

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

        public void BackToMenu()
        {
            SceneManager.LoadScene("MainMenuScene");
        }
        

        #endregion
        
        private IEnumerator StartTask()
        {
            scoreCounterWindow.SetActive(false);
            yield return new WaitUntil((() => Input.GetMouseButtonDown(0)));
            countdownText.text = "3";
            yield return new WaitForSeconds(1);
            countdownText.text = "2";
            yield return new WaitForSeconds(1);
            countdownText.text = "1";
            yield return new WaitForSeconds(1);
            _taskStarted = true;
            countdownWindow.SetActive(false);
            scoreCounterWindow.SetActive(true);
            ResetHitsAndMisses();
            ChangeState(FlickingTaskMotionShot.Game);
        }

        private void SpawnTargets()
        {
            for (int i = 0; i < StartingTargetCount; i++)
            {
                var target = Instantiate(targetPrefab);
                target.GetComponent<FlickingTaskMotionShotTargetController>().col = col;
                target.GetComponent<FlickingTaskMotionShotTargetController>().InstantChangePosition();
            }
        }

        private void ResetHitsAndMisses()
        {
            hits = 0;
            misses = 0;
            hitsText.text = "";
            missesText.text = "";
        }
        
        public void IncrementHits()
        {
            if(_taskStarted) hits++;
            hitsText.text = "Hits : " + hits;
            _currentTargetCount++;
            if(_currentTargetCount >= maxTargetCount) ChangeState(FlickingTaskMotionShot.PostGame);
        }
        
        public void IncrementMisses()
        {
            if(_taskStarted) misses++;
            missesText.text = "Misses : " + misses;
            _currentTargetCount++;
            if(_currentTargetCount >= maxTargetCount) ChangeState(FlickingTaskMotionShot.PostGame);
        }
        
        private void CalculateScore()
        {
            finalScoreCounterWindow.SetActive(true);
            finalScoreText.text = "Score: " + hits;
            
            /*CalculateAccuracy();
            
            void CalculateAccuracy()
            {
                float accuracy = (hits / maxTargetCount) * 100;
                accuracyPercentageText.text = $"Accuracy: {accuracy} %";
            }   */
        }
    }

    public enum FlickingTaskMotionShot
    {
        PreGame,
        Game,
        PostGame
    }
}
