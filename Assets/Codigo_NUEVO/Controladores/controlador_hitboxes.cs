
using UnityEngine;

public class ControladorHitboxes : MonoBehaviour
{
//Jugador o enemigo??
    public movimientos_personajes movimiento_jugador;
    public EnemigoMovimientoNuevo movimiento_enemigo;

//HITBOXES
    public GameObject hitbox_cuerpo;
    public GameObject hitbox_cabeza;
    public GameObject hitbox_baja;

    void Start()
    {
        if(movimiento_jugador == null)
        {
            movimiento_jugador = GetComponent<movimientos_personajes>();
        }

        if(movimiento_enemigo == null)
        {
            movimiento_enemigo = GetComponent<EnemigoMovimientoNuevo>();
        }
    }

    void Update()
    {
        bool esta_agachado = false;

        if(movimiento_jugador != null)
        {
            esta_agachado = movimiento_jugador.esta_agachado;
        }
        else if(movimiento_enemigo != null)
        {
            esta_agachado = movimiento_enemigo.esta_agachado;
        }

        hitbox_cuerpo.SetActive(!esta_agachado);
        hitbox_cabeza.SetActive(true);
        hitbox_baja.SetActive(true);
    }
}
