using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    private bool isCorrect;
    private QuestionSetup questionSetup; // Reference to QuestionSetup
    [SerializeField] private TextMeshProUGUI answerText;

    public void SetAnswerText(string newText)
    {
        answerText.text = newText;
    }

    public void SetIsCorrect(bool newBool)
    {
        isCorrect = newBool;
    }

    public void SetQuestionSetup(QuestionSetup setup)
    {
        questionSetup = setup;
    }

    public void OnClick()
    {
        if (isCorrect)
        {
            Debug.Log("JAWABAN BENAR");
        }
        else
        {
            Debug.Log("JAWABAN SALAH");
        }

        questionSetup.CheckAnswer(isCorrect);
    }
}
