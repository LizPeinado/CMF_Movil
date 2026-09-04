using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Etapa1Tutorial : MonoBehaviour
{
    private bool etapa_iniciada = false;
    private bool etapa_completada = false;
    private ControladorEtapas controlador_etapas;
    private PlayerInput entradas_jugador;
    private InputAction movimientos;

    private ControladorJugador jugador;

    private bool avanzo = false;
    private bool retrocedio = false;

    void Start()
    {
        jugador = FindFirstObjectByType<ControladorJugador>();
        controlador_etapas = FindFirstObjectByType<ControladorEtapas>();

        if(jugador != null)
        {
            entradas_jugador = jugador.GetComponent<PlayerInput>();
            movimientos = entradas_jugador.actions.FindAction("movimiento");
        }
    }

    void Update()
    {

        if(controlador_etapas.etapa_actual == EtapasTutorial.esperando)
        {
            dialogos_etapa(controlador_etapas.caja_dialogos);
        }
        if(controlador_etapas.etapa_actual != EtapasTutorial.avanzar_retroceder)
        {
            return;
        }

        if(jugador == null){
            return;
        }

        if(!etapa_iniciada)
        {
            etapa_iniciada = true;
            iniciar_etapa();
        }

        Vector2 direccion = movimientos.ReadValue<Vector2>();
        float direccion_horizontal = direccion.y;

        if(Mathf.Abs(direccion_horizontal) > 0.1f)
        {
            bool mirando_derecha = jugador.transform.forward.x > 0;
            bool esta_retrocediendo = (mirando_derecha && direccion_horizontal < 0) ||(!mirando_derecha && direccion_horizontal > 0);

            if(esta_retrocediendo)
            {
                if(!retrocedio)
                {
                    retrocedio = true;
                    Debug.Log("EL JUGADOR RETROCEDIO");
                }
            }
            else
            {
                if(!avanzo)
                {
                    avanzo = true;
                    Debug.Log("EL JUGADOR AVANZO");
                }
            }
        }
        comprobar_etapa();
    }

    void iniciar_etapa()
    {
        jugador.puede_atacar = false;
    }

    void comprobar_etapa()
    {
        if(avanzo && retrocedio && !etapa_completada)
        {
            etapa_completada = true;

            Debug.Log("ETAPA 1 COMPLETADA");

            controlador_etapas.cambiar_etapa(EtapasTutorial.saltar_agacharse);
            controlador_etapas.activar_pausa();
        }
    }

    void dialogos_etapa(TMP_Text cajita){
        cajita.text = "Estamos en la etapa 1 chavales";
        if(controlador_etapas.esta_en_pausa == true)
        {
            controlador_etapas.quitar_pausa();
            controlador_etapas.cambiar_etapa(EtapasTutorial.avanzar_retroceder);
        }
    }
}
