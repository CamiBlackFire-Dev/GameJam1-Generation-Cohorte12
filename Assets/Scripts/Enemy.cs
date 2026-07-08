using UnityEngine;

public class Enemy : MonoBehaviour
{
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
    
    // Detectar colisión con Box Collider
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡El enemigo chocó con el jugador!");
            // Aquí puedes poner: Destroy(gameObject) o lo que quieras
        }
    }
}