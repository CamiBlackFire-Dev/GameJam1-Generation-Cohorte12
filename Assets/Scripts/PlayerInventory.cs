using UnityEngine;
using TMPro; // Obligatorio para controlar TextMeshPro

public class PlayerInventory : MonoBehaviour
{
    [Header("Configuración de Recolección")]
    public int potionsCount = 0;
    public TextMeshProUGUI textoUI; // Arrastra tu texto de TMPro aquí

    void Start()
    {
        ActualizarTextoUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Al tocar una poción, la sumamos y la ocultamos del mapa
        if (other.CompareTag("Pociones"))
        {
            potionsCount++;
            ActualizarTextoUI();
            
            other.gameObject.SetActive(false); // Desaparece temporalmente
        }
    }

    public void ActualizarTextoUI()
    {
        if (textoUI != null)
        {
            textoUI.text = "Objetos recogidos : " + potionsCount + "/3";
        }
    }
}