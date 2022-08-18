using TMPro;
using UnityEngine;

namespace Managers
{
    public class EndingScoreManager : MonoBehaviour
    {
        public TMP_Text scoreText;

        private void Start()
        {
            CalculateScore();
        }

        private void CalculateScore()
        {
            var score = PlayerPrefs.GetFloat("Score");
            var totalScore = PlayerPrefs.GetFloat("TotalScore");
            scoreText.text = "Score : " + score.ToString();
        }
    }
}
