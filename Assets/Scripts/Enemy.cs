using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Enemy Settings")]
    [SerializeField] private float speed = 3f;
    
    private Transform player;
    
    void Start()
    {
        // Buscar al jugador
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("No encontré al jugador. Ponle el Tag 'Player'");
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
            transform.LookAt(player);
        }
    }
    
    // Se realiza el cambio para verificar si el enemigo choca nuevamente con el player
    void OnTriggerEnter(Collider other) 
    
    {
        // Aca inicia si choca al jugador
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        // Ahora cuando se choca con un proyectil
        if(other.CompareTag("Projectile"))
        {
            Destroy(gameObject);
            Destroy (other.gameObject);

        }
        
    }

}