using System;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    [SerializeField] private float speedBoost = 3f;
    [SerializeField] private float duration = 5f;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.ActivateSpeed(speedBoost, duration);

            Destroy(gameObject);

        }
    }
}
