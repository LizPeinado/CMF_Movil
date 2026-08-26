using System.Collections;
using UnityEngine;

public class EnemigoAtaquesNuevo : MonoBehaviour
{
    public bool puede_atacar = false;
    public bool solo_ataque_debil = false;

    public GolpesManos puño_izquierdo;
    public GolpesManos puño_derecho;

    private Animator animator;

    private bool siguiente_golpe_izquierdo = true;

    private float tiempo_transcurrido = 0f;
    public float tiempo_siguiente_ataque = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!puede_atacar)
        {
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
        if(siguiente_golpe_izquierdo)
        {
            animator.SetTrigger("Golpe2");
        }
        else
        {
            animator.SetTrigger("Golpe3");
        }

        siguiente_golpe_izquierdo = !siguiente_golpe_izquierdo;
        StartCoroutine(activar_ataque(puño_izquierdo, puño_derecho, 10, 0.2f));
    }

    IEnumerator activar_ataque(GolpesManos golpe1, GolpesManos golpe2, int daño, float duracion)
    {
        golpe1.daño = daño;
        golpe2.daño = daño;

        golpe1.ataque_activo = true;
        golpe2.ataque_activo = true;

        yield return new WaitForSeconds(duracion);

        golpe1.ataque_activo = false;
        golpe2.ataque_activo = false;
    }
}
