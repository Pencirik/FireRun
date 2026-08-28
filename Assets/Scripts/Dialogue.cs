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

    private PlayerController playerController;
    private Animator playerAnimator; // Riferimento all'Animator del player

    void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
            // Cerca il componente Animator sul player o nei suoi figli
            playerAnimator = player.GetComponentInChildren<Animator>();
        }
    }

    void OnEnable()
    {
        if (textComponent != null)
            textComponent.text = string.Empty;
        
        StartDialogue();
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
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
        Time.timeScale = 1f;
        index = 0;

        // Blocca il movimento del player all'inizio del dialogo
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Congela l'animazione del player nell'istante esatto
        if (playerAnimator != null)
        {
            playerAnimator.enabled = false;
        }

        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty;
        string currentLine = lines[index];
        
        int i = 0;
        while (i < currentLine.Length)
        {
            while (Time.timeScale == 0f)
            {
                yield return null;
            }

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
            // Riattiva il movimento del player alla fine del dialogo
            if (playerController != null)
            {
                playerController.enabled = true;
            }

            // Riattiva l'animator per far riprendere le animazioni
            if (playerAnimator != null)
            {
                playerAnimator.enabled = true;
            }

            gameObject.SetActive(false);
        }
    }
}