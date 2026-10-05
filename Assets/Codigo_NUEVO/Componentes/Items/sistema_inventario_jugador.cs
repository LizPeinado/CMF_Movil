using System;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class SistemaInventarioJugador : MonoBehaviour
{
    private PlayerInput entradas_del_jugador;
    private InputAction interactuar;

    private UbicacionInventario mano_derecha;
    private UbicacionInventario mano_izquierda;
    private InteractuableComportamiento puedo_tomar_esto;
    private GameObject objeto_para_recoger;
    
    
    void Start(){
        entradas_del_jugador = GetComponent<PlayerInput>();
        interactuar = entradas_del_jugador.actions.FindAction("Agacharse");

        interactuar.performed += realizar_interaccion;

        var ubicaciones_inventario = GetComponentInChildren<UbicacionInventario>();

        /*foreach (var ubicacion in ubicaciones_inventario) {

            if (ubicacion.lugar == NombreUbicacion.mano_derecha) {
                mano_derecha = ubicacion;
            }

            else {
                mano_izquierda = ubicacion;
            }
        }*/
    }

    void realizar_interaccion(InputAction.CallbackContext _){
        if(puedo_tomar_esto == null){
            return;
        }
        if(puedo_tomar_esto != null)
        {
            switch (puedo_tomar_esto.tipo)
            {
                case TipoItem.consumible:
                break;

                case TipoItem.golpazo:
                break;
            }
        }
    }

}
