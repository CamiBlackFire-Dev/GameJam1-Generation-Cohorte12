using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    [SerializeField] private float duration = 5f;

    private void OggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.ActivateShield(duration);

            Destroy(gameObject);
        }      
    }
}
