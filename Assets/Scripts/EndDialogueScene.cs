using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("Configurazione Dialogo Finale")]
    public GameObject targetDialogueBox;

    private string endSceneName = "MainMenu";

    private bool isMonitoring = false;
    private bool wasActive = false;

    void Update()
    {
        if (targetDialogueBox == null) return;

        // Controlla se il box è appena diventato attivo
        if (targetDialogueBox.activeInHierarchy)
        {
            wasActive = true;
            isMonitoring = true; 
        }
        else
        {
            if (isMonitoring && wasActive)
            {
                TriggerEndScene();
            }
        }
    }

    void TriggerEndScene()
    {
        // Evitiamo che venga richiamato in loop a ogni frame
        isMonitoring = false;
        wasActive = false;

        if (!string.IsNullOrEmpty(endSceneName))
        {
            SceneManager.LoadScene(endSceneName);
        }
        else
        {
            Debug.LogWarning("Nessuna scena finale impostata nel DialogueManager!");
        }
    }
}