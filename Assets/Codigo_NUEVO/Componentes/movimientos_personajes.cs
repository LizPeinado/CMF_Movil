using UnityEngine;

public class movimientos_personajes : MonoBehaviour
{

    public float velocidad_movimiento = 5.0f; //Esto cambiara dependiendo el personaje

    public delegate void cambio_estado_evento(EstadosMovimiento estado_nuevo);
    public event cambio_estado_evento hay_gente_escuchando_el_estado;
    private EstadosMovimiento estado_actual = EstadosMovimiento.quieto;

    private Rigidbody rigid_body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    
}
