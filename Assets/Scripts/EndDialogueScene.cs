using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DialogueManager : MonoBehaviour
{
    [Header("Configurazione Dialogo Finale")]
    public GameObject targetDialogueBox;

#if UNITY_EDITOR
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

        // Ripristina il tempo di gioco
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