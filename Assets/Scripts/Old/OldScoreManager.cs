using Managers;
using Old;
using TMPro;
using UnityEngine;

public class OldScoreManager : MonoBehaviour
{
    #region Singleton

    public static OldScoreManager Instance;
    private void Awake() => Instance = this;

    #endregion

    public string sceneName;
        
    [HideInInspector]
    public float hits;
    [HideInInspector]
    public float misses;
    [HideInInspector] 
    public int totalTargets;

    [SerializeField] private TMP_Text hitsText;
    [SerializeField] private TMP_Text missesText;

    private bool _levelCompleted;
    
    private void Start()
    {
        hits = 0;
        misses = 0;
        totalTargets = PlayerPrefs.GetInt("TargetCount");
        _levelCompleted = false;
    }

    private void Update()
    {
        hitsText.text = "Hits : " + hits;
        missesText.text = "Misses : " + misses;
        if ((hits + misses) >= totalTargets)
        {
            PlayerPrefs.SetFloat("Score", hits);
            if(!_levelCompleted) PostGame();
        }
    }

    private void PostGame()
    {
        GameManager.Instance.ChangeState(GameState.PostGame);
        _levelCompleted = true;
    }

    public void ResetScore()
    {
        hits = 0;
        misses = 0;
    }

    public void EnableUI()
    {
        hitsText.gameObject.SetActive(true);
        missesText.gameObject.SetActive(true);
    }
    public void DisableUI()
    {
        hitsText.gameObject.SetActive(false);
        missesText.gameObject.SetActive(false);
    }
}