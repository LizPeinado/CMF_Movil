using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControladorAtaques_ : MonoBehaviour
{
    public GolpesManos puño_izquierdo;
    public GolpesManos puño_derecho;

    public GolpesManos pie_izquierdo;
    public GolpesManos pie_derecho;

    private Animator animator;
    private bool siguiente_golpe_izquierdo = true;

    void hacer_ataque_debil(InputAction.CallbackContext _)
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
        StartCoroutine(activar_ataque(puño_izquierdo,puño_derecho,10,0.2f));
    }

    void hacer_ataque_medio(InputAction.CallbackContext _)
    {
        animator.SetTrigger("AtaqueMedio");
        StartCoroutine(activar_ataque(puño_izquierdo,puño_derecho,50,0.2f));
    }

    void hacer_ataque_fuerte(InputAction.CallbackContext _)
    {
        animator.SetTrigger("AtaqueFuerte");
        StartCoroutine(activar_ataque(pie_izquierdo,pie_derecho,100,0.35f));
    }

    IEnumerator activar_ataque(GolpesManos golpe1,GolpesManos golpe2,int daño,float duracion)
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
