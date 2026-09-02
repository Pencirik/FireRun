using UnityEngine;
using System.Collections.Generic;

public class MoveFire : MonoBehaviour
{
    [Header("Impostazioni Movimento")]
    [SerializeField] private float moveSpeed = 3f;
    
    [Header("Riferimenti Dialoghi")]
    [SerializeField] private List<GameObject> targetDialogueBoxes = new List<GameObject>();

    void Update()
    {
        if (Time.timeScale == 0f) return;

        bool isAnyDialogueActive = false;

        // Controlla l'intera lista per vedere se almeno uno dei dialoghi è attualmente attivo
        foreach (GameObject dialogueBox in targetDialogueBoxes)
        {
            if (dialogueBox != null && dialogueBox.activeInHierarchy)
            {
                isAnyDialogueActive = true;
                break;
            }
        }

        float currentSpeed = isAnyDialogueActive ? 0f : moveSpeed;

        if (currentSpeed > 0f)
        {
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        }
    }
}