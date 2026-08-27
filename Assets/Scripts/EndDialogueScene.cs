using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DialogueManager : MonoBehaviour
{
    [Header("Configurazione Dialogo Finale")]
    [Tooltip("Trascina qui il GameObject del box di dialogo da monitorare.")]
    public GameObject targetDialogueBox;

#if UNITY_EDITOR
    [Tooltip("Trascina qui la scena finale da caricare quando questo box di dialogo si chiude.")]
    public SceneAsset endSceneAsset;
#endif
    private string endSceneName;

    private bool isMonitoring = false;
    private bool wasActive = false;

#if UNITY_EDITOR
    void OnValidate()
    {
        if (endSceneAsset != null)
        {
            string path = AssetDatabase.GetAssetPath(endSceneAsset);
            int startIdx = path.LastIndexOf('/') + 1;
            int endIdx = path.LastIndexOf('.');
            if (endIdx > startIdx)
            {
                endSceneName = path.Substring(startIdx, endIdx - startIdx);
            }
        }
    }
#endif

    void Update()
    {
        // Se non abbiamo impostato un box di dialogo target, non facciamo nulla
        if (targetDialogueBox == null) return;

        // Controlla se il box è appena diventato attivo
        if (targetDialogueBox.activeInHierarchy)
        {
            wasActive = true;
            isMonitoring = true; // Iniziamo a monitorare la sua chiusura
        }
        else
        {
            // Se lo stavamo monitorando, era attivo, e ora è diventato spento (chiuso)...
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

        // Ripristina il tempo di gioco per sicurezza
        Time.timeScale = 1f;

        if (!string.IsNullOrEmpty(endSceneName))
        {
            SceneManager.LoadScene(endSceneName);
        }
        else
        {
            Debug.LogWarning("Nessuna scena finale selezionata nel DialogueManager!");
        }
    }
}