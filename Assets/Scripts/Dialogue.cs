using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue() 
    {
        // 1. Blocca il tempo di gioco (ferma il player, la fisica, ecc.)
        Time.timeScale = 0f;

        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
{
    textComponent.text = string.Empty;
    string currentLine = lines[index];
    
    int i = 0;
    while (i < currentLine.Length)
    {
        // Se troviamo l'inizio di un tag (es. "<sprite...")
        if (currentLine[i] == '<')
        {
            // Cerchiamo la fine del tag '>'
            int closingIndex = currentLine.IndexOf('>', i);
            if (closingIndex != -1)
            {
                // Aggiungiamo l'intero tag al testo tutto in un colpo solo
                textComponent.text += currentLine.Substring(i, (closingIndex - i) + 1);
                // Spostiamo l'indice alla fine del tag
                i = closingIndex + 1;
                continue;
            }
        }

        // Altrimenti, scriviamo il normale carattere uno alla volta
        textComponent.text += currentLine[i];
        i++;
        
        yield return new WaitForSecondsRealtime(textSpeed);
    }
}

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            // 2. Quando il dialogo finisce, riattiva il tempo di gioco
            Time.timeScale = 1f;
            
            // 3. Disattiva il pannello del dialogo
            gameObject.SetActive(false);
        }
    }
}