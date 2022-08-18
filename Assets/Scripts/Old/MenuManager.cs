using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    #region Singleton

    public static MenuManager Instance;
    private void Awake() => Instance = this;

    #endregion

    public string difficultyLevel;
    public TMP_InputField mouseSensitivityInput;
    public TMP_InputField usernameInput;
    public Slider masterVolumeSlider;

    private void Start()
    {
        if(PlayerPrefs.HasKey("Username")) usernameInput.text = PlayerPrefs.GetString("Username");
        if(PlayerPrefs.HasKey("MouseSensitivity")) mouseSensitivityInput.text = PlayerPrefs.GetString("MouseSensitivity");
    }

    public void UpdateUsername()
    {
        PlayerPrefs.SetString("Username", usernameInput.text);
    }
        
    public void UpdateSensitivity()
    {
        PlayerPrefs.SetString("MouseSensitivity", mouseSensitivityInput.text);
    }
        
    public void UpdateTargetCount(int targetCount)
    {
        PlayerPrefs.SetInt("TargetCount", targetCount);
    }
        
    public void QuitGame()
    {
        Application.Quit();
    }
}