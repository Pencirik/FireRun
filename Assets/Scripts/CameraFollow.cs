using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Riferimento al Giocatore")]
    [SerializeField] private Transform player; // Trascina qui il Player dall'Hierarchy

    [Header("Offset e Distanze")]
    [SerializeField] private float yOffset = 10f; // La Y del player + 10
    [SerializeField] private float zOffset = -10f; // La Z del player - 10 (distanza fissa)
    [SerializeField] private float smoothSpeed = 5f; // Velocità di movimento della telecamera

    void LateUpdate()
    {
        if (player == null) return;

        // Imposta X fissa a 0, Y calcolata sul player, Z calcolata sul player con offset
        float targetX = 0f;
        float targetY = player.position.y + yOffset;
        float targetZ = player.position.z + zOffset;

        Vector3 targetPosition = new Vector3(targetX, targetY, targetZ);

        // Muove la telecamera in modo fluido verso la posizione desiderata
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.unscaledDeltaTime);
    }
}