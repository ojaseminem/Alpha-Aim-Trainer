using System.Collections;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Old
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton

        public static GameManager Instance;
        private void Awake() => Instance = this;

        #endregion

        //Pre Game Scene Variables
        public string gameSceneName;
        
        //Game Scene Variables
        public PlayerController playerController;
        public TMP_Text countdownText;
        public GameObject beginningScreen;
        public string postGameSceneName;
        
        //Post Game Scene Variables
        public string mainMenuSceneName;

        [HideInInspector] public float mouseSensitivity;
        [HideInInspector] public string difficultyLevel;

        public void ChangeState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.PreGame:
                    SetVariables();
                    StartCoroutine(WaitingPhase());
                    break;
                case GameState.Game:
                    SpawnTargets();
                    break;
                case GameState.PostGame:
                    EnableCursor();
                    SwitchToPostGameScene();
                    break;
                case GameState.ReturnToMenu:
                    break;
            }
        }

        private void SetVariables()
        {
        }

        private void SpawnTargets()
        {
            TargetSpawner.Instance.SpawnTargets(difficultyLevel);
        }

        private IEnumerator WaitingPhase()
        {
            OldScoreManager.Instance.DisableUI();
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            countdownText.text = 3.ToString();
            yield return new WaitForSeconds(1f);
            countdownText.text = 2.ToString();
            yield return new WaitForSeconds(1f);
            countdownText.text = 1.ToString();
            yield return new WaitForSeconds(1f);
            beginningScreen.SetActive(false);
            OldScoreManager.Instance.EnableUI();
            OldScoreManager.Instance.ResetScore();
            ChangeState(GameState.Game);
        }

        public void SwitchToMainMenuScene() => SceneManager.LoadScene(mainMenuSceneName);
        public void SwitchToGameScene() => SceneManager.LoadScene(gameSceneName);
        private void SwitchToPostGameScene() => SceneManager.LoadScene(postGameSceneName);
        private void EnableCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public enum GameState
    {
        PreGame,
        Game,
        PostGame,
        ReturnToMenu
    }
}