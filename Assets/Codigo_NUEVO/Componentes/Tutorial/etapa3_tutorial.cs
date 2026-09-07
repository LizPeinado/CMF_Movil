using System.Collections;
using UnityEngine;
using TMPro;

public class Etapa3Tutorial : MonoBehaviour
{
    private bool etapa_iniciada = false;

    private ControladorJugador jugador;
    private ControladorEtapas controlador_etapas;

    private bool golpe_debil_realizado = false;
    private bool golpe_medio_realizado = false;
    private bool golpe_fuerte_realizado = false;

    private bool etapa_completada = false;

    // REGENERAR SALUD DEL ENEMIGO
    private Sistema_salud salud_enemigo;
    private Cerebro_enemigo enemigo;
    private int salud_anterior;
    private bool regenerando = false;
    //

    void Start()
    {
        jugador = FindFirstObjectByType<ControladorJugador>();
        controlador_etapas = FindFirstObjectByType<ControladorEtapas>();

        enemigo = FindFirstObjectByType<Cerebro_enemigo>();

        if(enemigo != null)
        {
            salud_enemigo = enemigo.GetComponent<Sistema_salud>();
        }
    }

    void Update()
    {
        if(controlador_etapas == null)
        {
            return;
        }

        if(controlador_etapas.etapa_actual != EtapasTutorial.ataques)
        {
            return;
        }

        if(jugador == null)
        {
            return;
        }

        // MOSTRAR EL MENSAJE CUANDO COMIENZA LA ETAPA
        if(!etapa_iniciada)
        {
            dialogos_etapa3(controlador_etapas.caja_dialogos);

            if(controlador_etapas.esta_en_pausa)
            {
                return;
            }

            iniciar_etapa();
        }

        // MIENTRAS ESTA EN PAUSA NO HACEMOS NADA
        if(controlador_etapas.esta_en_pausa)
        {
            return;
        }

        comprobar_golpe_debil();
        comprobar_golpe_medio();
        comprobar_golpe_fuerte();

        comprobar_salud();

        comprobar_etapa();
    }

    void iniciar_etapa()
    {
        etapa_iniciada = true;

        Debug.Log("ETAPA 3: ATAQUES");

        jugador.puede_atacar = true;

        // GUARDAMOS LA SALUD CUANDO REALMENTE INICIA LA ETAPA
        if(salud_enemigo != null)
        {
            salud_anterior = salud_enemigo.salud_actual;

            Debug.Log("SALUD INICIAL DEL ENEMIGO: " + salud_anterior);
        }
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

    void comprobar_salud()
    {
        if(salud_enemigo == null)
        {
            return;
        }

        if(regenerando)
        {
            return;
        }

        if(salud_enemigo.salud_actual < salud_anterior)
        {
            Debug.Log("EL ENEMIGO RECIBIO UN GOLPE");

            regenerando = true;

            StartCoroutine(regenerar_salud());
        }
    }

    IEnumerator regenerar_salud()
    {
        // ESPERAMOS PARA QUE LA BARRA PUEDA MOSTRAR EL DAÑO
        yield return new WaitForSecondsRealtime(0.5f);

        salud_enemigo.recuperar_salud();

        salud_anterior = salud_enemigo.salud_actual;

        Debug.Log("SALUD DESPUES DE RECUPERAR: " + salud_enemigo.salud_actual);

        regenerando = false;
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

    void dialogos_etapa3(TMP_Text cajita)
    {
        if(cajita == null)
        {
            return;
        }

        cajita.text = "Estamos en la etapa 3, dale en la madre (U - I - O)";
    }
}
