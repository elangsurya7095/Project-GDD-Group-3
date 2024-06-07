using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionSetup : MonoBehaviour
{
    [SerializeField] private List<QuestionData> allQuestions;
    private List<QuestionData> questions;
    private List<QuestionData> usedQuestions = new List<QuestionData>();

    private QuestionData currentQuestion;

    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI categoryText;
    [SerializeField] private AnswerButton[] answerButtons;

    [SerializeField] private int correctAnswerChoice;

    [SerializeField] private GameObject quizCanvasObject;
    private Canvas quizCanvas;

    [SerializeField] private NotificationManager notificationManager;

    [SerializeField] private GameObject itemPopup;

    [SerializeField] private GameObject newPopup;

    private bool hasAnsweredCorrectly = false;

    // Tambahkan referensi ke ScoreManager
    private ScoreManager scoreManager;

    private void Awake()
    {
        GetQuestionAssets();
        quizCanvas = quizCanvasObject.GetComponent<Canvas>();

        // Cari ScoreManager di scene
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnEnable()
    {
        if (hasAnsweredCorrectly)
        {
            ShowNewPopup();
        }
        else
        {
            HideNotifications();
            HideItemPopup();
            HideNewPopup();

            SelectNewQuestion();
            SetAnswerValues();
            DisplayQuestionValues();
        }
    }

    private void GetQuestionAssets()
    {
        allQuestions = new List<QuestionData>(Resources.LoadAll<QuestionData>("Questions"));
        questions = new List<QuestionData>(allQuestions);
    }

    private void SelectNewQuestion()
    {
        List<QuestionData> availableQuestions = FilterAvailableQuestions();

        if (availableQuestions.Count == 0)
        {
            questions.AddRange(usedQuestions);
            usedQuestions.Clear();
            availableQuestions = FilterAvailableQuestions();
        }

        ShuffleQuestions();

        int randomQuestionIndex = Random.Range(0, availableQuestions.Count);
        currentQuestion = availableQuestions[randomQuestionIndex];
        usedQuestions.Add(currentQuestion);
        questions.Remove(currentQuestion);

        DisplayQuestionValues();
    }

    private List<QuestionData> FilterAvailableQuestions()
    {
        List<QuestionData> availableQuestions = new List<QuestionData>();
        foreach (QuestionData question in questions)
        {
            if (!question.answeredCorrectly)
            {
                availableQuestions.Add(question);
            }
        }
        return availableQuestions;
    }

    private void DisplayQuestionValues()
    {
        questionText.text = currentQuestion.question;
        categoryText.text = currentQuestion.category;
    }

    public void CorrectAnswerSelected()
    {
        // Periksa apakah pertanyaan ini belum digunakan untuk meningkatkan skor
        if (!currentQuestion.isCorrectAnswered)
        {
            currentQuestion.isCorrectAnswered = true; // Menandai pertanyaan sebagai sudah dijawab dengan benar
            currentQuestion.answeredCorrectly = true;
            questions.Remove(currentQuestion);

            // Tambahkan logika untuk meningkatkan skor
            if (scoreManager != null)
            {
                scoreManager.IncreaseScore();
            }
        }
    }

    private void SetAnswerValues()
    {
        List<string> answers = RandomizeAnswers(new List<string>(currentQuestion.answers));

        for (int i = 0; i < answerButtons.Length; i++)
        {
            bool isCorrect = (i == correctAnswerChoice);

            answerButtons[i].SetIsCorrect(isCorrect);
            answerButtons[i].SetAnswerText(answers[i]);
            answerButtons[i].SetQuestionSetup(this);
        }
    }

    private List<string> RandomizeAnswers(List<string> originalList)
    {
        bool correctAnswerChosen = false;
        List<string> newList = new List<string>();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int random = Random.Range(0, originalList.Count);

            if (random == 0 && !correctAnswerChosen)
            {
                correctAnswerChoice = i;
                correctAnswerChosen = true;
            }

            newList.Add(originalList[random]);
            originalList.RemoveAt(random);
        }

        return newList;
    }

    private void ShuffleQuestions()
    {
        for (int i = 0; i < questions.Count; i++)
        {
            QuestionData temp = questions[i];
            int randomIndex = Random.Range(i, questions.Count);
            questions[i] = questions[randomIndex];
            questions[randomIndex] = temp;
        }
    }

    public void CheckAnswer(bool isCorrect)
    {
        if (isCorrect)
        {
            Debug.Log("JAWABAN BENAR");
            hasAnsweredCorrectly = true;
            ShowNotification("JAWABAN BENAR");
            ShowItemPopup();

            // Panggil CorrectAnswerSelected ketika jawaban benar
            CorrectAnswerSelected();
        }
        else
        {
            Debug.Log("JAWABAN SALAH");
            ShowNotification("JAWABAN SALAH");
        }
    }

    public void ShowNotification(string message)
    {
        if (notificationManager != null)
        {
            notificationManager.ShowNotification(message);
        }
    }

    private void HideNotifications()
    {
        if (notificationManager != null)
        {
            notificationManager.HideNotification();
        }
    }

    private void ShowItemPopup()
    {
        if (itemPopup != null)
        {
            itemPopup.SetActive(true);
        }
    }

    private void HideItemPopup()
    {
        if (itemPopup != null)
        {
            itemPopup.SetActive(false);
        }
    }

    private void ShowNewPopup()
    {
        if (newPopup != null)
        {
            newPopup.SetActive(true);
        }
    }

    private void HideNewPopup()
    {
        if (newPopup != null)
        {
            newPopup.SetActive(false);
        }
    }
}
