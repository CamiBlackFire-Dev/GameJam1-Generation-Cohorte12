using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    public static bool juegoTerminado = false;

    [Header("Conexión con el Caldero")]
    public CalderoController scriptCaldero;

    [Header("Interfaz de Usuario")]
    public Slider healthSlider;
    public GameObject textoGanaste;

    [Header("Estados Visuales (Sprites)")]
    public Sprite spriteIdle;
    public Sprite spriteSurprised;
    public Sprite spriteAngry;
    public Sprite spriteDead;

    [Header("Configuracion")]
    public int maxHealth = 30;
    private int currentHealth;

    private SpriteRenderer spriteRenderer;
    private Collider bossCollider;
    private bool isAngry = false;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        bossCollider = GetComponent<Collider>();

        if (textoGanaste != null) textoGanaste.SetActive(false);
        juegoTerminado = false;

        // Empezamos con la cara normal
        spriteRenderer.sprite = spriteIdle;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth; 
            healthSlider.value = currentHealth;  
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (scriptCaldero != null)
        {
            scriptCaldero.ReaparecerPociones();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            
            StartCoroutine(ShowSurprisedFace());
        }
    }

    
    IEnumerator ShowSurprisedFace()
    {
        spriteRenderer.sprite = spriteSurprised;

        yield return new WaitForSeconds(0.5f); // Se queda sorprendido por medio segundo

        // Al terminar el medio segundo, revisa qu cara le toca ponerse
        if (currentHealth <= maxHealth / 2)
        {
            isAngry = true;
            spriteRenderer.sprite = spriteAngry;
        }
        else
        {
            spriteRenderer.sprite = spriteIdle;
        }
    }

    private void Die()
    {
        StopAllCoroutines();
        spriteRenderer.sprite = spriteDead;
        
        // CORRECCIÓN 3D: Ahora usamos Collider normal de 3D
        Collider bossCollider = GetComponent<Collider>();
        if (bossCollider != null) bossCollider.enabled = false;
        
        if (healthSlider != null) healthSlider.gameObject.SetActive(false);
        // 1. Activamos el candado de seguridad
        juegoTerminado = true;

        // 2. Mostramos el mensaje en pantalla
        if (textoGanaste != null) textoGanaste.SetActive(true);
        
        AudioManager.Instance.PauseMusic();

        // 3. CONGELAMOS EL JUEGO (Físicas, movimientos, proyectiles)
        Time.timeScale = 0f;
        StartCoroutine(EsperarYIrAlMenu());
        Debug.Log("¡Boss Derrotado!");
    }

    IEnumerator EsperarYIrAlMenu()
    {
        
        yield return new WaitForSecondsRealtime(2f);


        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("CalderoBola")) 
        {
            TakeDamage(10); // Le baja 10 de vida (3 golpes y muere si maxHealth es 30)
            Destroy(other.gameObject); // Destruye la esfera al impactar
        }
    }
}