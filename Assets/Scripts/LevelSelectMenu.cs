using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour
{
    [Header("Pannelli")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject levelSelectPanel;

    [Header("Nomi delle Scene")]
    [SerializeField] private string tutorialSceneName = "TutorialScene";
    [SerializeField] private string level1SceneName = "Level1";
    [SerializeField] private string level2SceneName = "Level2";
    [SerializeField] private string level3SceneName = "Level3";

    // Pulsante "Go back" (Freccia per tornare indietro)
    public void OnBackButtonClicked()
    {
        if (levelSelectPanel != null) levelSelectPanel.SetActive(false);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true); 
    }

    // Caricamento dei singoli livelli
    public void PlayTutorial()
    {
        SceneManager.LoadScene(tutorialSceneName);
    }

    public void PlayLevel1()
    {
        SceneManager.LoadScene(level1SceneName);
    }

    public void PlayLevel2()
    {
        SceneManager.LoadScene(level2SceneName);
    }

    public void PlayLevel3()
    {
        SceneManager.LoadScene(level3SceneName);
    }
}