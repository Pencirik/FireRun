using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Pannelli")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject levelSelectPanel;
    [SerializeField] private GameObject settingsMenuPanel; // <-- Aggiunto il pannello impostazioni

    void Start()
    {
        Time.timeScale = 1f; // Assicura che il tempo sia sempre attivo nel menu
    }

    // Chiamato dal pulsante "Gioca" del menu principale
    public void OnPlayButtonClicked()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (levelSelectPanel != null) levelSelectPanel.SetActive(true);
    }

    // Chiamato dal pulsante "Impostazioni" del menu principale
    public void OpenSettings()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (settingsMenuPanel != null) settingsMenuPanel.SetActive(true);
    }

    // Chiamato dal pulsante "Indietro" (Back) dentro il pannello impostazioni
    public void CloseSettings()
    {
        if (settingsMenuPanel != null) settingsMenuPanel.SetActive(false);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Chiusura gioco...");
        Application.Quit();
    }
}