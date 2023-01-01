using System;
using System.Collections;
using Levels.Targets;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Levels
{
    public class PrecisionTaskDetectionLevelManager : MonoBehaviour
    {
        #region Singleton

        public static PrecisionTaskDetectionLevelManager Instance;
        private void Awake() => Instance = this;

        #endregion
        
        #region Variables

        //Script Reference
        public PlayerController playerController;
        //Collider and Prefab Reference
        public BoxCollider col;
        public GameObject targetPrefab;
        public Material greenMat;
        public Material redMat;
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

        #endregion
        
        private void Start()
        {
            ChangeState(PrecisionTaskDetection.PreGame);
            maxTargetCount = 31;
        }

        private void ChangeState(PrecisionTaskDetection precisionTaskDetection)
        {
            switch (precisionTaskDetection)
            {
                case PrecisionTaskDetection.PreGame:
                    SetVariables();
                    UnlockAimDisableCursor();
                    StartCoroutine(StartTask());
                    break;
                case PrecisionTaskDetection.Game:
                    StartCoroutine(SpawnTarget());
                    break;
                case PrecisionTaskDetection.PostGame:
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
            ChangeState(PrecisionTaskDetection.Game);
        }

        public void RedirectToSpawnTarget()
        {
            StartCoroutine(SpawnTarget());
        }
        private IEnumerator SpawnTarget()
        {
            yield return new WaitForSeconds(1f);
            SpawnTargets();
        }
        
        private void SpawnTargets()
        {
            var target = Instantiate(targetPrefab);
            target.transform.GetComponent<PrecisionTaskDetectionTargetController>().greenMat = greenMat;
            target.transform.GetComponent<PrecisionTaskDetectionTargetController>().redMat = redMat;
            target.transform.position = GetRandomPosition();
        }

        private void CalculateScore()
        {
            finalScoreCounterWindow.SetActive(true);
            finalScoreText.text = "Score: " + hits;
            
            CalculateAccuracy();

            void CalculateAccuracy()
            {
                // %A = 100 - { (Tv-Ov)  / Tv *100 }
                float value = (maxTargetCount - hits);
                value /= maxTargetCount;
                value *= 100;
                var finalValue = 100 - value;
                accuracyPercentageText.text = $"Accuracy : {(int)finalValue} %";
            }
        }
        
        public Vector3 GetRandomPosition()
        {
            var center = col.center + col.transform.position;

            var size = col.size;
            
            float minX = center.x - size.x / 2f;
            float maxX = center.x + size.x / 2f;
            float minY = center.y - size.y / 2f;
            float maxY = center.y + size.y / 2f;
            float minZ = center.z - size.z / 2f;
            float maxZ = center.z + size.z / 2f;

            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            float randomZ = Random.Range(minZ, maxZ);

            var randomPosition = new Vector3(randomX, randomY, randomZ);

            return randomPosition;
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
            if(_currentTargetCount >= maxTargetCount) ChangeState(PrecisionTaskDetection.PostGame);
        }
        
        public void IncrementMisses()
        {
            if(_taskStarted) misses++;
            missesText.text = "Misses : " + misses;
            _currentTargetCount++;
            if(_currentTargetCount >= maxTargetCount) ChangeState(PrecisionTaskDetection.PostGame);
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    public enum PrecisionTaskDetection
    {
        PreGame,
        Game,
        PostGame
    }
}
