using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientos_personajes : MonoBehaviour
{

    public float velocidad_movimiento = 5.0f; //Esto cambiara dependiendo el personaje

    private Rigidbody rigid_body;
    public delegate void cambio_estado_evento(EstadosMovimiento estado_nuevo);
    public event cambio_estado_evento hay_gente_escuchando_el_estado;
    
    //El estado empieza en quieto
    private EstadosMovimiento estado_actual = EstadosMovimiento.quieto;

    //Variables para verificar que este saltando/agachado
    public bool esta_agachado = false;
    public bool esta_saltando = false;

    //Recibir HITBOXES dependiendo personaje
    [Header("Hitboxes")]
    public GameObject hitbox_delantera_completa;
    public GameObject hitbox_trasera_completa;

    public GameObject hitbox_delantera_agachado;
    public GameObject hitbox_trasera_agachado;

    public GameObject hitbox_cabeza;

   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    //SALTAR
    public void salta_jugador(){
        bool estamos_en_el_suelo = false;
        Ray rayo_hacia_el_suelo = new Ray(transform.position, transform.TransformDirection(Vector3.down));

        RaycastHit chocamos_con;

        if(Physics.Raycast(rayo_hacia_el_suelo, out chocamos_con, 1.1F))
        {
            if (chocamos_con.collider.CompareTag("suelo")){
                estamos_en_el_suelo = true;
            }

            if (estamos_en_el_suelo){
                rigid_body.AddForce(Vector3.up * 700f);

                esta_saltando = true;

                cambiarEstado(EstadosMovimiento.saltando);
            }
        }
    }

    //SIGUE SALTANDO?
    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("suelo")){
            esta_saltando = false;
            cambiarEstado(EstadosMovimiento.quieto);
        }       
    }

    //AGACHARSE
    public void agacharse(){
        if(esta_saltando || (estado_actual != EstadosMovimiento.quieto)){
            return;
        }
        esta_agachado = true;

        cambiarEstado(EstadosMovimiento.agachado);

        // !! Cambiar hitboxes !!
    }

    public void agacharsent(){
        if(esta_agachado){
            esta_agachado = false;
            cambiarEstado(EstadosMovimiento.quieto);
        }
    }

    public void defender(){
        if(estado_actual != EstadosMovimiento.Retrocediendo){
            return;
        }
        cambiarEstado(EstadosMovimiento.defender);
        
        // !! CAMBIAR HITBOXES
    }

    void defender_agachao()
    {
        if(estado_actual != EstadosMovimiento.agachado){
            return;
        }
        cambiarEstado(EstadosMovimiento.defender_agachao);
        // !! CAMBIAR HITBOXES
    }

    void FixedUpdate(){
        if (esta_agachado){
            return;
        }
    }

    void cambiarEstado(EstadosMovimiento estado_nuevo){
        estado_actual = estado_nuevo;

        if(hay_gente_escuchando_el_estado != null){
            hay_gente_escuchando_el_estado.Invoke(estado_nuevo);
        }
    }

}
