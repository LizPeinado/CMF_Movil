using UnityEngine;

public class RecogibleComponente : MonoBehaviour, InteractuableComportamiento{

    public TipoItem tipo {get; set;}
    public string nombre {get; set;}

    public string nombre_del_objeto;

    public void accion_consumible(){

    }

    public void accion_golpazo(){
        
    }

    public void soltar(){

    }

    public void colocar_en(Transform ubicacion){
        transform.position = new Vector3(0,-10,0);
    }
    
    void Start()
    {
        tipo = TipoItem.consumible;
        nombre = nombre_del_objeto;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
