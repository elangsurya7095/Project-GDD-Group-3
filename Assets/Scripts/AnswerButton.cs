using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    private bool isCorrect;
    [SerializeField] private TextMeshProUGUI answerText;
    private QuestionSetup questionSetup; // Reference to QuestionSetup

    public void SetAnswerText(string newText)
    {
        answerText.text = newText;
    }

    public void SetIsCorrect(bool newBool)
    {
        isCorrect = newBool;
    }

    public void SetQuestionSetup(QuestionSetup setup) // Method to set reference to QuestionSetup
    {
        questionSetup = setup;
    }

    public void OnClick()
    {
        questionSetup.CheckAnswer(isCorrect);
    }
}
