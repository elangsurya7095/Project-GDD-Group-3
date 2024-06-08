
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---Audio Source---")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("---Audio Source---")]
    public AudioClip background;
    public AudioClip jalan;
    //public AudioClip quiz;

    private void start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}
