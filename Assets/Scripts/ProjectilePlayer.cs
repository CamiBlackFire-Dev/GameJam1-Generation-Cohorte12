using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ProjectilePlayer : MonoBehaviour
{
    [Header("Projectile Player Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float maxLifeTime = 2f;

    private Vector3 startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Guarda la pos inicial del proyectil
        startPosition = transform.position;

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
