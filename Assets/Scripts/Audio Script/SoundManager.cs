using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Singleton instance of SoundManager
    public static SoundManager Instance { get; private set; }
    public AudioSource music;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            Debug.Log("SoundManager instance created.");
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Duplicate SoundManager instance destroyed.");
        }
    }
   
    void Start()
    {
        
    }
    
    void Update()
    {
       
    }

    public void MuteSong()
    {
        music.mute = !music.mute;
        Debug.Log("Music mute toggled. Current mute state: " + music.mute);
    }
}
