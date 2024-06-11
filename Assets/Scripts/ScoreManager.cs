using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private int correctAnswers = 0;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int scoreThreshold = 5; // Nilai threshold untuk pindah scene
    [SerializeField] private string targetSceneName; // Nama scene tujuan

    private void Start()
    {
        UpdateScoreUI();
    }

    public void IncreaseScore()
    {
        correctAnswers++;
        UpdateScoreUI();
        CheckScoreThreshold();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "/" + correctAnswers;
        }
    }

    private void CheckScoreThreshold()
    {
        if (correctAnswers >= scoreThreshold)
        {
            LoadTargetScene();
        }
    }

    private void LoadTargetScene()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("Target scene name is not set or empty!");
        }
    }
}
