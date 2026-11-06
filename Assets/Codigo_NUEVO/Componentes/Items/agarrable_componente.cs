
using UnityEngine;

public class AgarrableComponente : MonoBehaviour
{
    public string nombre_actual;
    public bool puede_recogerse = true;

    public void colocar_en(Transform ubicacion)
    {
        // Quitar la fisica cuando se recoge el objeto.
        Rigidbody fisica = GetComponent<Rigidbody>();

        if(fisica != null)
        {
            Destroy(fisica);
        }

        // Colocar el objeto en la mano.
        transform.SetParent(ubicacion);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}


