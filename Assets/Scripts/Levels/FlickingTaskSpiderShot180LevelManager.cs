using System.Collections;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Levels
{
    public class FlickingTaskSpiderShot180LevelManager : MonoBehaviour
    {
        #region Singleton

        public static FlickingTaskSpiderShot180LevelManager Instance;
        private void Awake() => Instance = this;

        #endregion
        
        #region Variables

        //Script Reference
        public PlayerController playerController;
        
        //Collider and Prefab Reference
        public BoxCollider spawnBounds1;
        public BoxCollider spawnBounds2;
        [HideInInspector] public Vector3 spawnPosition;
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
            ChangeState(FlickingTaskSpiderShot180.PreGame);
            maxTargetCount = 31;
            _taskStarted = false;
        }

        private void ChangeState(FlickingTaskSpiderShot180 flickingTaskGridShot)
        {
            switch (flickingTaskGridShot)
            {
                case FlickingTaskSpiderShot180.PreGame:
                    SetVariables();
                    UnlockAimDisableCursor();
                    StartCoroutine(StartTask());
                    break;
                case FlickingTaskSpiderShot180.Game:
                    SpawnTargets();
                    break;
                case FlickingTaskSpiderShot180.PostGame:
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
            ChangeState(FlickingTaskSpiderShot180.Game);
        }

        private void SpawnTargets()
        {
            DecideSpawnBounds();
            
            for (int i = 0; i < StartingTargetCount; i++)
            {
                var target = Instantiate(targetPrefab);
                target.transform.position = spawnPosition;
                DecideSpawnBounds();
            }
        }

        public void DecideSpawnBounds()
        {
            var randomSpawnBounds = Random.Range(1, 10);
            if (randomSpawnBounds >= 6) spawnPosition = GetRandomPositionSpawnBounds1();
            if (randomSpawnBounds <= 5) spawnPosition = GetRandomPositionSpawnBounds2();
        }
        
        private Vector3 GetRandomPositionSpawnBounds1()
        {
            var center = spawnBounds1.center + spawnBounds1.transform.position;

            var size = spawnBounds1.size;
            
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
        
        private Vector3 GetRandomPositionSpawnBounds2()
        {
            var center = spawnBounds2.center + spawnBounds2.transform.position;

            var size = spawnBounds2.size;
            
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
            if(_currentTargetCount >= maxTargetCount) ChangeState(FlickingTaskSpiderShot180.PostGame);
        }
        
        public void IncrementMisses()
        {
            if(_taskStarted) misses++;
            missesText.text = "Misses : " + misses;
            _currentTargetCount++;
            if(_currentTargetCount >= maxTargetCount) ChangeState(FlickingTaskSpiderShot180.PostGame);
        } 
        
        private void CalculateScore()
        {
            finalScoreCounterWindow.SetActive(true);
            finalScoreText.text = "Score: " + hits;
            
            /*CalculateAccuracy();
            
            void CalculateAccuracy()
            {
                var accuracy = (hits / maxTargetCount) * 100;
                accuracyPercentageText.text = $"Accuracy: {accuracy} %";
            }*/
        }

    }

    public enum FlickingTaskSpiderShot180
    {
        PreGame,
        Game,
        PostGame
    }
}
