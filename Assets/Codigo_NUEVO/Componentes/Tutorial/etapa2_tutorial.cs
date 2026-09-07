using TMPro;
using UnityEngine;

public class Etapa2Tutorial : MonoBehaviour
{
    private bool etapa_iniciada = false;
    private ControladorJugador jugador;
    private ControladorEtapas controlador_etapas;

    private bool salto_realizado = false;
    private bool agacharse_realizado = false;

    private bool etapa_completada = false;

    void Start()
    {
        jugador = FindFirstObjectByType<ControladorJugador>();
        controlador_etapas = FindFirstObjectByType<ControladorEtapas>();
        
    }

    void Update()
    {


        if(controlador_etapas.etapa_actual != EtapasTutorial.saltar_agacharse)
        {
            return;
        }

        if(jugador == null)
        {
          return;  
        }
        if(!etapa_iniciada)
        {
            etapa_iniciada = true;
            controlador_etapas.activar_pausa();
            dialogos_etapa2(controlador_etapas.caja_dialogos);
           
            
        }
         
        comprobar_salto();
        comprobar_agacharse();
        comprobar_etapa();
    }

    void iniciar_etapa()
    {
        jugador.puede_atacar = false;
    }

    void comprobar_salto()
    {
        if(jugador.esta_saltando)
        {
            if(!salto_realizado)
            {
                salto_realizado = true;
                Debug.Log("EL JUGADOR SALTO");
            }
        }
    }

    void comprobar_agacharse()
    {
        if(jugador.esta_agachado)
        {
            if(!agacharse_realizado)
            {
                agacharse_realizado = true;
                Debug.Log("EL JUGADOR SE AGACHO");
            }
        }
    }

    void comprobar_etapa()
    {
        if(salto_realizado && agacharse_realizado && !etapa_completada)
        {
            etapa_completada = true;
            Debug.Log("ETAPA 2 COMPLETADA");

            //controlador_etapas.cambiar_etapa(EtapasTutorial.ataques);

            controlador_etapas.cambiar_etapa(EtapasTutorial.ataques);
        }
    }

    void dialogos_etapa2(TMP_Text cajita){
        cajita.text = "Estamos en la etapa 2 chavales. saltale saltale (y agachate)";
        if(controlador_etapas.esta_en_pausa == false)
        {
            controlador_etapas.cambiar_etapa(EtapasTutorial.saltar_agacharse);
            iniciar_etapa();
        }
    }
}