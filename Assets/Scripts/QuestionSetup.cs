using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionSetup : MonoBehaviour
{
    [SerializeField]
    private List<QuestionData> allQuestions;
    private List<QuestionData> questions;
    private List<QuestionData> usedQuestions = new List<QuestionData>(); // List to store questions that have been used

    private QuestionData currentQuestion;

    [SerializeField]
    private TextMeshProUGUI questionText;
    [SerializeField]
    private TextMeshProUGUI categoryText;
    [SerializeField]
    private AnswerButton[] answerButtons;

    [SerializeField]
    private int correctAnswerChoice;

    [SerializeField]
    private GameObject quizCanvasObject; // Reference to the quiz Canvas as GameObject
    private Canvas quizCanvas; // Reference to the Canvas component

    [SerializeField]
    private NotificationManager notificationManager; // Reference to the NotificationManager

    [SerializeField]
    private GameObject itemPopup; // Reference to the item popup

    private int incorrectAnswerCount = 0; // Track number of incorrect answers

    private void Awake()
    {
        // Menyiapkan Pertanyaan
        GetQuestionAssets();

        // Get the Canvas component from the GameObject
        quizCanvas = quizCanvasObject.GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        ResetQuiz(); // Reset the quiz when the GameObject becomes active
    }

    private void GetQuestionAssets()
    {
        // Ngambil semua pertanyaan dari folder resources
        allQuestions = new List<QuestionData>(Resources.LoadAll<QuestionData>("Questions"));
        questions = new List<QuestionData>(allQuestions);
    }

    private void SelectNewQuestion()
    {
        if (questions.Count == 0)
        {
            // Reset questions list to its original state
            questions.AddRange(usedQuestions);
            usedQuestions.Clear();
        }

        // Shuffle the questions before selecting a new one
        ShuffleQuestions();

        int randomQuestionIndex = Random.Range(0, questions.Count);
        currentQuestion = questions[randomQuestionIndex];
        usedQuestions.Add(currentQuestion);
        questions.RemoveAt(randomQuestionIndex);
    }

    private void SetQuestionValues()
    {
        questionText.text = currentQuestion.question;
        categoryText.text = currentQuestion.category;
    }

    private void SetAnswerValues()
    {
        // Randomize urutan tombol jawaban
        List<string> answers = RandomizeAnswers(new List<string>(currentQuestion.answers));

        // Set up tombol jawaban
        for (int i = 0; i < answerButtons.Length; i++)
        {
            bool isCorrect = (i == correctAnswerChoice);

            answerButtons[i].SetIsCorrect(isCorrect);
            answerButtons[i].SetAnswerText(answers[i]);
            answerButtons[i].SetQuestionSetup(this); // Set the reference to QuestionSetup
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
            StartCoroutine(HandleCorrectAnswer()); // Show correct answer notification and item popup
        }
        else
        {
            Debug.Log("JAWABAN SALAH");
            incorrectAnswerCount++;

            if (incorrectAnswerCount >= 3)
            {
                CloseQuiz(); // Close the quiz canvas if 3 incorrect answers
            }
            else
            {
                ShowNotification("JAWABAN SALAH"); // Show incorrect answer notification
            }
        }
    }

    private IEnumerator HandleCorrectAnswer()
    {
        ShowNotification("JAWABAN BENAR"); // Show correct answer notification

        // Wait for the notification duration before showing the item popup
        yield return new WaitForSeconds(notificationManager.notificationDuration);

        ShowItemPopup(); // Show item popup
    }

    // Method to show notification
    public void ShowNotification(string message)
    {
        if (notificationManager != null)
        {
            notificationManager.ShowNotification(message);
        }
    }

    // Method to show item popup
    private void ShowItemPopup()
    {
        if (itemPopup != null)
        {
            itemPopup.SetActive(true);
        }
    }

    // Method to reset the quiz
    private void ResetQuiz()
    {
        // Reset quiz status
        incorrectAnswerCount = 0;

        // Select new question
        SelectNewQuestion();

        // Set question values
        SetQuestionValues();

        // Set answer values
        SetAnswerValues();

        // Open the quiz canvas
        OpenQuiz();
    }

    // Method to open the quiz canvas
    private void OpenQuiz()
    {
        if (quizCanvas != null)
        {
            quizCanvas.gameObject.SetActive(true);
        }
    }

    // Method to close the quiz canvas
    private void CloseQuiz()
    {
        if (quizCanvas != null)
        {
            quizCanvas.gameObject.SetActive(false);
        }
    }
}
