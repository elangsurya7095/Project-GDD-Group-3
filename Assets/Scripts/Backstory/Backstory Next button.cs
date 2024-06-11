using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackstoryNextButton : MonoBehaviour
{
    public GameObject PanelDialog1;
    public GameObject PanelDialog2;
    public GameObject PanelDialog3;
    public string mainMenuSceneName = "Main Menu"; // Nama scene tujuan

    private List<GameObject> panels;
    private int currentPanelIndex = 0;

    void Start()
    {
        panels = new List<GameObject> { PanelDialog1, PanelDialog2, PanelDialog3 };
        UpdatePanelVisibility();
    }

    void Update()
    {
        // Update code if needed
    }

    public void NextPanel()
    {
        if (currentPanelIndex < panels.Count - 1)
        {
            currentPanelIndex++;
            UpdatePanelVisibility();

            // Jika sudah di panel terakhir, muat scene baru
            if (currentPanelIndex == panels.Count - 1)
            {
                StartCoroutine(LoadMainMenuAfterDelay(2f)); // Menunggu 2 detik sebelum memuat scene baru
            }
        }
    }

    private void UpdatePanelVisibility()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].SetActive(i == currentPanelIndex);
        }
    }

    private IEnumerator LoadMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
