
using System.Collections;
using UnityEngine;

public class ControladorAnimacionObjeto : MonoBehaviour
{
    private GameObject jugador;
    private Animator animator;

    public float duracion_si_no_hay_animator = 2.0f;

    public void iniciar(GameObject jugador_a_desactivar)
    {
        jugador = jugador_a_desactivar;

        animator = GetComponentInChildren<Animator>();

        jugador.SetActive(false);

        StartCoroutine(esperar_animacion());
    }

    IEnumerator esperar_animacion()
    {
        // Dar tiempo para que el Animator inicie su estado.
        yield return null;

        float duracion = duracion_si_no_hay_animator;

        if(animator != null)
        {
            AnimatorStateInfo estado = animator.GetCurrentAnimatorStateInfo(0);

            if(estado.length > 0)
            {
                duracion = estado.length;
            }
        }

        yield return new WaitForSeconds(duracion);

        // Reactivar al jugador.
        jugador.SetActive(true);

        // Destruir el modelo animado.
        Destroy(gameObject);
    }
}
