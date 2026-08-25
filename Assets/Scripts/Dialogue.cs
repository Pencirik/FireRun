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

    // SOSTITUIAMO START CON ONENABLE: 
    // In questo modo, ogni volta che un DialogueBox viene acceso (SetActive(true)), 
    // partirà automaticamente il dialogo senza perdersi i tempi di Unity.
    void OnEnable()
    {
        if (textComponent != null)
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
            if (currentLine[i] == '<')
            {
                int closingIndex = currentLine.IndexOf('>', i);
                if (closingIndex != -1)
                {
                    textComponent.text += currentLine.Substring(i, (closingIndex - i) + 1);
                    i = closingIndex + 1;
                    continue;
                }
            }

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
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
    }
}