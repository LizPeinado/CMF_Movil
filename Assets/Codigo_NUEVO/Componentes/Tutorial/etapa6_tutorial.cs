
using System.Collections;
using TMPro;
using UnityEngine;

public class Etapa6Tutorial : MonoBehaviour
{
    private ControladorEtapas controlador_etapas;
    private ControladorJugador jugador;
    private SistemaInventarioJugador inventario;
    public GameObject objeto_tutorial;
    public Transform punto_aparicion;

    public float altura_aparicion = 5f;

    public float tiempo_espera = 2f;

    private GameObject objeto_creado;

    private bool etapa_iniciada = false;
    private bool objeto_aparecio = false;
    private bool etapa_completada = false;

    void Start()
    {
        controlador_etapas = FindFirstObjectByType<ControladorEtapas>();
        jugador = FindFirstObjectByType<ControladorJugador>();

        if(jugador != null)
        {
            inventario = jugador.GetComponent<SistemaInventarioJugador>();

            if(inventario != null)
            {
                inventario.puede_usar_objeto = false;
                inventario.objeto_utilizado = false;
            }
        }
    }

    void Update()
    {
        if(controlador_etapas.etapa_actual != EtapasTutorial.usar_item)
        {
            return;
        }

        if(!etapa_iniciada)
        {
            etapa_iniciada = true;
            dialogos_etapa(controlador_etapas.caja_dialogos);
        }

        // Esperar a que se presione Entendido.
        if(controlador_etapas.esta_en_pausa)
        {
            return;
        }

        // Crear el objeto una sola vez.
        if(!objeto_aparecio)
        {
            objeto_aparecio = true;
            StartCoroutine(hacer_caer_objeto());
        }

        // Comprobar si se utilizo el objeto
        // y si el jugador ya regreso.
        if(inventario != null && inventario.objeto_utilizado && jugador != null && jugador.gameObject.activeInHierarchy && !etapa_completada)
        {
            etapa_completada = true;
            StartCoroutine(esperar_para_siguiente_etapa());
        }
    }

    void dialogos_etapa(TMP_Text cajita)
    {
        cajita.text = "Ahora vamos a utilizar un objeto. Recoge el objeto que aparecera en el escenario (agachate) y realiza cualquier ataque para utilizarlo.";
    }

    IEnumerator hacer_caer_objeto()
    {
        if(objeto_tutorial == null || punto_aparicion == null)
        {
            Debug.LogError("FALTA ASIGNAR EL OBJETO O EL PUNTO DE APARICION");
            yield break;
        }

        Vector3 posicion = punto_aparicion.position + Vector3.up * altura_aparicion;

        objeto_creado = Instantiate(objeto_tutorial, posicion, punto_aparicion.rotation);

        DetectorCaidaItem detector = objeto_creado.GetComponent<DetectorCaidaItem>();

        if(detector == null)
        {
            Debug.LogError("EL OBJETO NECESITA DetectorCaidaItem");
            yield break;
        }

        // Esperar hasta que toque el suelo.
        while(!detector.toco_suelo)
        {
            yield return null;
        }

        Debug.Log("OBJETO LISTO PARA RECOGER");

        inventario.puede_usar_objeto = true;
    }

    IEnumerator esperar_para_siguiente_etapa()
    {
        controlador_etapas.caja_dialogos.text = "yAy";
        //Debug.Log("EL JUGADOR REGRESO DESPUES DE USAR EL OBJETO");

        // Esperar para que se pueda observar la secuencia.
        yield return new WaitForSecondsRealtime(tiempo_espera);

        Debug.Log("ETAPA COMPLETADA");

        inventario.puede_usar_objeto = false;

        controlador_etapas.cambiar_etapa(
            EtapasTutorial.ataque_especial
        );
    }
}


