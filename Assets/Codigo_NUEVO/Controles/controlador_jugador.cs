using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControladorJugador : movimientos_personajes{
    private Rigidbody rigid__body;
    private PlayerInput entradas_jugador;
    private InputAction movimientos;
    private InputAction saltando;
    private InputAction agachando;

    //RECIBIR INFORMACION DE PERSONAJE
    public float velocidad_movimiento = 5.0f;
    //cuando se tenga, minimo empezar con Jose



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        //Agarrando componentes pa que jale
        entradas_jugador = GetComponent<PlayerInput>();
        rigid__body = GetComponent<Rigidbody>();

        //Declarando movimientos principales
        movimientos = entradas_jugador.actions.FindAction("movimiento");
        saltando = entradas_jugador.actions.FindAction("saltar");
        saltando.performed += saltar;

        agachando = entradas_jugador.actions.FindAction("Agacharse");
        agachando.performed += agachar;
        agachando.canceled += no_se_agache;
    }

    void saltar(InputAction.CallbackContext _){
        salta_jugador(rigid__body);
    }

    void agachar(InputAction.CallbackContext _){
        agacharse();
    }

    void no_se_agache(InputAction.CallbackContext _){
        agacharsent();
    }

     public void avanzar_adelante(Vector2 direccion, float velocidad_movimiento){
        Vector3 movimiento = new Vector3(direccion.y, 0f,0f);

        rigid__body.MovePosition(transform.position + (movimiento * velocidad_movimiento * Time.fixedDeltaTime));
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
}
