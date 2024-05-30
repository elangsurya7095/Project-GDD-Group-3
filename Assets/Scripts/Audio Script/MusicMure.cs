using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicMure : MonoBehaviour
{   
    public Sprite[] spriteMute;
    public Button buttonMute;

    // Start is called before the first frame update
    void Start()
    {
      UpdateMuteButtonSprite();
      buttonMute.onClick.AddListener(ButtonMuteMusic);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonMuteMusic()
    {
        SoundManager.Instance.MuteSong();
        UpdateMuteButtonSprite();
    }

    public void UpdateMuteButtonSprite()
    {
        
        if (SoundManager.Instance.music.mute)
        {
            buttonMute.image.sprite = spriteMute[1];  
        }
        else
        {
            buttonMute.image.sprite = spriteMute[0];
        }
    }
}
