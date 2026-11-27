using System.Collections;
using UnityEngine;

public class GeneradorObjetosNivel : MonoBehaviour
{
    private SistemaInventarioJugador inventario;
    private ControladorJugador jugador;

    public GameObject objeto_a_generar;
    public Transform punto_aparicion;

    public float altura_aparicion = 5f;
    public float tiempo_espera = 10f;

    private GameObject objeto_creado;

    void Start()
    {
        jugador = FindFirstObjectByType<ControladorJugador>();
        if(jugador != null)
        {
            inventario = jugador.GetComponent<SistemaInventarioJugador>();
        }
        StartCoroutine(generar_objeto());
    }

    IEnumerator generar_objeto()
    {
        // Esperar 60 segundos desde que inicia el nivel.
        yield return new WaitForSeconds(tiempo_espera);

        if(objeto_a_generar == null || punto_aparicion == null)
        {
            yield break;
        }

        // Crear el objeto encima del punto de aparicion.
        Vector3 posicion = punto_aparicion.position + Vector3.up * altura_aparicion;

        objeto_creado = Instantiate(
            objeto_a_generar,
            posicion,
            punto_aparicion.rotation
        );

        DetectorCaidaItem detector = objeto_creado.GetComponent<DetectorCaidaItem>();

        if(detector == null)
        {
            yield break;
        }

        // Esperar hasta que toque el suelo.
        while(!detector.toco_suelo)
        {
            yield return null;
        }

        if(inventario != null)
        {
            inventario.puede_usar_objeto = true;
        }
    }
}