using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControladorJugador : movimientos_personajes{
    private Rigidbody rigid__body;
    private PlayerInput entradas_jugador;
    private Sistema_salud sistema_salud; //para defenderse
    
    //INPUT ACTIONS - MOVIMIENTOS
    private InputAction movimientos;
    private InputAction saltando;
    private InputAction agachando;

    //INPUT ACTIONS - ATAQUES
    private InputAction golpe_debil;
    private InputAction golpe_medio;
    private InputAction golpe_fuerte;
    
   // private InputAction patada_debil;
    //private InputAction patada_fuerte;

    //Manos y Pies Para ataques (limpiar al rato)
    
    //PARA COMPROBAR ATAQUES EN LA ETAPA 3 DEL TUTORIAL
    public bool golpe_debil_realizado = false;
    public bool golpe_medio_realizado = false;
    public bool golpe_fuerte_realizado = false;
    //

    public bool defensa_realizada = false;

    //RECIBIR INFORMACION DE PERSONAJE
    public float velocidad_movimiento = 5.0f;

    private Animator animator_jugador;
    //cuando se tenga, minimo empezar con Jose

    //INFORMACION DE ENEMIGO
    private List<GameObject> a_quien_madreamos;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        //Agarrando componentes pa que jale
        entradas_jugador = GetComponent<PlayerInput>();
        rigid__body = GetComponent<Rigidbody>();
        animator_jugador = GetComponent<Animator>();
        sistema_salud = GetComponent<Sistema_salud>(); //pa defender

        if(sistema_salud != null)
        {
            sistema_salud.al_recibir_golpe_evento += recibio_golpe;
        }

        //Declarando movimientos principales
        movimientos = entradas_jugador.actions.FindAction("movimiento");
        saltando = entradas_jugador.actions.FindAction("saltar");
        saltando.performed += saltar;

        agachando = entradas_jugador.actions.FindAction("Agacharse");
        agachando.performed += agachar;
        agachando.canceled += no_se_agache;

        //Declarar ataques

        golpe_debil = entradas_jugador.actions.FindAction("GolpeDebil");
        golpe_medio = entradas_jugador.actions.FindAction("GolpeMedio");
        golpe_fuerte = entradas_jugador.actions.FindAction("GolpeFuerte");

        golpe_debil.performed += dale_golpe_debil;
        golpe_medio.performed += dale_golpe_medio;
        golpe_fuerte.performed += dale_golpe_fuerte;
        /*
        golpe_debil = entradas_jugador.actions.FindAction("GolpeDebil");
        golpe_fuerte = entradas_jugador.actions.FindAction("GolpeFuerte");

        patada_debil = entradas_jugador.actions.FindAction("PatadaDebil");
        patada_fuerte = entradas_jugador.actions.FindAction("PatadaFuerte");

        golpe_debil.performed += dale_golpe_debil;
        golpe_fuerte.performed += dale_golpe_fuerte;

        patada_debil.performed += dale_patada_debil;
        patada_fuerte.performed += dale_patada_fuerte;*/

        //Buscar a quien pegar


    }

    /*
    void dale_golpe_debil(InputAction.CallbackContext _){
        hacer_golpe_debil(animator_jugador);
        
    }

    void dale_golpe_fuerte(InputAction.CallbackContext _){
        hacer_golpe_fuerte(animator_jugador);
        
    }

    void dale_patada_debil(InputAction.CallbackContext _){
        hacer_patada_debil(animator_jugador);   
    }

    void dale_patada_fuerte(InputAction.CallbackContext _){
        hacer_patada_fuerte(animator_jugador);
    }
    */

    void dale_golpe_debil(InputAction.CallbackContext _)
    {
        Debug.Log("INPUT GOLPE DEBIL DETECTADO");
        golpe_debil_realizado = true;

        hacer_golpe_debil(animator_jugador);
    }

    void dale_golpe_medio(InputAction.CallbackContext _)
    {
        Debug.Log("INPUT GOLPE MEDIO DETECTADO");
        golpe_medio_realizado = true;

        hacer_golpe_medio(animator_jugador);
    }

    void dale_golpe_fuerte(InputAction.CallbackContext _)
    {
        Debug.Log("INPUT GOLPE FUERTE DETECTADO");
        golpe_fuerte_realizado = true;

        hacer_golpe_fuerte(animator_jugador);
    }

    void saltar(InputAction.CallbackContext _)
    {
        Debug.Log("INPUT DE SALTO DETECTADO");
        salta_jugador(rigid__body);
    }

    void agachar(InputAction.CallbackContext _)
    {
        Debug.Log("INPUT DE AGACHARSE DETECTADO");
        agacharse();
    }

    void no_se_agache(InputAction.CallbackContext _){
        agacharsent();
    }

    public void avanzar_adelante(Vector2 direccion, float velocidad_movimiento){
        Vector3 movimiento = new Vector3(direccion.y, 0f,0f);

        rigid__body.MovePosition(transform.position + (movimiento * velocidad_movimiento * Time.fixedDeltaTime));
    }

    void recibio_golpe()
    {
        Debug.Log("EL JUGADOR RECIBIO UN GOLPE");

        if(estado_actual == EstadosMovimiento.Retrocediendo)
        {
            Debug.Log("EL JUGADOR SE DEFENDIO");
            defensa_realizada = true;
            cambiarEstado(EstadosMovimiento.defender);
        }
    }

    void FixedUpdate(){
        if(esta_agachado)
        {
            return;
        }

        Vector2 direccion = movimientos.ReadValue<Vector2>();

        float direccion_horizontal = direccion.y;

        if(Mathf.Abs(direccion_horizontal) < 0.1f)
        {
            cambiarEstado(EstadosMovimiento.quieto);
        }
        else
        {
            bool mirando_derecha = transform.forward.x > 0;

            bool retrocediendo = (mirando_derecha && direccion_horizontal < 0) || (!mirando_derecha && direccion_horizontal > 0);

            if(retrocediendo)
            {
                cambiarEstado(EstadosMovimiento.Retrocediendo);
            }
            else
            {
                cambiarEstado(EstadosMovimiento.caminando);
            }
        }

        Debug.Log($"El valor es: {direccion.magnitude}");
       
        if(direccion.magnitude > 0.1f){
            avanzar_adelante(direccion, velocidad_movimiento);
        }

        
    }
    //Para los Ataques
}
