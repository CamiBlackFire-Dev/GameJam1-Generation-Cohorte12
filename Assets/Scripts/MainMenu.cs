using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    private void Start()
    {
        Time.timeScale = 1f;
    }

    
    public void PlayGame()
    {
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