using System.Collections;
using UnityEngine;

public enum CategoriaLuchador
{
    agil,
    pesado,
    equilibrado
}

public class IA_Enemigo : MonoBehaviour
{
    public CategoriaLuchador categoria = CategoriaLuchador.pesado;

    //Configuracion del luchador pesado"
    public int salud_baja = 400;
    public float duracion_defensa = 2f;
    public float tiempo_entre_decisiones = 1f;
    public float tiempo_ataque_salud_baja = 0.6f;

    //Control de IA
    public bool ia_activa = false;

    private EnemigoMovimientoNuevo movimiento;
    private EnemigoAtaquesNuevo ataques;
    private Cerebro_enemigo cerebro;
    private Sistema_salud salud;
    private ControladorAturdimiento aturdimiento;

    private Coroutine rutina_combate;

    //Victoria"
    public Sistema_salud salud_jugador;

    private Animator animator;
    private bool victoria_activada = false;

    //Agacharse"
    public float duracion_agachado = 1.5f;

    /*void Start()
    {
        movimiento = GetComponent<EnemigoMovimientoNuevo>();
        ataques = GetComponent<EnemigoAtaquesNuevo>();
        cerebro = GetComponent<Cerebro_enemigo>();
        salud = GetComponent<Sistema_salud>();
        aturdimiento = GetComponent<ControladorAturdimiento>();
        animator = GetComponent<Animator>();
    }*/

    void Start()
    {
        movimiento = GetComponent<EnemigoMovimientoNuevo>();
        ataques = GetComponent<EnemigoAtaquesNuevo>();
        cerebro = GetComponent<Cerebro_enemigo>();
        salud = GetComponent<Sistema_salud>();
        aturdimiento = GetComponent<ControladorAturdimiento>();
        animator = GetComponent<Animator>();

        if(ia_activa)
        {
            activar_ia();
        }
    }

    void Update()
    {
        if(victoria_activada) return;

        if(salud_jugador != null && salud_jugador.muerto)
        {
            activar_victoria();
        }
    }

    public void activar_ia()
    {
        if(rutina_combate != null) return;

        ia_activa = true;

        switch(categoria)
        {
            case CategoriaLuchador.pesado:
                cerebro.cambiar_estado(EstadosEnemigo.pelea);
                rutina_combate = StartCoroutine(combate_pesado());
            break;

            case CategoriaLuchador.agil:
                cerebro.cambiar_estado(EstadosEnemigo.pelea);
                rutina_combate = StartCoroutine(combate_agil());
            break;

            case CategoriaLuchador.equilibrado:
                cerebro.cambiar_estado(EstadosEnemigo.pelea);
                rutina_combate = StartCoroutine(combate_equilibrado());
            break;
        }
    }

    public void desactivar_ia()
    {
        ia_activa = false;

        if(rutina_combate != null)
        {
            StopCoroutine(rutina_combate);
            rutina_combate = null;
        }

        cerebro.retroceder_al_defender = false;
    }

    IEnumerator combate_pesado()
    {
        while(ia_activa)
        {
            // ESPERAR SI EL ENEMIGO ESTA ATURDIDO
            if(aturdimiento != null && aturdimiento.esta_aturdido)
            {
                yield return new WaitForSeconds(0.1f);
                continue;
            }

            // ESPERAR SI ESTA EJECUTANDO UN ATAQUE
            if(ataques.ataque_en_curso)
            {
                yield return null;
                continue;
            }

            int vida_actual = salud.salud_actual;

            // CUANDO TIENE POCA SALUD ATACA MAS SEGUIDO
            if(vida_actual <= salud_baja)
            {
                cerebro.cambiar_estado(EstadosEnemigo.pelea);

                elegir_ataque();

                yield return new WaitForSeconds(tiempo_ataque_salud_baja);
                continue;
            }

            int accion = Random.Range(0,100);

            if(accion < 55)
            {
                // DEFENDERSE
                cerebro.defender_agachado = Random.Range(0,100) < 20;
                cerebro.retroceder_al_defender = true;

                cerebro.cambiar_estado(EstadosEnemigo.cubriendose);

                yield return new WaitForSeconds(duracion_defensa);

                cerebro.retroceder_al_defender = false;

                if(ia_activa && !aturdimiento.esta_aturdido)
                    cerebro.cambiar_estado(EstadosEnemigo.pelea);
            }
            else if(accion < 75)
            {
                // ATAQUE DEBIL
                cerebro.cambiar_estado(EstadosEnemigo.pelea);
                ataques.hacer_golpe_debil();
            }
            else if(accion < 86)
            {
                // ATAQUE MEDIO
                cerebro.cambiar_estado(EstadosEnemigo.pelea);
                ataques.hacer_golpe_medio();
            }
            else if(accion < 92)
            {
                // ATAQUE FUERTE
                cerebro.cambiar_estado(EstadosEnemigo.pelea);
                ataques.hacer_golpe_fuerte();
            }
            else if(accion < 97)
            {
                // AGACHARSE SIN DEFENDERSE
                cerebro.cambiar_estado(EstadosEnemigo.pelea);

                yield return StartCoroutine(agacharse_un_momento());
            }
            else
            {
                // SALTAR
                cerebro.cambiar_estado(EstadosEnemigo.pelea);
                movimiento.saltar();
            }

            yield return new WaitForSeconds(tiempo_entre_decisiones);
        }
    }

