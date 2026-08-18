using UnityEngine;
using UnityEngine.InputSystem;

public class controlador_jugador : movimientos_personajes{
    private Rigidbody rigid_body;
    private PlayerInput entradas_jugador;
    private InputAction movimientos;
    private InputAction saltando;
    private InputAction agachando;

    //RECIBIR INFORMACION DE PERSONAJE
    //cuando se tenga, minimo empezar con Jose



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        //Agarrando componentes pa que jale
        entradas_jugador = GetComponent<PlayerInput>();
        rigid_body = GetComponent<Rigidbody>();

        //Declarando movimientos principales
        movimientos = entradas_jugador.actions.FindAction("movimiento");
        saltando = entradas_jugador.actions.FindAction("saltar");
        saltando.performed += saltar;

        agachando = entradas_jugador.actions.FindAction("Agacharse");
        agachando.performed += agachar;
        agachando.canceled += no_se_agache;
    }

    void saltar(InputAction.CallbackContext _){
        salta_jugador();
    }

    void agachar(InputAction.CallbackContext _){
        agacharse();
    }

    void no_se_agache(InputAction.CallbackContext _){
        agacharsent();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
