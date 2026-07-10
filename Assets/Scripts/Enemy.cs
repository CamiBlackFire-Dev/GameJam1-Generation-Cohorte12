using UnityEngine.UI;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Enemy Settings")]
    [SerializeField] private float speed = 3f;
    
    

    [Header("Health")]
    [SerializeField] private int maxHealth = 3;

    private int currentHealth;
    [SerializeField] private Slider healthBar;
    private Transform player;
    
    
    void Start()
    {
        currentHealth = maxHealth;
        
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

        
        // Buscar al jugador
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        
        if (playerObject != null)
    {
        player = playerObject.transform;
    }
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;

    if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }

    }
    
    void Update()
    {
        if (player == null) return;
        
        // Dirección hacia el jugador
        Vector3 direction = player.position - transform.position;
        direction.y = 0;
        
        // Mover al enemigo
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        
        // Mirar hacia el jugador
        if (direction != Vector3.zero)
        {   
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation * Quaternion.Euler(-90, 0f, 0f);
        }
    }
    
    // Se realiza el cambio para verificar si el enemigo choca nuevamente con el player
    void OnTriggerEnter(Collider other) 
    
    {
        // Aca inicia si choca al jugador
        if(other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if(player.HasShield())
            {
                Destroy(gameObject);

            }

        else
            {
             
             player.TakeDamage(1);
             Destroy(gameObject);

            }
        }
    }

    


}

