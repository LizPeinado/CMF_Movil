using UnityEngine;

public class mirar_UN_enemigo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform objetivo;

    // Update is called once per frame
    void Update()
    {

        if (objetivo == null) return;

        // derecha
        if (objetivo.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        //izquierda
        else
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }
}
