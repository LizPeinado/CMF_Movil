using UnityEngine;

public class AgarrableComponente : MonoBehaviour
{
    public string nombre_actual;

    public void colocar_en(Transform ubicacion)
    {
        transform.SetParent(ubicacion);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}

