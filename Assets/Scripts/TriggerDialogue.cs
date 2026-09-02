 using UnityEngine;


[System.Serializable]
public class DialogueTriggerStep
{
    public float targetZ = 10f;
    public GameObject dialogueBox;
}

public class TriggerDialogue : MonoBehaviour
{
    public Transform player;
    public DialogueTriggerStep[] steps;

    private int currentStepIndex = 0;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        
        foreach (var step in steps)
        {
            if (step.dialogueBox != null)
               step.dialogueBox.SetActive(false);
        }
    }

    void Update()
    {
        if (currentStepIndex >= steps.Length)
        {
            enabled = false;
            return;
        }

        DialogueTriggerStep currentStep = steps[currentStepIndex];        

        if (player.position.z >= currentStep.targetZ)
        {
            Time.timeScale = 0f;
            
            if (currentStep.dialogueBox != null)
                currentStep.dialogueBox.SetActive(true);

            currentStepIndex++;
        }
    }
} 