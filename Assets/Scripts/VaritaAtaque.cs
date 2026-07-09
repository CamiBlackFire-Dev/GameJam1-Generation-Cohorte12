using UnityEngine;
using UnityEngine.InputSystem; // Requerido
public class VaritaAtaque : MonoBehaviour
{
   [Header("Referencias")]
    [Tooltip("La punta de la varita de donde sale el rayo")]
    public Transform puntaVarita;
    
    [Tooltip("Arrastra aquí el objeto con el Line Renderer (RayoVisual)")]
    public LineRenderer rayoVisual;

    [Header("Ajustes del Rayo")]
    public float duracionRayoVisble = 0.1f; // Cuánto tiempo se queda dibujado en pantalla

    private Camera camaraPrincipal;

    void Awake()
    {
        camaraPrincipal = Camera.main;

        if (rayoVisual != null)
        {
            rayoVisual.enabled = false; // El rayo inicia apagado
            rayoVisual.positionCount = 2; // Solo necesita dos puntos: inicio y fin
        }
    }

    void Update()
    {
        // Detecta el clic izquierdo con el Nuevo Input System
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            DispararRayo();
        }
    }

    void DispararRayo()
{
    if (rayoVisual == null || puntaVarita == null || camaraPrincipal == null)
    {
        Debug.LogWarning("Faltan referencias en el Inspector para el rayo.");
        return;
    }

    Vector2 posicionMouse = Mouse.current.position.ReadValue();
    Ray rayoDesdeCamara = camaraPrincipal.ScreenPointToRay(posicionMouse);

    // AHGy: Creamos el plano horizontal usando la altura exacta (Y) de la punta de la varita
    Plane planoSuelo = new Plane(Vector3.up, new Vector3(0, puntaVarita.position.y, 0));
    float distanciaAlSuelo;

    if (planoSuelo.Raycast(rayoDesdeCamara, out distanciaAlSuelo))
    {
        Vector3 puntoFinalRayo = rayoDesdeCamara.GetPoint(distanciaAlSuelo);

        StopAllCoroutines(); 
        StartCoroutine(MostrarRayoTemporal(puntaVarita.position, puntoFinalRayo));

        Debug.Log("¡Rayo Recto Disparado a la altura de la varita!");
    }
}

    // Corrutina para encender el Line Renderer y apagarlo tras una fracción de segundo
    System.Collections.IEnumerator MostrarRayoTemporal(Vector3 inicio, Vector3 fin)
    {
        rayoVisual.enabled = true;
        
        rayoVisual.SetPosition(0, inicio); // Origen en la punta de la varita
        rayoVisual.SetPosition(1, fin);    // Destino en el suelo donde hiciste clic

        yield return new WaitForSeconds(duracionRayoVisble);
        
        rayoVisual.enabled = false;
    }

}
