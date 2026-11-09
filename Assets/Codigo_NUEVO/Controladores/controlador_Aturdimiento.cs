using System.Collections;
using UnityEngine;

public class ControladorAturdimiento : MonoBehaviour
{
    public float tiempo_aturdido = 1.5f;
    public bool esta_aturdido = false;

    private Animator animator;
    private movimientos_personajes movimiento_jugador;
    private EnemigoMovimientoNuevo movimiento_enemigo;
    private EnemigoAtaquesNuevo ataques_enemigo;

    void Start()
    {
        animator = GetComponent<Animator>();
        movimiento_jugador = GetComponent<movimientos_personajes>();
        movimiento_enemigo = GetComponent<EnemigoMovimientoNuevo>();
        ataques_enemigo = GetComponent<EnemigoAtaquesNuevo>();
    }

    public void romper_defensa()
    {
        if(esta_aturdido) return;

        StartCoroutine(aturdir_personaje());
    }

    IEnumerator aturdir_personaje()
    {
        esta_aturdido = true;

        bool movimiento_anterior = false;
        bool ataque_anterior = false;

        if(movimiento_jugador != null)
        {
            ataque_anterior = movimiento_jugador.puede_atacar;
            movimiento_jugador.puede_atacar = false;
            movimiento_jugador.cambiarEstado(EstadosMovimiento.aturdido);
        }

        if(movimiento_enemigo != null)
        {
            movimiento_anterior = movimiento_enemigo.puede_moverse;
            movimiento_enemigo.puede_moverse = false;
        }

        if(ataques_enemigo != null)
        {
            ataque_anterior = ataques_enemigo.puede_atacar;
            ataques_enemigo.puede_atacar = false;
        }

        /*if(animator != null)
            animator.SetTrigger("RomperDefensa");
*/
        if(animator != null)
        {
            Debug.Log("ANIMACION ROMPER DEFENSA ACTIVADA");
            animator.SetTrigger("RomperDefensa");
        }
        else
        {
            Debug.Log("NO SE ENCONTRO EL ANIMATOR");
        }
        yield return new WaitForSeconds(tiempo_aturdido);

        esta_aturdido = false;

        if(movimiento_jugador != null)
        {
            movimiento_jugador.puede_atacar = ataque_anterior;
            movimiento_jugador.cambiarEstado(EstadosMovimiento.quieto);
        }

        if(movimiento_enemigo != null)
            movimiento_enemigo.puede_moverse = movimiento_anterior;

        if(ataques_enemigo != null)
            ataques_enemigo.puede_atacar = ataque_anterior;
    }
}