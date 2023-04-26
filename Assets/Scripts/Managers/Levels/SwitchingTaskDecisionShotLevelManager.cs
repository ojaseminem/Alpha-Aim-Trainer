using System;
using System.Collections;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Levels
{
    public class SwitchingTaskDecisionShotLevelManager : MonoBehaviour
    {
        #region Variables

        //Script Reference
        public PlayerController playerController;
        //Collider and Prefab Reference
        public BoxCollider col;
        public GameObject targetPrefab1;
        public GameObject targetPrefab2;
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
        private GameObject _target1;
        private GameObject _target2;
        private int _currentTargetCount;
        private bool _taskStarted;

        #endregion
        
        private void Start()
        {
            ChangeState(SwitchingTaskDecisionShot.PreGame);
            maxTargetCount = 31;
        }

        private void ChangeState(SwitchingTaskDecisionShot switchingTaskDecisionShot)
        {
            switch (switchingTaskDecisionShot)
            {
                case SwitchingTaskDecisionShot.PreGame:
                    SetVariables();
                    UnlockAimDisableCursor();
                    StartCoroutine(StartTask());
                    break;
                case SwitchingTaskDecisionShot.Game:
                    SpawnTargets();
                    break;
                case SwitchingTaskDecisionShot.PostGame:
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
            ChangeState(SwitchingTaskDecisionShot.Game);
        }

        private void SpawnTargets()
        {
            _target1 = Instantiate(targetPrefab1);
            ChangeTarget1Position();
            _target2 = Instantiate(targetPrefab2);
            ChangeTarget2Position();
        }

        public void ChangeTarget1Position()
        {
            _target1.transform.position = GetRandomPosition();
        }

        public void ChangeTarget2Position()
        {
            _target2.transform.position = GetRandomPosition();
        }
        
        private Vector3 GetRandomPosition()
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
            if(_currentTargetCount >= maxTargetCount) ChangeState(SwitchingTaskDecisionShot.PostGame);
        }
        
        public void IncrementMisses()
        {
            if(_taskStarted) misses++;
            missesText.text = "Misses : " + misses;
            _currentTargetCount++;
            if(_currentTargetCount >= maxTargetCount) ChangeState(SwitchingTaskDecisionShot.PostGame);
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
    }

    public enum SwitchingTaskDecisionShot
    {
        PreGame,
        Game,
        PostGame
    }
}
