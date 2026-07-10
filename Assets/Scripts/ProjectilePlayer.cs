using System;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ProjectilePlayer : MonoBehaviour
{
    [Header("Projectile Player Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxLifeTime = 4f;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Destruye el mismo despues de un tiempo establecido

        Destroy(gameObject, maxLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        // Mueve el proyectil hacia adelante
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
