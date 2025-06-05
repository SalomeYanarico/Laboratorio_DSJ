using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimiento : MonoBehaviour
{
    public GameObject[] waypoints;
    int currentWP = 0;

    public float speed = 10.0f;
    public float rotSpeed = 10.0f;

    private bool detener = false;

    void Update()
    {
        if (detener) return;

        if (Vector3.Distance(transform.position, waypoints[currentWP].transform.position) < 3)
        {
            // Si llegamos al último waypoint llamado "arco", nos detenemos
            if (currentWP == waypoints.Length - 1 && waypoints[currentWP].name == "f1")
            {
                detener = true;
                return;
            }
            else if (currentWP == waypoints.Length - 1 && waypoints[currentWP].name == "f2")
            {
                detener = true;
                return;
            }
            else if (currentWP == waypoints.Length - 1 && waypoints[currentWP].name == "f3")
            {
                detener = true;
                return;
            }
            else if (currentWP == waypoints.Length - 1 && waypoints[currentWP].name == "f4")
            {
                detener = true;
                return;
            }


            currentWP++;
        }

        if (currentWP >= waypoints.Length)
            return;

        Quaternion lookatWP = Quaternion.LookRotation(waypoints[currentWP].transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookatWP, rotSpeed * Time.deltaTime);

        // Fijar rotación X en -89.68
        Vector3 fixedRotation = transform.eulerAngles;
        fixedRotation.x = -89.68f;
        transform.eulerAngles = fixedRotation;

        // Mover solo en XZ
        Vector3 targetPos = waypoints[currentWP].transform.position;
        Vector3 moveDir = targetPos - transform.position;
        moveDir.y = 0;
        moveDir.Normalize();

        transform.position += moveDir * speed * Time.deltaTime;
    }
}
