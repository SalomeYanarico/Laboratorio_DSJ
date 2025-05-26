using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoHor : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento
    private Vector3 puntoInicial;
    private Vector3 destinoDerecha;
    private Vector3 destinoIzquierda;
    private bool yendoDerecha = true;

    void Start()
    {
        puntoInicial = transform.position;
        destinoDerecha = puntoInicial + new Vector3(2f, 0f, 0f);
        destinoIzquierda = puntoInicial + new Vector3(-1f, 0f, 0f);
    }

    void Update()
    {
        if (yendoDerecha)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinoDerecha, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, destinoDerecha) < 0.1f)
                yendoDerecha = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, destinoIzquierda, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, destinoIzquierda) < 0.1f)
                yendoDerecha = true;
        }
    }
}


