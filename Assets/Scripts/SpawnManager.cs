
using UnityEngine;

public class SpawnManager : MonoBehaviour

{   
    [Header("Portals")]
    [SerializeField] private Transform portalLeft;
    [SerializeField] private Transform portalRight;


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
    

    void Start()
    {
        // Agregue al primer spawn 2 segundos
        nextSpawnTime = Time.time + 2f;
        
    }

    
    void Update()
    {
        CountEnemies();
        // Si no hay muchos enemigos y es el momento para spawnear
        if(currentEnemies == 0 && Time.time >= nextSpawnTime)
        {
            SpawnWave();
            nextSpawnTime = Time.time + spawnRate;

            if(enemiesWave < maxEnemiesPortals)
            {
                enemiesWave++;

            }    
        }

        
    }

    void CountEnemies()
    {
        // Se cuentan cuantos enemigos estan en la escena
        currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    void SpawnWave()
    {
        SpawnFromPortal(portalLeft);
        SpawnFromPortal(portalRight);

            
    }

    void SpawnFromPortal(Transform portal)
    {
        for(int i = 0; i< enemiesWave; i++)
        {
            Vector3 spawnPosition = portal.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

            Instantiate(enemyPrefab, spawnPosition, transform.rotation);
        }
    }

    }
        
    

