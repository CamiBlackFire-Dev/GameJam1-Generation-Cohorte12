using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject pausePanel;

    [Header("Player Input")]
    public PlayerInput playerInput; // Opcional, para desactivar el movimiento

    private bool isPaused = false;

    private void Awake()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        // Detectar la tecla Esc usando el nuevo Input System (sin necesidad de action asset)
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);

        // Desactivamos el PlayerInput para que el jugador no se mueva
        if (playerInput != null)
            playerInput.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);

        if (playerInput != null)
            playerInput.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnResumeButton()
    {
        ResumeGame();
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        if (playerInput != null)
            playerInput.enabled = true;

        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}