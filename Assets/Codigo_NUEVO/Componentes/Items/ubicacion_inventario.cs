
using UnityEngine;

public class UbicacionInventario : MonoBehaviour
{
    public bool ocupada = false;
    public AgarrableComponente objeto_agarrado;

    public bool agarrar(AgarrableComponente objeto)
    {
        if(ocupada || objeto == null)
        {
            return false;
        }

        objeto.colocar_en(transform);
        objeto_agarrado = objeto;
        ocupada = true;

        Debug.Log("OBJETO COLOCADO EN LA MANO: " + objeto.name);
        return true;
    }

    public AgarrableComponente liberar()
    {
        AgarrableComponente objeto = objeto_agarrado;

        objeto_agarrado = null;
        ocupada = false;

        return objeto;
    }
}

