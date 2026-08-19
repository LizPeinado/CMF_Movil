using UnityEngine;

[RequireComponent(typeof(Animator))]

public class ControladorAnimacionJugador : MonoBehaviour{
    private Animator controlador_animacion;


    void Start(){
        controlador_animacion = GetComponent<Animator>();

        var control_de_movimientos = GetComponent<ControladorJugador>();

        control_de_movimientos.hay_gente_escuchando_el_estado += CambiarEstadoDelControladorDeMovimientos;
    }

    void CambiarEstadoDelControladorDeMovimientos(EstadosMovimiento nuevo_estado)
    {
        controlador_animacion.SetBool("Quieto",false);
        controlador_animacion.SetBool("Retrocede", false);
        controlador_animacion.SetBool("Agachado", false);
        controlador_animacion.SetBool("Defender",false);

        switch (nuevo_estado){
            case EstadosMovimiento.quieto:
                controlador_animacion.SetBool("Quieto", true);
            break;

            case EstadosMovimiento.caminando:
            break;

            case EstadosMovimiento.Retrocediendo:
                controlador_animacion.SetBool("Retrocede", true);
            break;

            case EstadosMovimiento.agachado:
                controlador_animacion.SetBool("Agachado", true);
            break;

            /*case EstadosMovimiento.defender:
                controlador_animacion.SetBool("Defender",true);
            break;*/
        }
    }
}