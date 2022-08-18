using System.Collections;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Levels
{
    public class PrecisionTaskSpiderShotLevelManager : MonoBehaviour
    {
        #region Singleton

        public static PrecisionTaskSpiderShotLevelManager Instance;
        private void Awake() => Instance = this;

        #endregion
        
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
            ChangeState(PrecisionTaskSpiderShot.PreGame);
            maxTargetCount = 30;
        }

        private void ChangeState(PrecisionTaskSpiderShot flickingTaskGridShot)
        {
            switch (flickingTaskGridShot)
            {
                case PrecisionTaskSpiderShot.PreGame:
                    SetVariables();
                    UnlockAimDisableCursor();
                    StartCoroutine(StartTask());
                    break;
                case PrecisionTaskSpiderShot.Game:
                    SpawnTargets();
                    break;
                case PrecisionTaskSpiderShot.PostGame:
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
            ChangeState(PrecisionTaskSpiderShot.Game);
        }

        private void SpawnTargets()
        {
            for (int i = 0; i < StartingTargetCount; i++)
            {
                var target = Instantiate(targetPrefab);
                target.transform.position = GetRandomPosition();
            }
            ResetHitsAndMisses();
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
            if(_currentTargetCount >= maxTargetCount) ChangeState(PrecisionTaskSpiderShot.PostGame);
        }
        
        public void IncrementMisses()
        {
            if(_taskStarted) misses++;
            missesText.text = "Misses : " + misses;
            _currentTargetCount++;
            if(_currentTargetCount >= maxTargetCount) ChangeState(PrecisionTaskSpiderShot.PostGame);
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    public enum PrecisionTaskSpiderShot
    {
        PreGame,
        Game,
        PostGame
    }
}
