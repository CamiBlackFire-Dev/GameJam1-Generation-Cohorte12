
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header ("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float turnSpeed = 10f;

    [SerializeField] private Animator animator;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 7f;

    
    private Rigidbody rb;
    private bool isGrounded = true;
    
    private Vector2 moveInput;
    private float normalSpeed;
    private bool shield;


    void Start()
    {
        normalSpeed = speed;
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
    public void ActivateSpeed(float extraSpeed, float time)
    {
        speed = normalSpeed + extraSpeed;
        Invoke("ResetSpeed", time);

    }

    void ResetSpeed()
    {
        speed = normalSpeed;  
    }

    public void ActivateShield(float time)
    {
        shield = true;
        Invoke("DisableShield", time);

    }
    void DisableShield()
    {

        shield = false;
    
    }

    public bool HasShield()
    {

        return shield;
    
    }

}
