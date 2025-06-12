using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(AIPath))] // o RichAI / AILerp
public class AIDestinationSetter : VersionedMonoBehaviour
{
    public Transform target;

    [Header("Configuración de movimiento")]
    public float velocidad = 3.5f;

    [Header("Detección de llegada")]
    public float distanciaFinal = 0.5f;

    private IAstarAI ai;
    private LineRenderer lineRenderer;
    private Seeker seeker;

    private bool gameOver = false;

    void OnEnable()
    {
        ai = GetComponent<IAstarAI>();
        seeker = GetComponent<Seeker>();
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        if (ai != null) ai.onSearchPath += Update;
    }

    void OnDisable()
    {
        if (ai != null) ai.onSearchPath -= Update;
    }

    void Update()
    {
        if (gameOver || ai == null || target == null) return;

        ai.destination = target.position;
        ai.maxSpeed = velocidad;

        DrawPath();

        // Verificar si llegó al objetivo
        float distancia = Vector3.Distance(transform.position, target.position);
        if (distancia <= distanciaFinal)
        {
            gameOver = true;
            Time.timeScale = 0f; 
            Debug.Log("GAME OVER");
        }
    }

    void DrawPath()
    {
        if (seeker != null && seeker.GetCurrentPath() != null && seeker.GetCurrentPath().vectorPath != null)
        {
            var path = seeker.GetCurrentPath().vectorPath;
            lineRenderer.positionCount = path.Count;
            lineRenderer.SetPositions(path.ToArray());
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
    }

    void OnGUI()
    {
        if (gameOver)
        {
            GUIStyle estilo = new GUIStyle(GUI.skin.label);
            estilo.fontSize = 40;
            estilo.normal.textColor = Color.red;
            estilo.alignment = TextAnchor.MiddleCenter;

            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 20, 200, 50), "GAME OVER", estilo);
        }
    }
}
