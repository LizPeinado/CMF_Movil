using UnityEngine;

public class MirarEnemigo : MonoBehaviour
{


    public Transform[] quienes_estan;
    private Transform objetivo;

    void Start()
    {
        if(quienes_estan.Length == 1)
        {
            objetivo = quienes_estan[0];
        }
        else
        {
            switch (PlayerPrefs.GetInt("personaje_seleccionado"))
            {
                case 0:
                    objetivo = quienes_estan[0];
                break;
                case 2:
                    objetivo = quienes_estan[1];
                break;
                case 3:
                    objetivo = quienes_estan[2];
                break;
                default:
                    objetivo = quienes_estan[0];
                break;
            }
        }
        
    }

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