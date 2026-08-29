using UnityEngine;

public class MoveFire : MonoBehaviour
{
    [Header("Impostazioni Movimento")]
    [SerializeField] private float moveSpeed = 3f; // Velocità personalizzabile dall'Inspector

    void Update()
    {
        // Muove il fuoco in avanti lungo l'asse Z locale in base al tempo trascorso
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}