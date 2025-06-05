using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public List<Camera> cameras = new List<Camera>();
    private int currentIndex = 0;

    void Start()
    {
        // Asegurarse de que solo la cámara actual esté activa al inicio
        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].gameObject.SetActive(i == currentIndex);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Desactivar la cámara actual
            cameras[currentIndex].gameObject.SetActive(false);

            // Cambiar a la siguiente
            currentIndex = (currentIndex + 1) % cameras.Count;

            // Activar la nueva cámara
            cameras[currentIndex].gameObject.SetActive(true);
        }
    }
}