    IEnumerator combate_agil()
    {
        while(ia_activa)
        {
            Debug.Log("IA TOMANDO DECISIONES: " + categoria);
            
            if(aturdimiento != null && aturdimiento.esta_aturdido)
            {
                yield return new WaitForSeconds(0.1f);
                continue;
            }

            if(ataques.ataque_en_curso || movimiento.esta_saltando || movimiento.esta_agachado)
            {
                yield return null;
                continue;
            }

            int vida_actual = salud.salud_actual;
            int accion = Random.Range(0,100);

            if(vida_actual <= salud_baja)
            {
                // AGIL CON POCA SALUD

                if(accion < 5)
                {
                    yield return StartCoroutine(defender_un_momento());
                }
                else if(accion < 25)
                {
                    ataques.hacer_golpe_debil();
                }
                else if(accion < 60)
                {
                    ataques.hacer_golpe_medio();
                }
                else if(accion < 85)
                {
                    movimiento.saltar();
                }
                else if(accion < 90)
                {
                    yield return StartCoroutine(agacharse_un_momento());
                }
                else
                {
                    ataques.hacer_golpe_fuerte();
                }
            }
            else
            {
                // AGIL CON SALUD NORMAL

                if(accion < 5)
                {
                    yield return StartCoroutine(defender_un_momento());
                }
                else if(accion < 60)
                {
                    ataques.hacer_golpe_debil();
                }
                else if(accion < 75)
                {
                    ataques.hacer_golpe_medio();
                }
                else if(accion < 80)
                {
                    movimiento.saltar();
                }
                else if(accion < 85)
                {
                    yield return StartCoroutine(agacharse_un_momento());
                }
                else
                {
                    ataques.hacer_golpe_fuerte();
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator combate_equilibrado()
{
    while(ia_activa)
    {
        if(aturdimiento != null && aturdimiento.esta_aturdido)
        {
            yield return new WaitForSeconds(0.1f);
            continue;
        }

        if(ataques.ataque_en_curso || movimiento.esta_saltando || movimiento.esta_agachado)
        {
            yield return null;
            continue;
        }

        int accion = Random.Range(0,100);

        if(accion < 17)
        {
            yield return StartCoroutine(defender_un_momento());
        }
        else if(accion < 34)
        {
            ataques.hacer_golpe_debil();
        }
        else if(accion < 51)
        {
            ataques.hacer_golpe_medio();
        }
        else if(accion < 68)
        {
            ataques.hacer_golpe_fuerte();
        }
        else if(accion < 84)
        {
            yield return StartCoroutine(agacharse_un_momento());
        }
        else
        {
            movimiento.saltar();
        }

        yield return new WaitForSeconds(tiempo_entre_decisiones);
    }
}

    void elegir_ataque()
    {
        int ataque = Random.Range(0, 100);

        if(ataque < 40)
        {
            ataques.hacer_golpe_debil();
        }
        else if(ataque < 75)
        {
            ataques.hacer_golpe_medio();
        }
        else
        {
            ataques.hacer_golpe_fuerte();
        }
    }

    IEnumerator agacharse_un_momento()
    {
        movimiento.esta_agachado = true;
        animator.SetBool("Agachado", true);

        yield return new WaitForSeconds(duracion_agachado);

        movimiento.esta_agachado = false;
        animator.SetBool("Agachado", false);
    }
    IEnumerator defender_un_momento()
    {
        cerebro.defender_agachado = Random.Range(0,100) < 20;
        cerebro.retroceder_al_defender = true;

        cerebro.cambiar_estado(EstadosEnemigo.cubriendose);

        yield return new WaitForSeconds(duracion_defensa);

        cerebro.retroceder_al_defender = false;

        if(ia_activa && (aturdimiento == null || !aturdimiento.esta_aturdido))
        {
            cerebro.cambiar_estado(EstadosEnemigo.pelea);
        }
    }

    void activar_victoria()
    {
        victoria_activada = true;
        ia_activa = false;

        if(rutina_combate != null)
        {
            StopCoroutine(rutina_combate);
            rutina_combate = null;
        }

        movimiento.puede_moverse = false;
        movimiento.retrocediendo = false;

        ataques.detener_ataques();

        cerebro.cambiar_estado(EstadosEnemigo.quieto);

        animator.SetTrigger("Victoria");

        Debug.Log("VICTORIA: EL ENEMIGO HA GANADO");
    }
}
