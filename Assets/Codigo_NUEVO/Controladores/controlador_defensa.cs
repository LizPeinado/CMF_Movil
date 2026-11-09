using UnityEngine;

public class ControladorDefensa : MonoBehaviour
{
    public bool esta_defendiendo = false;
    public bool defensa_agachada = false;

    public void activar_defensa(bool agachado)
    {
        esta_defendiendo = true;
        defensa_agachada = agachado;

        Debug.Log("DEFENSA ACTIVADA");
    }

    public void quitar_defensa()
    {
        esta_defendiendo = false;
        defensa_agachada = false;

        Debug.Log("DEFENSA ROTA");
    }
}

