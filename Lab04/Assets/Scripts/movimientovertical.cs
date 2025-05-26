using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientovertical : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento
    private Vector3 puntoInicial;
    private Vector3 destinoArriba;
    private Vector3 destinoAbajo;
    private bool yendoArriba = true;

    void Start()
    {
        puntoInicial = transform.position;
        destinoArriba = puntoInicial + new Vector3(0f, 2f, 0f);
        destinoAbajo = puntoInicial + new Vector3(0f, -1f, 0f);
    }

    void Update()
    {
        if (yendoArriba)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinoArriba, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, destinoArriba) < 0.1f)
                yendoArriba = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, destinoAbajo, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, destinoAbajo) < 0.1f)
                yendoArriba = true;
        }
    }
}
