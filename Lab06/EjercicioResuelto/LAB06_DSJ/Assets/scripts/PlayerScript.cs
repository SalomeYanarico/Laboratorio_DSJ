using UnityEngine;

// Script de Unity para controlar al jugador
public class PlayerScript : MonoBehaviour
{
    public float Speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // Inicialización si es necesaria
    }

    // Update is called once per frame
    void Update()
    {
        // Movimiento hacia la izquierda
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * Speed * Time.deltaTime;
        }

        // Movimiento hacia la derecha
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * Speed * Time.deltaTime;
        }

        // Movimiento hacia arriba
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * Speed * Time.deltaTime;
        }

        // Movimiento hacia abajo
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * Speed * Time.deltaTime;
        }
    }
}
