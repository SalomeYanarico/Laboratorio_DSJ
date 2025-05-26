using UnityEngine;

public class Ball : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public CircleCollider2D col;

    [HideInInspector] public Vector3 pos { get { return transform.position; } }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
    }

    public void Push(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void ActivateRb()
    {
        rb.isKinematic = false;
    }

    public void DesactivateRb()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
    }

    private void Update()
    {
        CheckScorePositions();
    }

    private void CheckScorePositions()
    {
        Vector3 position = transform.position;

        if (Vector3.Distance(position, new Vector3(0.322099f, -1.243334f, 0)) < 0.1f)
        {
            GameManager.Instance.AddScore(1);
            RegenerateBall();
        }
        else if (Vector3.Distance(position, new Vector3(-9.16f, -0.9f, 0)) < 0.1f)
        {
            GameManager.Instance.AddScore(1);
            RegenerateBall();
        }
        else if (Vector3.Distance(position, new Vector3(10.15f, 1.11f, 0)) < 0.1f)
        {
            GameManager.Instance.AddScore(1);
            RegenerateBall();
        }
		else if (Vector3.Distance(position, new Vector3(10.18f, 1.01f, 0)) < 0.1f)
        {
            GameManager.Instance.AddScore(1);
            RegenerateBall();
        }
		else if (Vector3.Distance(position, new Vector3(6.48f, -1.33f, 0)) < 0.1f)
        {
            GameManager.Instance.AddScore(1);
            RegenerateBall();
        }
    }

    private void RegenerateBall()
    {
        transform.position = new Vector3(-1.534f, 0.809f, 0);
        ActivateRb(); 
    }
}
