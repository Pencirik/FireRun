using UnityEngine;
using UnityEngine.SceneManagement;

public class FireDamage : MonoBehaviour
{
    [Header("UI di Game Over")]
    [SerializeField] private GameObject gameOverPanel; // Trascina qui il pannello della morte

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se l'oggetto che entra nel fuoco è il Player (tramite tag)
        if (other.CompareTag("Player"))
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        // Attiva il pannello di Game Over se è stato assegnato
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Sblocca e mostra il cursore del mouse per permettere di cliccare sui bottoni del menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Mette in pausa il gioco bloccando il tempo
        Time.timeScale = 0f;

        Debug.Log("Game Over: Il giocatore è entrato nel fuoco!");
    }
}