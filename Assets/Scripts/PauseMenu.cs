using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Pannello di Pausa")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject settingsMenuPanel;
    [SerializeField] private GameObject gameOverPanel;

    private bool isPaused = false;
    private GameObject previousPanel;

    void Start()
    {
        Time.timeScale = 1f; 
    }

    void Update()
    {
        if (gameOverPanel != null && gameOverPanel.activeSelf) return;

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
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f; 
        isPaused = false;
    }

    void PauseGame()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void OpenSettings()
    {
        // Controlla da solo quale pannello è aperto e lo memorizza
        if (pauseMenuPanel != null && pauseMenuPanel.activeSelf)
        {
            previousPanel = pauseMenuPanel;
            pauseMenuPanel.SetActive(false);
        }
        else if (gameOverPanel != null && gameOverPanel.activeSelf)
        {
            previousPanel = gameOverPanel;
            gameOverPanel.SetActive(false);
        }

        if (settingsMenuPanel != null) settingsMenuPanel.SetActive(true);
    }

    public void CloseSettings()
    {
       if (settingsMenuPanel != null) settingsMenuPanel.SetActive(false);

       // Riapre automaticamente il pannello salvato in precedenza
       if (previousPanel != null)
       {
           previousPanel.SetActive(true);
       }
       else if (pauseMenuPanel != null)
       {
           pauseMenuPanel.SetActive(true); // Fallback di sicurezza
       }
    }

    // Chiamato dal pulsante "Retry" del pannello Game Over
    public void RetryGame()
    {
        Time.timeScale = 1f;
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; 

        // Sblocca e mostra il mouse per il menu principale
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("MainMenu");
    }
}