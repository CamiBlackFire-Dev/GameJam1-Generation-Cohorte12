using UnityEngine;

public class VaritaFollowPlayer : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("Arrastra aquí al Mago Cilindro")]
    public Transform jugadorTarget;

    [Header("Configuración de Posición")]
    [Tooltip("Distancia relativa al jugador (X: Lado, Y: Altura, Z: Adelante/Atrás)")]
    public Vector3 posicionRelativa = new Vector3(0.6f, 0.5f, 0.3f);

    void LateUpdate()
    {
        // Usamos LateUpdate para que la varita se mueva DESPUÉS de que el jugador ya se movió en el FixedUpdate
        if (jugadorTarget != null)
        {
            // Sigue la posición del jugador más la distancia que definas
            transform.position = jugadorTarget.position + posicionRelativa;
        }
    }
}
