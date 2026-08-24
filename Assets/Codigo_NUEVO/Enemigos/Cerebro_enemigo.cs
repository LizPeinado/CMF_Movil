using UnityEngine;

public enum EstadosEnemigo{
    quieto,
    solo_ataque,
    cubriendose,
    pelea
}

public class Cerebro_enemigo : MonoBehaviour
{
    public EstadosEnemigo estado_actual = EstadosEnemigo.quieto;

    private EnemigoMovimientoNuevo movimiento;

    private EnemigoAtaquesNuevo ataques;

    private EstadosEnemigo estado_anterior;

    private void Start()
    {
        movimiento = GetComponent<EnemigoMovimientoNuevo>();
        ataques = GetComponent<EnemigoAtaquesNuevo>();
        estado_anterior = estado_actual;
        aplicar_estado();
    }

    private void Update()
    {
        if(estado_actual != estado_anterior)
        {
            estado_anterior = estado_actual;
            Debug.Log("Estado cambiado desde Inspector: " + estado_actual);
            aplicar_estado();
        }
    }

    public void cambiar_estado(EstadosEnemigo nuevo_estado)
    {
        estado_actual = nuevo_estado;

        Debug.Log("Estado del enemigo: " + estado_actual);

        aplicar_estado();
    }

    void aplicar_estado()
    {
        switch (estado_actual)
        {
            case EstadosEnemigo.quieto:
                estado_quieto();
            break;

            case EstadosEnemigo.solo_ataque:
                estado_solo_ataque();
            break;

            case EstadosEnemigo.cubriendose:
                estado_cubriendose();
            break;

            case EstadosEnemigo.pelea:
                estado_pelea();
            break;
        }
    }

    void estado_quieto()
    {
        Debug.Log("ENEMIGO: QUIETO");

        movimiento.puede_moverse = false;
        movimiento.retrocediendo = false;

        ataques.puede_atacar = false;
    }

    void estado_solo_ataque()
    {
        Debug.Log("ENEMIGO: SOLO ATAQUE");

        movimiento.puede_moverse = true;
        movimiento.retrocediendo = false;

        ataques.puede_atacar = true;
        ataques.solo_ataque_debil = true;
    }

    void estado_cubriendose()
    {
        Debug.Log("ENEMIGO: CUBRIENDOSE");

        movimiento.puede_moverse = true;
        movimiento.retrocediendo = true;

        ataques.puede_atacar = false;
    }

    void estado_pelea()
    {
        Debug.Log("ENEMIGO: PELEA");

        movimiento.puede_moverse = true;
        movimiento.retrocediendo = false;

        ataques.puede_atacar = true;
        ataques.solo_ataque_debil = false;
    }
}
