using UnityEngine;

public class ControladorDefensa : MonoBehaviour
{
    public bool esta_defendiendo = false;
    public bool defensa_agachada = false;
    public bool defensa_rota = false;

    public void activar_defensa(bool agachado)
    {
        esta_defendiendo = true;
        defensa_agachada = agachado;
        defensa_rota = false;

        Debug.Log("DEFENSA ACTIVADA");
    }

    public void quitar_defensa()
    {
        esta_defendiendo = false;
        defensa_agachada = false;

        Debug.Log("DEFENSA DESACTIVADA");
    }

    public void romper_defensa()
    {
        esta_defendiendo = false;
        defensa_agachada = false;
        defensa_rota = true;

        Debug.Log("DEFENSA ROTA POR UN GOLPE");
    }
}


