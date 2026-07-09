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
    [SerializeField] private Animator animator;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 7f;
    private Rigidbody rb;
    private bool isGrounded = true;
    
    private Vector2 moveInput;
    private float lastShoot;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Jump()
    {
        isGrounded = false;
        animator.SetTrigger("Jump");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void Update()
    {
        HandleMovement();

        // Disparar con la barra espaciadora
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
        if(Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            Jump();
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

        // Animacion del personaje
        animator.SetFloat("Speed", movement.magnitude);
        
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
        if (Time.time - lastShoot < shootColdown) 
        return;
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }


    }
}
