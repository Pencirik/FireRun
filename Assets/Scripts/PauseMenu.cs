using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Pannello di Pausa")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject settingsMenuPanel;

    private bool isPaused = false;

    void Start()
    {
        Time.timeScale = 1f; // Assicura che il tempo sia sempre attivo nel menu
    }

    void Update()
    {
        // Controlla se il giocatore preme il tasto ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f; // Riattiva il tempo di gioco normale
        isPaused = false;
    }

    void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f; // Blocca completamente il tempo (ferma il fumo, il player, ecc.)
        isPaused = true;
    }

     public void OpenSettings()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (settingsMenuPanel != null) settingsMenuPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsMenuPanel != null) settingsMenuPanel.SetActive(false);
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
    }


public void QuitToMainMenu()
{
    Time.timeScale = 1f; // Riattiva il tempo

    // Sblocca e mostra il mouse per il menu principale
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;

    SceneManager.LoadScene("MainMenu");
}

}