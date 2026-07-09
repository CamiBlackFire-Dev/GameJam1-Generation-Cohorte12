
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 3f;
    [SerializeField] private int maxEnemies = 10;

    //Cambie para que inicie cada portal con 1 enemigo
    [SerializeField] private int enemiesWave = 1;
    [SerializeField] private int maxEnemiesPortals = 5;

    private float nextSpawnTime;
    private int currentEnemies;
    // Se controla cuando van a spawnear
    private bool onSpawn = false;

    void Start()
    {
        // Agregue al primer spawn 2 segundos
        nextSpawnTime = Time.time + 2f;
        
    }

    
    void Update()
    {
        // Se cuentan los enemigos actuales
        CountEnemies();

        // Si no hay muchos enemigos y es el momento para spawnear
        if(currentEnemies == 0 && !onSpawn)
        {
            onSpawn = true;
            
            if(enemiesWave < maxEnemiesPortals)
            {
            // Aumenta los enemigos para la oleada siguiente
            enemiesWave++;

            }
            nextSpawnTime = Time.time + spawnRate;
        }

        if(onSpawn && Time.time >= nextSpawnTime && currentEnemies < maxEnemies)
        {
            SpawnWave();
        }
    }

    void CountEnemies()
    {
        // Se cuentan cuantos enemigos estan en la escena
        currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    void SpawnWave()
    {
        // Bloqueo el spawn hasta que mueran los enemigos
        onSpawn = false;


        for (int i = 0; i < enemiesWave; i++)
        {
            // Cambie un poco la posicion para que no salgan todos a la vez
           Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            // Crear enemigo para la poscioon del portal
            Instantiate(enemyPrefab, spawnPosition, transform.rotation);

        }
        
    }
}
