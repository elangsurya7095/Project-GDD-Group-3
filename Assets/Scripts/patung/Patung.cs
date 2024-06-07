using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Patung : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool playerinRange;

    [SerializeField]
    private QuestionSetup questionSetup;

    private void Start()
    {
        if (questionSetup == null)
        {
            questionSetup = GetComponentInChildren<QuestionSetup>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerinRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                // Initialize the quiz for this specific Patung
                questionSetup.enabled = true; // This will trigger the OnEnable method of QuestionSetup
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerinRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerinRange = false;
            dialogBox.SetActive(false); // Close the dialog box when the player leaves the range
        }
    }
}
