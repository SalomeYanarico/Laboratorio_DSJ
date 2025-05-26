using UnityEngine;

public class Cup : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisión detectada con: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager.Instance.AddScore(1);
            Debug.Log("Score incrementado: " + GameManager.Instance.Score);
            collision.gameObject.SetActive(false);
        }
    }

}
