using UnityEngine;

public class BossLifeBar : MonoBehaviour
{
    private BossController bossController;
    private GameObject[] healthSquares;
    private int previousHealth;
    public Transform squaresContainer; // Asigna el objeto que contiene los 30 cuadritos

    void Start()
    {
        InitializeComponents();
    }

    void InitializeComponents()
    {
        // Intentar encontrar el boss si aún no está asignado
        if (bossController == null)
        {
            bossController = FindFirstObjectByType<BossController>();
            
            if (bossController == null)
            {
                Debug.LogError("BossLifeBar: No se encontró BossController en la escena");
                return;
            }
            Debug.Log("BossLifeBar: BossController encontrado. Salud máxima: " + bossController.maxHealth);
        }

        // Intentar cargar los cuadritos si aún no están cargados
        if (healthSquares == null || healthSquares.Length == 0)
        {
            if (squaresContainer == null)
            {
                Debug.LogError("BossLifeBar: No se asignó 'Squares Container' en el inspector. " +
                    "Arrastra el objeto que contiene los 30 cuadritos al campo 'Squares Container'");
                return;
            }

            if (squaresContainer.childCount == 0)
            {
                Debug.LogError("BossLifeBar: El container no tiene hijos. Asegúrate de que contiene los 30 cuadritos");
                return;
            }

            Debug.Log("BossLifeBar: Buscando cuadritos en el container: " + squaresContainer.name);
            healthSquares = new GameObject[squaresContainer.childCount];
            
            // Obtener todos los hijos directos
            for (int i = 0; i < squaresContainer.childCount; i++)
            {
                healthSquares[i] = squaresContainer.GetChild(i).gameObject;
            }
            
            Debug.Log("BossLifeBar: Se encontraron " + healthSquares.Length + " cuadritos en el container");
        }

        previousHealth = bossController.health;
        UpdateHealthBar();
    }

    void Update()
    {
        // Reintentar inicializar si falta algo
        if (bossController == null || healthSquares == null || healthSquares.Length == 0)
        {
            InitializeComponents();
            return;
        }

        // Si la salud cambió, actualizar los cuadritos
        if (bossController.health != previousHealth)
        {
            Debug.Log("BossLifeBar: Salud cambió de " + previousHealth + " a " + bossController.health);
            UpdateHealthBar();
            previousHealth = bossController.health;
        }
    }

    void UpdateHealthBar()
    {
        if (bossController == null || healthSquares == null || healthSquares.Length == 0)
        {
            Debug.LogWarning("BossLifeBar: No se puede actualizar la barra, faltan componentes");
            return;
        }

        // Calcular cuántos cuadritos deben estar activos basado en la proporción de salud
        // Esto funciona incluso si maxHealth != número de cuadritos
        float healthPercentage = (float)bossController.health / bossController.maxHealth;
        int squaresToShow = Mathf.CeilToInt(healthPercentage * healthSquares.Length);
        
        // Asegurarse de que no exceda el número de cuadritos disponibles
        squaresToShow = Mathf.Clamp(squaresToShow, 0, healthSquares.Length);

        // Mostrar/ocultar cuadritos
        int activeCubes = 0;
        for (int i = 0; i < healthSquares.Length; i++)
        {
            bool shouldBeActive = i < squaresToShow;
            
            if (healthSquares[i] != null)
            {
                healthSquares[i].SetActive(shouldBeActive);
                if (shouldBeActive) activeCubes++;
            }
        }
        
        Debug.Log("BossLifeBar: Actualizado. Salud: " + bossController.health + "/" + bossController.maxHealth + 
                  " (" + (healthPercentage * 100f).ToString("F1") + "%). Cuadritos activos: " + activeCubes + "/" + healthSquares.Length);
    }
}
