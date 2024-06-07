using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int correctAnswers = 0;

    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        UpdateScoreUI();
    }

    public void IncreaseScore()
    {
        correctAnswers++;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "/" + correctAnswers;
        }
    }
}
