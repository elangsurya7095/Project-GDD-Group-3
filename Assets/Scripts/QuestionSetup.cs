using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionSetup : MonoBehaviour
{
    [SerializeField]
    private List<QuestionData> allQuestions;
    public List<QuestionData> questions;
    private QuestionData currentQuestion;
    private List<QuestionData> usedQuestions = new List<QuestionData>(); // List to store questions that have been used

    [SerializeField]
    private TextMeshProUGUI questionText;
    [SerializeField]
    private TextMeshProUGUI categoryText;
    [SerializeField]
    private AnswerButton[] answerButtons;

    [SerializeField]
    private int correctAnswerChoice;

    private void Awake()
    {
        // Menyiapkan Pertanyaan
        GetQuestionAssets();
    }

    public void OnEnable()
    {
        SelectNewQuestion();
        SetQuestionValues();
        SetAnswerValues();
    }


    private void GetQuestionAssets()
    {
        // Ngambil semua pertanyaan dari folder resources
        questions = new List<QuestionData>(Resources.LoadAll<QuestionData>("Questions"));
    }

    private void SelectNewQuestion()
    {
        if (questions.Count == 0)
        {
            // Reset questions list to its original state
            questions.AddRange(usedQuestions);
            usedQuestions.Clear();
        }

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
            bool isCorrect = false;

            if(i == correctAnswerChoice)
            {
                isCorrect = true;
            }

            answerButtons[i].SetIsCorrect(isCorrect);
            answerButtons[i].SetAnswerText(answers[i]);
        }
    }

    private List<string> RandomizeAnswers(List<string> originalList)
    {
        bool correctAnswerChosen = false;

        List<string>  newList = new List<string>();

        for(int i = 0; i < answerButtons.Length; i++)
        {
            int random = Random.Range(0, originalList.Count);

            if(random == 0 && !correctAnswerChosen)
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
}