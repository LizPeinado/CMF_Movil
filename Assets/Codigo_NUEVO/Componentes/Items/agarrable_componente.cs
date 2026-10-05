using System;
using UnityEngine;

public class AgarrableComponente : MonoBehaviour, InteractuableComportamiento{
    public TipoItem tipo {get; set;}

    private bool me_han_agarrado = false;
    
    public String nombre {get; set;}
    public String nombre_actual;

    public delegate void siendo_observado(bool si_me_observan);
    public event siendo_observado saber_si_me_observan;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        tipo = TipoItem.golpazo;
        nombre = nombre_actual;
    }

    public void marcar_como_observado(){
        saber_si_me_observan?.Invoke(true);
    }

    public void desmarcar_como_observado(){
        saber_si_me_observan?.Invoke(false);
    }

    public void colocar_en(Transform ubicacion){
        transform.SetParent(ubicacion);
        transform.position = ubicacion.position;
    }

    public void soltar(){ //Aquí desapareceria el item porque ya se "usó"

    }

    public void accion_consumible(){ //Aquí para los items que suben estadisticas como barra o salud

    }

    public void accion_golpazo(){ //Accion para la silla, llamar animacion y dar hitbox para hacer daño
        
    }
   
}
