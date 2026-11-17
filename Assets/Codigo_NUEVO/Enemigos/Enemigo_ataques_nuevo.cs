using System.Collections;
using UnityEngine;

public class EnemigoAtaquesNuevo : MonoBehaviour
{
    public bool puede_atacar = false;
    public bool solo_ataque_debil = false;
    public bool ataque_en_curso = false;

    public GolpesManos puño_izquierdo;
    public GolpesManos puño_derecho;

    private Animator animator;
    private bool siguiente_golpe_izquierdo = true;

    private float tiempo_transcurrido = 0f;
    public float tiempo_siguiente_ataque = 1f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(!puede_atacar || !solo_ataque_debil)
        {
            tiempo_transcurrido = 0f;
            return;
        }

        tiempo_transcurrido += Time.deltaTime;

        if(tiempo_transcurrido >= tiempo_siguiente_ataque)
        {
            tiempo_transcurrido = 0f;
            hacer_golpe_debil();
        }
    }

    public void hacer_golpe_debil()
    {
        if(!puede_atacar || ataque_en_curso) return;

        if(siguiente_golpe_izquierdo)
            animator.SetTrigger("Golpe2");
        else
            animator.SetTrigger("Golpe3");

        siguiente_golpe_izquierdo = !siguiente_golpe_izquierdo;

        StartCoroutine(activar_ataque(10, 0.2f));
    }

    public void hacer_golpe_medio()
    {
        if(!puede_atacar || solo_ataque_debil || ataque_en_curso) return;

        animator.SetTrigger("GolpeFuerte");

        StartCoroutine(activar_ataque(30, 0.5f));
    }

    public void hacer_golpe_fuerte()
    {
        if(!puede_atacar || solo_ataque_debil || ataque_en_curso) return;

        animator.SetTrigger("PatadaFuerte");

        StartCoroutine(activar_ataque(30, 1f));
    }

    IEnumerator activar_ataque(int daño, float duracion)
    {
        ataque_en_curso = true;

        puño_izquierdo.daño = daño;
        puño_derecho.daño = daño;

        puño_izquierdo.ataque_activo = true;
        puño_derecho.ataque_activo = true;

        yield return new WaitForSeconds(duracion);

        puño_izquierdo.ataque_activo = false;
        puño_derecho.ataque_activo = false;

        ataque_en_curso = false;
    }

    public void detener_ataques()
    {
        StopAllCoroutines();

        ataque_en_curso = false;
        puede_atacar = false;
        solo_ataque_debil = false;

        puño_izquierdo.ataque_activo = false;
        puño_derecho.ataque_activo = false;
    }
}