using UnityEngine;
using TMPro;
using UnityEngine.UI; // Para CanvasScaler y GraphicRaycaster

public class GameManager : MonoBehaviour
{
    #region Singleton class: GameManager

    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SetupScoreText();
        SetupAudio();
    }

    #endregion

    public Ball ball;
    public Trajectory trajectory;
    [SerializeField] float pushForce = 4f;

    private int score = 0;
    public int Score { get { return score; } }

    private TextMeshProUGUI scoreText;

    bool isDragging = false;

    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;
    float distance;

    // === AUDIO ===
    private AudioSource audioSource;
    private AudioClip aciertoClip;

    void SetupScoreText()
    {
        // Busca un Canvas en la escena o crea uno nuevo si no existe
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasGO = new GameObject("Canvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
        }

        // Crea el objeto del texto y lo asigna al Canvas
        GameObject textGO = new GameObject("ScoreText");
        textGO.transform.SetParent(canvas.transform);

        // Configura el RectTransform para posicionar el texto arriba a la izquierda
        RectTransform rect = textGO.AddComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(0, 1);
        rect.pivot = new Vector2(0, 1);
        rect.anchoredPosition = new Vector2(80, -50);
        rect.sizeDelta = new Vector2(300, 100);

        // Añade el componente TextMeshProUGUI
        scoreText = textGO.AddComponent<TextMeshProUGUI>();
        scoreText.fontSize = 36;
        scoreText.color = Color.blue;
        scoreText.text = "Puntaje: 0";
    }

    void SetupAudio()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        // Carga el audio desde Resources/Audio/acierto.wav
        aciertoClip = Resources.Load<AudioClip>("Audio/acierto");

        if (aciertoClip == null)
        {
            Debug.LogWarning("No se pudo cargar el audio 'acierto.wav' desde Resources/Audio/");
        }

        audioSource.playOnAwake = false;
    }

    void Start()
    {
        ball.DesactivateRb();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            OnDragStart();
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            OnDragEnd();
        }

        if (isDragging)
        {
            OnDrag();
        }
    }

    void OnDragStart()
    {
        ball.DesactivateRb();
        startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        trajectory.Show();
    }

    void OnDrag()
    {
        endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;

        Debug.DrawLine(startPoint, endPoint);
        trajectory.UpdateDots(ball.pos, force);
    }

    void OnDragEnd()
    {
        ball.ActivateRb();
        ball.Push(force);
        trajectory.Hide();
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (scoreText != null)
        {
            scoreText.text = "Puntaje: " + score;
        }

        // Reproduce el sonido de acierto
        if (audioSource != null && aciertoClip != null)
        {
            audioSource.PlayOneShot(aciertoClip);
        }

        Debug.Log("Puntaje total: " + score);
    }
}
