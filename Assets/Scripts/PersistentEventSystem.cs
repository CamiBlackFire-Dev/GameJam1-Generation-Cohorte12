using UnityEngine;
using UnityEngine.EventSystems;

public class PersistentEventSystem : MonoBehaviour
{
    private void Awake()
    {
        // Buscamos todos los EventSystems en la escena sin ordenar (más rápido)
        EventSystem[] eventSystems = FindObjectsByType<EventSystem>(FindObjectsSortMode.None);
        
        // Si ya existe más de uno, destruimos este duplicado
        if (eventSystems.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        
        // Hacemos que este EventSystem persista entre escenas
        DontDestroyOnLoad(gameObject);
    }
}