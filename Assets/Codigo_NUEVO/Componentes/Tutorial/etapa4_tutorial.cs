using System.Collections;
using UnityEngine;

public class Etapa4Tutorial : MonoBehaviour
{
    private ControladorEtapas controlador_etapas;
    private Cerebro_enemigo cerebro_enemigo;
    private ControladorJugador jugador;

    private bool etapa_iniciada = false;
    private bool etapa_completada = false;

    
    void Start()
    {
        controlador_etapas = FindFirstObjectByType<ControladorEtapas>();
        cerebro_enemigo = FindFirstObjectByType<Cerebro_enemigo>();
        jugador = FindFirstObjectByType<ControladorJugador>();
    }

    void Update()
    {
        if(controlador_etapas == null)
        {
            return;
        }

        if(controlador_etapas.etapa_actual != EtapasTutorial.defender)
        {
            return;
        }

        if(!etapa_iniciada)
        {
            iniciar_etapa();
        }
        comprobar_defensa();
    }

    void iniciar_etapa()
    {
        etapa_iniciada = true;
        Debug.Log("ETAPA 4: DEFENDER");

        if(cerebro_enemigo != null)
        {
            cerebro_enemigo.cambiar_estado(
                EstadosEnemigo.solo_ataque
            );
        }
    }

    void comprobar_defensa()
    {
        if(jugador == null)
        {
            return;
        }

        if(jugador.defensa_realizada && !etapa_completada)
        {
            etapa_completada = true;
            Debug.Log("ETAPA 4 COMPLETADA");
        }
    }
}
