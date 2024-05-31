using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menupanel1;
    public GameObject menupanel2;
    // public GameObject setting;
    // Start is called before the first frame update
    void Start()
    {
        menupanel2.SetActive(true);
        // menupanel2.SetActive(false);
        // setting.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtonPanel2(string scenename){
        SceneManager.LoadScene(scenename);
        }

    public void StartButtonPanel1(){
        menupanel1.SetActive(false);
        menupanel2.SetActive(true); 
    }

        public void ExitGame(){
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    // public void SettingButton(){
    //     menupanel1.SetActive(false);
    //     menupanel2.SetActive(false);
    //     setting.SetActive(true);
    // }

    // public void BackButton(){
    //     menupanel1.SetActive(false);
    //     menupanel2.SetActive(true);
    //     setting.SetActive(false);
    // }
    
}