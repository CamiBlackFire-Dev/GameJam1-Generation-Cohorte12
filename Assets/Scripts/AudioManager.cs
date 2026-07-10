using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource musicAudioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameplayMusic;
    [SerializeField] private AudioClip buttonClick;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log("AudioManager iniciado");
        PlayMenuMusic();
    }

    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
    }

    public void PlayGameplayMusic()
    {
        PlayMusic(gameplayMusic);
    }

    public void PlayButtonClick()
    {
        PlaySFX(buttonClick);
    }
    
    public void PauseMusic()
    {
        musicAudioSource.Pause();
    }

    public void ResumeMusic()
    {
        musicAudioSource.UnPause();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        sfxAudioSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        if (musicAudioSource.clip == clip && musicAudioSource.isPlaying)
            return;

        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }
}