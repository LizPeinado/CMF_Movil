
using System.Collections;
using UnityEngine;
using TMPro;

public class Etapa5Tutorial : MonoBehaviour
{
    private ControladorEtapas controlador_etapas;
    private Cerebro_enemigo cerebro_enemigo;
    private ControladorDefensa defensa_enemigo;

    private bool etapa_iniciada = false;
    private bool etapa_completada = false;

    public float tiempo_alternar_defensa = 3f;
    public float tiempo_espera = 2f;

    void Start()
    {
        controlador_etapas = FindFirstObjectByType<ControladorEtapas>();
        cerebro_enemigo = FindFirstObjectByType<Cerebro_enemigo>();

        if(cerebro_enemigo != null)
        {
            defensa_enemigo = cerebro_enemigo.GetComponent<ControladorDefensa>();
        }
    }

    void Update()
    {
        if(controlador_etapas == null)
        {
            return;
        }

        if(controlador_etapas.etapa_actual != EtapasTutorial.quitar_defensa)
        {
            return;
        }

        if(!etapa_iniciada)
        {
            dialogos_etapa5(controlador_etapas.caja_dialogos);
        }

        comprobar_defensa();
    }

    void iniciar_etapa()
    {
        etapa_iniciada = true;

        Debug.Log("ETAPA 5: QUITAR LA DEFENSA");

        if(cerebro_enemigo != null)
        {
            // Comenzar defendiendo de pie
            cerebro_enemigo.defender_agachado = false;

            cerebro_enemigo.cambiar_estado(
                EstadosEnemigo.cubriendose
            );

            StartCoroutine(alternar_defensa());
        }
    }

    IEnumerator alternar_defensa()
    {
        while(!etapa_completada)
        {
            yield return new WaitForSeconds(tiempo_alternar_defensa);

            if(etapa_completada)
            {
                yield break;
            }

            // Cambiar entre defensa de pie y agachada
            cerebro_enemigo.defender_agachado =
                !cerebro_enemigo.defender_agachado;

            cerebro_enemigo.cambiar_estado(
                EstadosEnemigo.cubriendose
            );
        }
    }

    void comprobar_defensa()
    {
        if(defensa_enemigo == null || etapa_completada)
        {
            return;
        }

        if(defensa_enemigo.defensa_rota)
        {
            etapa_completada = true;

            Debug.Log("ETAPA 5 COMPLETADA");

            StopAllCoroutines();

            StartCoroutine(esperar_para_siguiente_etapa());
        }
    }

    void dialogos_etapa5(TMP_Text cajita)
    {
        cajita.text = "Ahora rompe la defensa del enemigo, Si está de pie utiliza un golpe bajo y si está agachado dale en la cabeza";

        if(controlador_etapas.esta_en_pausa == false)
        {
            controlador_etapas.activar_pausa();
            iniciar_etapa();
        }
    }

    IEnumerator esperar_para_siguiente_etapa()
    {
        controlador_etapas.caja_dialogos.text =
            "Se rompio la defensa del enemigo.";

        yield return new WaitForSecondsRealtime(tiempo_espera);

        Debug.Log("ETAPA 5 COMPLETADA");

        controlador_etapas.cambiar_etapa(
            EtapasTutorial.usar_item
        );
    }
}
