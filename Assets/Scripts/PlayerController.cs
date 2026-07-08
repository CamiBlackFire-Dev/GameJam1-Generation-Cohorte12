using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header ("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float turnSpeed = 10f;

    [Header ("Shoot")]
    
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float shootColdown = 0.5f;
    [SerializeField] private Transform shootPoint;
    
    private Vector2 moveInput;
    private float lastShoot;
    
    void Update()
    {
        HandleMovement();

        // Disparar con la barra espaciadora
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Shoot();
        }
        
       
    void HandleMovement()
    {
            
    float horizontal = moveInput.x;
    float vertical = moveInput.y;
    
     // Crear movimiento base
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        
        // Rotar 45 grados para vista isométrica
        movement = Quaternion.Euler(0, 45, 0) * movement;
        
        // Normalizar para que la diagonal no sea más rápida
        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }
        
        // Mover al jugador
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
        
        // Rotar hacia donde se mueve
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }
    }
    
    private void Shoot()
    {
        // Verificar el cooldown del disparo
        if (Time.time - lastShoot < shootColdown) return;
            {
            // Crear el proyectil
            Instantiate(projectilePrefab, shootPoint.position, transform.rotation);

            lastShoot = Time.time;

            }



    }
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}