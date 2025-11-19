using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [Header("Objetivo a Seguir")]
    public Transform playerTarget; // Aquí arrastraremos al personaje

    [Header("Configuración de Suavizado")]
    [Range(0, 1)]
    public float smoothSpeed = 0.125f; // Qué tan suave sigue la cámara (Vital para Parallax)
    public Vector3 offset = new Vector3(0, 1, -10); // Ajuste de posición

    // Usamos LateUpdate para mover la cámara DESPUÉS de que el jugador se haya movido
    // Esto elimina los temblores y hace que el Parallax se vea fluido.
    void LateUpdate()
    {
        if (playerTarget != null)
        {
            // 1. Calculamos dónde debería estar la cámara
            Vector3 desiredPosition = playerTarget.position + offset;

            // 2. Usamos Lerp para movernos suavemente hacia allí
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // 3. Aplicamos la posición asegurando que Z sea -10 (para ver el 2D)
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10f);
        }
    }
}