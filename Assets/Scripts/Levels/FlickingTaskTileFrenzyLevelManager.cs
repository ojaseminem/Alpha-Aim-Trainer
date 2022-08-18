using System.Collections;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Levels
{
    public class FlickingTaskTileFrenzyLevelManager : MonoBehaviour
    {
        #region Variables

        //Script Reference
        public PlayerController playerController;
        //Collider and Prefab Reference
        public Transform[] spawnPositions;
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
        private const int StartingTargetCount = 3;

        #endregion
        
        private void Start()
        {
            ChangeState(FlickingTaskTileFrenzy.PreGame);
            maxTargetCount = 51;
        }

        private void ChangeState(FlickingTaskTileFrenzy flickingTaskTileFrenzy)
        {
            switch (flickingTaskTileFrenzy)
            {
                case FlickingTaskTileFrenzy.PreGame:
                    SetVariables();
                    UnlockAimDisableCursor();
                    StartCoroutine(StartTask());
                    break;
                case FlickingTaskTileFrenzy.Game:
                    ResetHitsAndMisses();
                    SpawnTargets();
                    break;
                case FlickingTaskTileFrenzy.PostGame:
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
            StopAllCoroutines();
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
            ChangeState(FlickingTaskTileFrenzy.Game);
        }

        private void SpawnTargets()
        {
            for (int i = 0; i < StartingTargetCount; i++)
            {
                var target = Instantiate(targetPrefab);
                target.transform.position = GetRandomPosition();
            }
        }
        
        public Vector3 GetRandomPosition()
        {
            var randomSpawnPosition = Random.Range(0, spawnPositions.Length);

            return spawnPositions[randomSpawnPosition].position;
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
            if(_currentTargetCount >= maxTargetCount) ChangeState(FlickingTaskTileFrenzy.PostGame);
        }
        
        public void IncrementMisses()
        {
            if(_taskStarted) misses++;
            missesText.text = "Misses : " + misses;
            _currentTargetCount++;
            if(_currentTargetCount >= maxTargetCount) ChangeState(FlickingTaskTileFrenzy.PostGame);
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

    public enum FlickingTaskTileFrenzy
    {
        PreGame,
        Game,
        PostGame
    }
}
