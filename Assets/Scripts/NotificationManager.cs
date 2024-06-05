using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    [SerializeField]
    private GameObject notificationPopup; // The popup game object
    [SerializeField]
    private TextMeshProUGUI notificationText; // The text component in the popup
    public float notificationDuration = 2f; // Duration for which the notification is displayed

    private Coroutine currentCoroutine;

    // Method to show the notification
    public void ShowNotification(string message)
    {
        notificationPopup.SetActive(true);
        notificationText.text = message;

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(HideNotificationAfterDelay());
    }

    // Coroutine to hide the notification after a delay
    private IEnumerator HideNotificationAfterDelay()
    {
        yield return new WaitForSeconds(notificationDuration);
        notificationPopup.SetActive(false);
    }
}
