using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private GameObject notificationPopup;
    [SerializeField] private TextMeshProUGUI notificationText;
    [SerializeField] private float notificationDuration = 1.5f; // Durasi notifikasi dalam detik

    private Coroutine currentNotificationCoroutine;

    public void ShowNotification(string message)
    {
        // Set the text and activate the notification popup
        notificationText.text = message;
        notificationPopup.SetActive(true);

        // If there is an existing notification coroutine, stop it
        if (currentNotificationCoroutine != null)
        {
            StopCoroutine(currentNotificationCoroutine);
        }

        // Start a new coroutine to hide the notification after the specified duration
        currentNotificationCoroutine = StartCoroutine(HideNotificationAfterDelay());
    }

    private IEnumerator HideNotificationAfterDelay()
    {
        yield return new WaitForSeconds(notificationDuration);
        HideNotification();
    }

    public void HideNotification()
    {
        notificationPopup.SetActive(false);
    }
}
