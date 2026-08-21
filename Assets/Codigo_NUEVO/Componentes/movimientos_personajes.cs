using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movimientos_personajes : MonoBehaviour
{

    public delegate void cambio_estado_evento(EstadosMovimiento estado_nuevo);
    public event cambio_estado_evento hay_gente_escuchando_el_estado;
    
    //El estado empieza en quieto
    private EstadosMovimiento estado_actual = EstadosMovimiento.quieto;

    //Variables para verificar que este saltando/agachado
    public bool esta_agachado = false;
    public bool esta_saltando = false;

    //Recibir HITBOXES dependiendo personaje
    /*[Header("Hitboxes")]
    public GameObject hitbox_delantera_completa;
    public GameObject hitbox_trasera_completa;

    public GameObject hitbox_delantera_agachado;
    public GameObject hitbox_trasera_agachado;

    public GameObject hitbox_cabeza;*/

    private bool siguiente_golpe_izquierdo = true;

    public GolpesManos puño_izquierdo;
    public GolpesManos puño_derecho;

    public GolpesManos pie_izquierdo;
    public GolpesManos pie_derecho;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    //SALTAR
    public void salta_jugador(Rigidbody rigidbody){
        bool estamos_en_el_suelo = false;
        Ray rayo_hacia_el_suelo = new Ray(transform.position, transform.TransformDirection(Vector3.down));

        RaycastHit chocamos_con;

        if(Physics.Raycast(rayo_hacia_el_suelo, out chocamos_con, 1.1F))
        {
            if (chocamos_con.collider.CompareTag("suelo")){
                estamos_en_el_suelo = true;
               
            }

            if (estamos_en_el_suelo){
                rigidbody.AddForce(Vector3.up * 700f);

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

    public void defender_agachao()
    {
        if(estado_actual != EstadosMovimiento.agachado){
            return;
        }
        cambiarEstado(EstadosMovimiento.defender_agachao);
        // !! CAMBIAR HITBOXES
    }

    public void hacer_golpe_debil(Animator animator)
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

    public void hacer_golpe_fuerte(Animator animator){
        animator.SetTrigger("GolpeFuerte");
        StartCoroutine(activar_ataque(puño_izquierdo,puño_derecho,30,1f));
    }

    public void hacer_patada_debil(Animator animator){
        animator.SetTrigger("PatadaDebil");
        
        StartCoroutine(activar_ataque(pie_izquierdo,pie_derecho,20,1f));
    }

    public void hacer_patada_fuerte(Animator animator)
    {
        animator.SetTrigger("PatadaFuerte");
        StartCoroutine(activar_ataque(pie_izquierdo,pie_derecho,60,0.35f));
    }

    public void cambiarEstado(EstadosMovimiento estado_nuevo){
        estado_actual = estado_nuevo;

        if(hay_gente_escuchando_el_estado != null){
            hay_gente_escuchando_el_estado.Invoke(estado_nuevo);
        }
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
