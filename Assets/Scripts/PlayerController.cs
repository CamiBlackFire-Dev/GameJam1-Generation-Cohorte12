using UnityEngine;
using UnityEngine.InputSystem; // Requerido para el nuevo Input System

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    private float moveSpeed = 8f;
    public ParticleSystem particulaMovimiento;

    private Rigidbody rb;
    private Vector2 moveInput;
    private Vector3 movementInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Si hay una referencia asignada a las partículas
        if (particulaMovimiento != null)
        {
            // Si la magnitud del input es mayor a cero, significa que estamos presionando teclas
            if (moveInput.sqrMagnitude > 0.05f)
            {
                // Si no se están reproduciendo, las encendemos
                if (!particulaMovimiento.isPlaying)
                {
                    particulaMovimiento.Play();
                }
            }
            else
            {
                // Si dejamos de presionar teclas y siguen activas, las apagamos de forma suave
                if (particulaMovimiento.isPlaying)
                {
                    // StopEmitting hace que dejen de salir estrellas nuevas, pero las que ya nacieron mueren de forma natural
                    particulaMovimiento.Stop(false, ParticleSystemStopBehavior.StopEmitting);
                }
            }
        }
    }

    private void particulaMover()
    {
        particulaMovimiento.Play();
    }

    // Esta función la llama automáticamente el componente 'Player Input'
    // Recuerda configurar el Behavior en "Send Messages" en el Inspector
    public void OnMove(InputValue value)
    {
        // Guardamos el input (WASD, flechas o joystick)
        moveInput = value.Get<Vector2>();
        
        // Convertimos el Vector2 de la pantalla (X, Y) al espacio 3D (X, 0, Z)
        movementInput = new Vector3(moveInput.x, 0f, moveInput.y);
        particulaMover();
    }

    private void FixedUpdate()
    {
        // Normalizamos para que el movimiento diagonal no sea más rápido
        Vector3 finalMovement = movementInput.normalized * moveSpeed;

        // Movemos el Rigidbody de forma física
        rb.MovePosition(rb.position + finalMovement * Time.fixedDeltaTime);
    }
}