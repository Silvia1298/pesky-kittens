using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    private BossController bossController;
    private Image healthBarImage;
    private Text healthText; // Opcional: muestra números
    private Canvas canvas;

    void Start()
    {
        // Encontrar el boss en la escena
        bossController = FindObjectOfType<BossController>();
        
        if (bossController == null)
        {
            Debug.LogError("BossHealthBar: No se encontró BossController en la escena");
            return;
        }

        // Obtener componentes de UI
        healthBarImage = GetComponent<Image>();
        
        // Intentar encontrar el texto de salud si existe
        Transform textTransform = transform.Find("HealthText");
        if (textTransform != null)
            healthText = textTransform.GetComponent<Text>();

        // Intentar encontrar el canvas
        canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
            canvas.enabled = true;
    }

    void Update()
    {
        if (bossController == null) return;

        // Actualizar barra de vida
        if (healthBarImage != null)
        {
            float healthPercent = (float)bossController.health / bossController.maxHealth;
            healthBarImage.fillAmount = healthPercent;
        }

        // Actualizar texto de salud si existe
        if (healthText != null)
        {
            healthText.text = bossController.health + " / " + bossController.maxHealth;
        }
    }

    public void HideBossHealthBar()
    {
        if (canvas != null)
            canvas.enabled = false;
    }
}
