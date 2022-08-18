using System;
using System.Globalization;
using Levels;
using Old;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class MainMenuManager : MonoBehaviour
    {
        #region Variables

        //SCRIPT REFERENCE
        [SerializeField] private GameManager gameManager;
        [SerializeField] private LevelsManager levelsManager;
        [SerializeField] private LevelDescriptionBank levelDescriptionBank;
        
        //Input Reference
        [SerializeField] private TMP_InputField usernameInput;
        [SerializeField] private Slider mouseSensitivityInput;
        [SerializeField] private TMP_Text mouseSensitivity;
        [SerializeField] private Slider masterVolumeSliderInput;
        
        //Level Description Reference
        [SerializeField] private TMP_Text levelName;
        [SerializeField] private TMP_Text levelDescription;
        [SerializeField] private Image levelImage;
        
        //Private reference variables
        private string _levelSelected;

        #endregion

        private void Start()
        {
            UpdateVariables();
        }

        #region SaveAndUpdateVariables

        private void UpdateVariables()
        {
            if (PlayerPrefs.HasKey("Username")) usernameInput.text = PlayerPrefs.GetString("Username");
            if (PlayerPrefs.HasKey("MouseSensitivity"))
            { 
                mouseSensitivityInput.value = PlayerPrefs.GetFloat("MouseSensitivity"); 
                mouseSensitivity.text = mouseSensitivityInput.value.ToString(CultureInfo.InvariantCulture); 
            } 
            if (PlayerPrefs.HasKey("MasterVolume")) masterVolumeSliderInput.value = PlayerPrefs.GetFloat("MasterVolume");
        }

        public void Save()
        {
            UpdateVariables();
        }

        public void UpdateUsername()
        {
            PlayerPrefs.SetString("Username", usernameInput.text);
        }

        public void UpdateSensitivity()
        {
            mouseSensitivity.text = mouseSensitivityInput.value.ToString(CultureInfo.InvariantCulture);
            PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivityInput.value);
        }

        public void UpdateMasterVolume()
        {
            PlayerPrefs.SetFloat("MasterVolume", masterVolumeSliderInput.value);
        }

        #endregion

        #region LevelSelectionAndActions

        public void StoreLevelSelection(string currentLevelName)
        {
            _levelSelected = currentLevelName;
            ShowLevelDescription();
        }

        private void ShowLevelDescription()
        {
            //Add Level specific name, description, image references
            switch (_levelSelected)
            {
                case "Flicking Task Grid Shot Level":
                    levelName.text = levelDescriptionBank.flickingTaskGridShotLevelName;
                    levelDescription.text = levelDescriptionBank.flickingTaskGridShotLevelDescription;
                    levelImage.sprite = levelDescriptionBank.flickingTaskGridShotLevelImage;
                    break;
                case "Precision Task Micro Shot Level":
                    levelName.text = levelDescriptionBank.precisionTaskMicroShotLevelName;
                    levelDescription.text = levelDescriptionBank.precisionTaskMicroShotLevelDescription;
                    levelImage.sprite = levelDescriptionBank.precisionTaskMicroShotLevelImage;
                    break;
                case "Precision Task Spider Shot Level":
                    levelName.text = levelDescriptionBank.precisionTaskSpiderShotLevelName;
                    levelDescription.text = levelDescriptionBank.precisionTaskSpiderShotLevelDescription;
                    levelImage.sprite = levelDescriptionBank.precisionTaskSpiderShotLevelImage;
                    break;
                case "Tracking Task Motion Track Level":
                    levelName.text = levelDescriptionBank.trackingTaskMotionTrackLevelName;
                    levelDescription.text = levelDescriptionBank.trackingTaskMotionTrackLevelDescription;
                    levelImage.sprite = levelDescriptionBank.trackingTaskMotionTrackLevelImage;
                    break;
                case "Flicking Task Micro Flex Level":
                    levelName.text = levelDescriptionBank.flickingTaskMicroFlexLevelName;
                    levelDescription.text = levelDescriptionBank.flickingTaskMicroFlexLevelDescription;
                    levelImage.sprite = levelDescriptionBank.flickingTaskMicroFlexLevelImage;
                    break;
                case "Tracking Task Strafe Bot Level":
                    levelName.text = levelDescriptionBank.trackingTaskStrafeBotLevelName;
                    levelDescription.text = levelDescriptionBank.trackingTaskStrafeBotLevelDescription;
                    levelImage.sprite = levelDescriptionBank.trackingTaskStrafeBotLevelImage;
                    break;
                case "Flicking Task Spider Shot 180 Level":
                    levelName.text = levelDescriptionBank.flickingTaskSpiderShot180LevelName;
                    levelDescription.text = levelDescriptionBank.flickingTaskSpiderShot180LevelDescription;
                    levelImage.sprite = levelDescriptionBank.flickingTaskSpiderShot180LevelImage;
                    break;
                case "Flicking Task Tile Frenzy Level":
                    levelName.text = levelDescriptionBank.flickingTaskTileFrenzyLevelName;
                    levelDescription.text = levelDescriptionBank.flickingTaskTileFrenzyLevelDescription;
                    levelImage.sprite = levelDescriptionBank.flickingTaskTileFrenzyLevelImage;
                    break;
                case "Switching Task Decision Shot Level":
                    levelName.text = levelDescriptionBank.switchingTaskDecisionShotLevelName;
                    levelDescription.text = levelDescriptionBank.switchingTaskDecisionShotLevelDescription;
                    levelImage.sprite = levelDescriptionBank.switchingTaskDecisionShotLevelImage;
                    break;
                case "Flicking Task Motion Shot Level":
                    levelName.text = levelDescriptionBank.flickingTaskMotionShotLevelName;
                    levelDescription.text = levelDescriptionBank.flickingTaskMotionShotLevelDescription;
                    levelImage.sprite = levelDescriptionBank.flickingTaskMotionShotLevelImage;
                    break;
                case "Precision Task Micro Shot Speed Level":
                    levelName.text = levelDescriptionBank.precisionTaskMicroShotSpeedLevelName;
                    levelDescription.text = levelDescriptionBank.precisionTaskMicroShotSpeedLevelDescription;
                    levelImage.sprite = levelDescriptionBank.precisionTaskMicroShotSpeedLevelImage;
                    break;
                case "Precision Task Detection Level":
                    levelName.text = levelDescriptionBank.precisionTaskDetectionLevelName;
                    levelDescription.text = levelDescriptionBank.precisionTaskDetectionLevelDescription;
                    levelImage.sprite = levelDescriptionBank.precisionTaskDetectionLevelImage;
                    break;
            }
        }

        public void LoadLevel()
        {
            //Call Level specific load level
            switch (_levelSelected)
            {
                case "Flicking Task Grid Shot Level": levelsManager.LoadLevel(Levels.Levels.FlickingTaskGridShot); break;
                case "Precision Task Micro Shot Level": levelsManager.LoadLevel(Levels.Levels.PrecisionTaskMicroShot); break;
                case "Precision Task Spider Shot Level": levelsManager.LoadLevel(Levels.Levels.PrecisionTaskSpiderShot); break;
                case "Tracking Task Motion Track Level": levelsManager.LoadLevel(Levels.Levels.TrackingTaskMotionTrack); break;
                case "Flicking Task Micro Flex Level": levelsManager.LoadLevel(Levels.Levels.FlickingTaskMicroFlex); break;
                case "Tracking Task Strafe Bot Level": levelsManager.LoadLevel(Levels.Levels.TrackingTaskStrafeBot); break;
                case "Flicking Task Spider Shot 180 Level": levelsManager.LoadLevel(Levels.Levels.FlickingTaskSpiderShot180); break;
                case "Flicking Task Tile Frenzy Level": levelsManager.LoadLevel(Levels.Levels.FlickingTaskTileFrenzy); break;
                case "Switching Task Decision Shot Level": levelsManager.LoadLevel(Levels.Levels.SwitchingTaskDecisionShot); break;
                case "Flicking Task Motion Shot Level": levelsManager.LoadLevel(Levels.Levels.FlickingTaskMotionShot); break;
                case "Precision Task Micro Shot Speed Level": levelsManager.LoadLevel(Levels.Levels.PrecisionTaskMicroShotSpeed); break;
                case "Precision Task Detection Level": levelsManager.LoadLevel(Levels.Levels.PrecisionTaskDetection); break;
            }
        }

        #endregion
        
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
