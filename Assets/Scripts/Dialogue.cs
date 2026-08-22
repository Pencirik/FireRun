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
        
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            // IMPORTANTE: Usiamo WaitForSecondsRealtime perché il timeScale è a 0!
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