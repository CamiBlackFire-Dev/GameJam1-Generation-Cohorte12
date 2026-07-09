using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [Header("PowerUps")]
    [SerializeField] private GameObject mushroomPrefab;
    [SerializeField] private GameObject shieldPotionPrefab;


    [Header("Spawn Area")]
    [SerializeField] private float rangeX = 12f;
    [SerializeField] private float rangeZ = 12f;
    [SerializeField] private float spawnTime = 10f;
   
    void Start()
    {
        InvokeRepeating("SpawnPowerUp", 5f, spawnTime);
    }

    // Update is called once per frame
    void SpawnPowerUp()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-rangeX, rangeX) -23.27f, 0.9f, Random.Range(-rangeZ, rangeZ));

    int randomPowerUp = Random.Range(0,2);

    if(randomPowerUp == 0)
        {
            Instantiate(mushroomPrefab, randomPosition, Quaternion.Euler(-90, 0, 0));
        }
    else
        {
            Instantiate(shieldPotionPrefab, randomPosition, Quaternion.Euler(-90, 0, 0));
        }
        
    }
}
