using UnityEngine;

public class CalderoBolaController : MonoBehaviour
{
    [Header("Configuración del Vuelo")]
    public float velocidad = 10f;     
    public float alturaParabola = 5f; 

    private Vector3 puntoInicio;
    private Vector3 puntoDestino;
    private Vector3 puntoControlAlto;
    
    private float tiempoProgreso = 0f;
    private float duracionTotalVuelo = 1f;
    private bool objetivoEncontrado = false;

    void Start()
    {
        
        GameObject boss = GameObject.FindWithTag("Boss");

        if (boss != null)
        {
            puntoInicio = transform.position;
            puntoDestino = boss.transform.position;

            
            Vector3 puntoMedio = (puntoInicio + puntoDestino) / 2f;
            
            
            puntoControlAlto = puntoMedio + Vector3.up * alturaParabola;

            
            float distancia = Vector3.Distance(puntoInicio, puntoDestino);
            duracionTotalVuelo = distancia / velocidad;

            if (duracionTotalVuelo <= 0) duracionTotalVuelo = 0.5f;
            
            objetivoEncontrado = true;
        }
        else
        {
            Debug.LogWarning("¡No se encontró ningún objeto con el Tag 'Boss' en la escena!");
            Destroy(gameObject); 
        }
    }

    void Update()
    {
        if (!objetivoEncontrado) return;

        
        tiempoProgreso += Time.deltaTime / duracionTotalVuelo;

        if (tiempoProgreso <= 1f)
        {
            
            Vector3 m1 = Vector3.Lerp(puntoInicio, puntoControlAlto, tiempoProgreso);
            Vector3 m2 = Vector3.Lerp(puntoControlAlto, puntoDestino, tiempoProgreso);
            
            transform.position = Vector3.Lerp(m1, m2, tiempoProgreso);
        }
        else
        {
           
            Destroy(gameObject);
        }
    }
}