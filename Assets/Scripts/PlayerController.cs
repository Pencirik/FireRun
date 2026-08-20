using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [Header("Parametri di Movimento")]
    [SerializeField] private float playerSpeed = 6.0f;
    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float gravityValue = -20.0f;
    [SerializeField] private float rotationSpeed = 15.0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Verifica se il personaggio è a terra
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // Legge l'input da tastiera in modo compatibile con Unity 6
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) moveZ += 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) moveZ -= 1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveX += 1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveX -= 1f;

        // Vettore di movimento basato sul piano XZ (vista isometrica)
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;

        // Muove il personaggio
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Ruota il personaggio verso la direzione di marcia
        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Gestione del Salto (tasto Spazio)
        bool jumpPressed = Input.GetKeyDown(KeyCode.Space);
        if (jumpPressed && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        // Applica la gravità nel tempo
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}