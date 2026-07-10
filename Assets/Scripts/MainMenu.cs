using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    private void Start()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.PlayMenuMusic();
    }

    
    public void PlayGame()
    {
        AudioManager.Instance.PlayGameplayMusic();
        SceneManager.LoadScene(1);
        
    }

    
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}