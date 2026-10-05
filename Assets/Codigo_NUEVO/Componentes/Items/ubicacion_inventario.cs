using UnityEngine;


public enum NombreUbicacion{
    mano_derecha,
    mano_izquierda
}
public class UbicacionInventario : MonoBehaviour
{
   public NombreUbicacion lugar = NombreUbicacion.mano_derecha;
   public bool ocupada = false;

   public InteractuableComportamiento objeto_agarrado;

   public void interaccion(InteractuableComportamiento objeto)
    {
        if (ocupada){

        }
        else{
            agarrar(objeto);
        }
    }
    
    public bool agarrar(InteractuableComportamiento objeto){
        if(ocupada || objeto == null){
            return false;
        }

        objeto.colocar_en(gameObject.transform);
        objeto_agarrado = objeto;
        ocupada = true;

        return true;
    }

    public void objeto_usado(){
        if (ocupada){
            objeto_agarrado = null;
            
            ocupada = false;
        }
    }
}
