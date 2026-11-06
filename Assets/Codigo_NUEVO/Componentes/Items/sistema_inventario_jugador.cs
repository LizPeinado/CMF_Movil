
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class SistemaInventarioJugador : MonoBehaviour
{
    private PlayerInput entradas_del_jugador;
    private InputAction agacharse;

    public UbicacionInventario mano_derecha;
    public UbicacionInventario mano_izquierda;

    private AgarrableComponente objeto_cercano;

    private GameObject jugador;

    //SILLAZO
    public GameObject sillazo;

   // CONTROL DEL TUTORIAL
    public bool puede_usar_objeto = false;
    public bool objeto_utilizado = false;

    void Start()
    {
        entradas_del_jugador = GetComponent<PlayerInput>();

        agacharse = entradas_del_jugador.actions.FindAction("Agacharse");

        if(agacharse != null)
        {
            agacharse.performed += realizar_interaccion;
        }
        else
        {
            Debug.LogError("NO SE ENCONTRO LA ACCION Agacharse");
        }
    }

    void OnDestroy()
    {
        if(agacharse != null)
        {
            agacharse.performed -= realizar_interaccion;
        }
    }

    void Update()
    {
        cambiar_de_mano();
    }

    void OnTriggerEnter(Collider otro)
    {
        AgarrableComponente objeto =
            otro.GetComponentInParent<AgarrableComponente>();

        if(objeto != null)
        {
            objeto_cercano = objeto;
            Debug.Log("OBJETO CERCANO: " + objeto.name);
        }
    }

    void OnTriggerExit(Collider otro)
    {
        AgarrableComponente objeto =
            otro.GetComponentInParent<AgarrableComponente>();

        if(objeto != null && objeto == objeto_cercano)
        {
            objeto_cercano = null;
            Debug.Log("OBJETO FUERA DE ALCANCE");
        }
    }

    void realizar_interaccion(InputAction.CallbackContext _)
    {
        if(objeto_cercano == null)
        {
            Debug.Log("NO HAY OBJETO CERCA PARA RECOGER");
            return;
        }

        UbicacionInventario mano_elegida;

        if(transform.forward.x > 0)
        {
            mano_elegida = mano_derecha;
        }
        else
        {
            mano_elegida = mano_izquierda;
        }

        if(mano_elegida == null)
        {
            Debug.LogError("NO SE ASIGNO LA MANO CORRESPONDIENTE");
            return;
        }

        if(mano_elegida.agarrar(objeto_cercano))
        {
            objeto_cercano = null;
        }
    }

    void cambiar_de_mano()
    {
        UbicacionInventario mano_elegida;
        UbicacionInventario otra_mano;

        if(transform.forward.x > 0)
        {
            mano_elegida = mano_derecha;
            otra_mano = mano_izquierda;
        }
        else
        {
            mano_elegida = mano_izquierda;
            otra_mano = mano_derecha;
        }

        if(mano_elegida == null || otra_mano == null)
        {
            return;
        }

        // Si el objeto ya esta en la mano correcta, no hacemos nada.
        if(mano_elegida.ocupada)
        {
            return;
        }

        // Si el objeto esta en la otra mano, lo cambiamos.
        if(otra_mano.ocupada)
        {
            AgarrableComponente objeto = otra_mano.liberar();

            if(objeto != null)
            {
                mano_elegida.agarrar(objeto);

                Debug.Log("OBJETO CAMBIADO DE MANO");
            }
        }
    }
    
    
    public void usar_objeto()
    {
        if(!puede_usar_objeto)
        {
            Debug.Log("TODAVIA NO SE PUEDEN UTILIZAR OBJETOS");
            return;
        }

        AgarrableComponente agarrable = null;
        RecogibleComponente objeto = null;

        // Revisar la mano derecha.
        if(mano_derecha != null && mano_derecha.ocupada)
        {
            agarrable = mano_derecha.objeto_agarrado;
            mano_derecha.liberar();
        }
        // Revisar la mano izquierda.
        else if(mano_izquierda != null && mano_izquierda.ocupada)
        {
            agarrable = mano_izquierda.objeto_agarrado;
            mano_izquierda.liberar();
        }

        if(agarrable != null)
        {
            objeto = agarrable.GetComponent<RecogibleComponente>();

            if(objeto != null)
            {
                if(objeto.tipo == TipoItem.golpazo)
                {
                    objeto.accion_golpazo(gameObject);
                    objeto_utilizado = true;
                }
                else if(objeto.tipo == TipoItem.consumible)
                {
                    objeto.accion_consumible();
                }

                objeto_utilizado = true;
            }
            else
            {
                Debug.LogError("EL OBJETO NO TIENE RecogibleComponente");
            }

            Destroy(agarrable.gameObject);
        }
    }




    //Cambiar esto de sistema inventario
    /*public void dar_sillazo()
    {

        GameObject modelo_sillazo = Instantiate(sillazo, transform.position, transform.rotation);
        modelo_sillazo.transform.SetParent(null);

        //Destroy (gameObject);
        gameObject.SetActive(false);
        
        if(gameObject == null){
            gameObject.SetActive(true);
        }

        //Falta que desaparezca el modelo de sillazo y vuelva el jugador
    }*/

    /*public void usar_objeto()
    {
        AgarrableComponente objeto = null;

        // Revisar si el objeto esta en la mano derecha.
        if(mano_derecha != null && mano_derecha.ocupada)
        {
            objeto = mano_derecha.liberar();
        }
        // Si no, revisar la mano izquierda.
        else if(mano_izquierda != null && mano_izquierda.ocupada)
        {
            objeto = mano_izquierda.liberar();
        }

        // Si hay un objeto, consumirlo.
        if(objeto != null)
        {
            Debug.Log("OBJETO UTILIZADO: " + objeto.nombre_actual);

            dar_sillazo();
            Destroy(objeto.gameObject); 
        }
    }*/
}




