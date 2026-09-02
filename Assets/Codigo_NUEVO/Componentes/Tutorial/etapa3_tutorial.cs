using UnityEngine;

public class Etapa3Tutorial : MonoBehaviour
{

    //Agregar regenerar la vida del enemigo despues de cada golpe

    private ControladorJugador jugador;
    private ControladorEtapas controlador_etapas;

    private bool golpe_debil_realizado = false;
    private bool golpe_medio_realizado = false;
    private bool golpe_fuerte_realizado = false;

    private bool etapa_completada = false;

    void Start()
    {
        jugador = FindFirstObjectByType<ControladorJugador>();
        controlador_etapas = FindFirstObjectByType<ControladorEtapas>();
    }

    void Update()
    {
        if(controlador_etapas.etapa_actual != EtapasTutorial.ataques)
        {
            return;
        }

        if(jugador == null)
        {
            return;
        }

        comprobar_golpe_debil();
        comprobar_golpe_medio();
        comprobar_golpe_fuerte();

        comprobar_etapa();
    }

    void comprobar_golpe_debil()
    {
        if(jugador.golpe_debil_realizado)
        {
            if(!golpe_debil_realizado)
            {
                golpe_debil_realizado = true;
                Debug.Log("EL JUGADOR HIZO GOLPE DEBIL");
            }
        }
    }

    void comprobar_golpe_medio()
    {
        if(jugador.golpe_medio_realizado)
        {
            if(!golpe_medio_realizado)
            {
                golpe_medio_realizado = true;
                Debug.Log("EL JUGADOR HIZO GOLPE MEDIO");
            }
        }
    }

    void comprobar_golpe_fuerte()
    {
        if(jugador.golpe_fuerte_realizado)
        {
            if(!golpe_fuerte_realizado)
            {
                golpe_fuerte_realizado = true;
                Debug.Log("EL JUGADOR HIZO GOLPE FUERTE");
            }
        }
    }

    void comprobar_etapa()
    {
        if(golpe_debil_realizado && golpe_medio_realizado && golpe_fuerte_realizado && !etapa_completada)
        {
            etapa_completada = true;

            Debug.Log("ETAPA 3 COMPLETADA");

            controlador_etapas.cambiar_etapa(
                EtapasTutorial.defender
            );
        }
    }
}
