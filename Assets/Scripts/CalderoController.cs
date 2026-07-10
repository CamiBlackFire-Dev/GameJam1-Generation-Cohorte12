using UnityEngine;

public class CalderoController : MonoBehaviour
{
    [Header("Referencias de Ataque")]
    public GameObject bolaPrefab;       // El prefab de tu "CalderoBola"
    public Transform puntoDisparo;       // Un objeto vacío que indica de dónde sale la bola

    [Header("Referencias de la Escena")]
    public GameObject[] listaPociones;   // Arrastra las 3 pociones físicas aquí

    private void OnTriggerEnter(Collider other)
    {
        // Si el jugador toca el Caldero
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventario = other.GetComponent<PlayerInventory>();

            // Verificamos si tiene las 3 pociones necesarias
            if (inventario != null && inventario.potionsCount >= 3)
            {
                DispararCaldero(inventario);
            }
        }
    }

    void DispararCaldero(PlayerInventory inventario)
    {
        // 1. Instanciamos la CalderoBola en el punto de disparo
        Vector3 posicionSpawn = puntoDisparo != null ? puntoDisparo.position : transform.position;
        Instantiate(bolaPrefab, posicionSpawn, Quaternion.identity);

        // 2. Reseteamos el inventario del jugador
        inventario.potionsCount = 0;
        inventario.ActualizarTextoUI();
    }

    // Este método lo llamará el Boss para revivir los objetos
    public void ReaparecerPociones()
    {
        foreach (GameObject pocion in listaPociones)
        {
            if (pocion != null)
            {
                pocion.SetActive(true); // Vuelven a ser visibles y activas
            }
        }
    }
}