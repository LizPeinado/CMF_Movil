using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PersonajesHitbox : MonoBehaviour
{

    //public float velocidad_rotacion = 0.1f;
    //private float _velocidad_por_fotograma = 0.1f;

    public delegate void cambio_estado_evento(EstadosMovimiento estado_nuevo);
    public event cambio_estado_evento hay_gente_escuchando_el_estado;



    private PlayerInput entradas_del_jugador;
  
    public bool esta_agachado = false;
    public bool esta_saltando = false;

    public GameObject hitbox_cuerpo;
    public GameObject hitbox_cabeza;
    public GameObject hitbox_baja;

    void Start(){
        entradas_del_jugador = GetComponent<PlayerInput>();


        //_velocidad_por_fotograma = velocidad_movimiento / 60;

        actualizar_hitboxes();

        //animator = GetComponent<Animator>();
        /*pegar_poquito = entradas_del_jugador.actions.FindAction("GolpeLeve");
        pegar_poquito.performed += estoy_golpeando;*/
    }


    void actualizar_hitboxes()
    {
        /*if(esta_agachado)
        {
            hitbox_delantera_completa.SetActive(false);
            hitbox_trasera_completa.SetActive(false);

            hitbox_delantera_agachado.SetActive(true);
            hitbox_trasera_agachado.SetActive(true);
        }
        else
        {
            hitbox_delantera_completa.SetActive(true);
            hitbox_trasera_completa.SetActive(true);

            hitbox_delantera_agachado.SetActive(false);
            hitbox_trasera_agachado.SetActive(false);
        }
        hitbox_cabeza.SetActive(true);*/
        
        hitbox_cabeza.SetActive(true);

        if(esta_agachado)
        {
            hitbox_cuerpo.SetActive(false);
            hitbox_baja.SetActive(true);
        }
        else
        {
            hitbox_cuerpo.SetActive(true);
            hitbox_baja.SetActive(false);
        }
        
    }

    void FixedUpdate(){
        if(esta_agachado)
        {
            return;
        }
    }
}
