using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [Header("Velocidad de avance")]
    public float speed = 5f;

    [Header("Tipo de movimiento")]
    public bool usePhysics = true; 

    private Rigidbody rb;

    void Start()
    {
        if (usePhysics)
        {
            
            rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody>();
                rb.useGravity = false;    
                rb.constraints = RigidbodyConstraints.FreezeRotation; 
            }
        }
    }

    void FixedUpdate()
    {
        
        if (usePhysics && rb != null)
        {
            
            Vector3 velocity = transform.forward * speed;
            rb.linearVelocity = velocity;
        }
    }

    void Update()
    {
        
        if (!usePhysics)
        {
            // Movimiento hacia adelante usando Translate
            // Time.deltaTime se multiplica por Time.timeScale, así que si pausamos (timeScale=0) se detiene
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}