 using UnityEngine;


[System.Serializable]

public class DialogueTriggerStep

{

    public float targetZ = 10f;             // La Z da raggiungere per questo step

    public GameObject dialogueBox;          // Il pannello/dialogo da attivare

}


public class TriggerDialogue : MonoBehaviour

{

    public Transform player;                // Il player

    public DialogueTriggerStep[] steps;     // Lista degli step (puoi aggiungerne quanti ne vuoi dall'Inspector)


    private int currentStepIndex = 0;


    void Start()

    {

        if (player == null)

            player = GameObject.FindGameObjectWithTag("Player").transform;


        // Assicurati che tutti i dialoghi della lista partano spenti

        foreach (var step in steps)

        {

            if (step.dialogueBox != null)

                step.dialogueBox.SetActive(false);

        }

    }


    void Update()

    {

        // Se abbiamo completato tutti gli step, disattiva lo script

        if (currentStepIndex >= steps.Length)

        {

            enabled = false;

            return;

        }


        DialogueTriggerStep currentStep = steps[currentStepIndex];


        // Quando il player supera la Z target dello step attuale

        if (player.position.z >= currentStep.targetZ)

        {

            // 1. Mette in pausa il gioco

            Time.timeScale = 0f;

            

            // 2. Attiva il rispettivo box di dialogo

            if (currentStep.dialogueBox != null)

                currentStep.dialogueBox.SetActive(true);

            

            // 3. Passa allo step successivo della lista

            currentStepIndex++;

        }

    }

} 