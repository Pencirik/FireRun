using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Riferimento al Giocatore")]
    [SerializeField] private Transform player;

    [Header("Offset e Distanze")]
    [SerializeField] private float yOffset = 10f;
    [SerializeField] private float zOffset = -10f;
    [SerializeField] private float smoothSpeed = 5f; // Velocità di movimento della telecamera

    void LateUpdate()
    {
        if (player == null) return;

        float targetX = 0f;
        float targetY = player.position.y + yOffset;
        float targetZ = player.position.z + zOffset;

        Vector3 targetPosition = new Vector3(targetX, targetY, targetZ);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.unscaledDeltaTime);
    }
}