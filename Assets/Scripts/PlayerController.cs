using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool isJumping = false; // Variabile di blocco per evitare lo spam

    [Header("Parametri di Movimento")]
    [SerializeField] private float playerSpeed = 6.0f;
    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float gravityValue = -20.0f;
    [SerializeField] private float rotationSpeed = 15.0f;

    [Header("Animazioni")]
    [SerializeField] private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    void Update()
    {
        // Verifica se il player è a terra
        groundedPlayer = controller.isGrounded;
        
        if (groundedPlayer)
        {
            if (playerVelocity.y < 0)
            {
                playerVelocity.y = -2f; 
            }

            // Quando tocchiamo terra, sblocchiamo la possibilità di saltare e diciamo all'animator che non stiamo saltando
            if (isJumping)
            {
                isJumping = false;
                if (animator != null)
                {
                    animator.SetBool("IsJumping", false);
                }
            }
        }
        else
        {
            // Se siamo in aria, applichiamo la gravità
            playerVelocity.y += gravityValue * Time.deltaTime;
        }

        // Input da tastiera
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) moveZ += 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) moveZ -= 1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveX += 1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveX -= 1f;

        // Passa i dati al Blend Tree
        if (animator != null && !isJumping)
        {
            animator.SetFloat("InputX", moveX);
            animator.SetFloat("InputY", moveZ);
        }

        // Movimento orizzontale
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;
        
        // Rotazione
        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Gestione del salto
        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer && !isJumping)
        {
            isJumping = true;
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);

            if (animator != null)
            {
                animator.SetBool("IsJumping", true);
            }
        }

        // Applicazione finale del movimento
        Vector3 finalMovement = (move * playerSpeed) + new Vector3(0, playerVelocity.y, 0);
        controller.Move(finalMovement * Time.deltaTime);
    }
}