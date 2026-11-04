using UnityEngine;

public class RecogibleComponente : MonoBehaviour, InteractuableComportamiento{

    public TipoItem tipo {get; set;}
    public string nombre {get; set;}

    public string nombre_del_objeto;

    //SILLAZO
    public GameObject sillazo;
    // public GameObject objeto_a_desactivar;

    public void accion_consumible()
    {
        
    }

    public void accion_golpazo()
    {

        /*GameObject modelo_sillazo = Instantiate(sillazo, transform.position, transform.rotation);
        modelo_sillazo.transform.SetParent(null);

        Destroy (gameObject);
        /*if (objeto_a_desactivar != null) {
            
        }*/

        //Falta que desaparezca el modelo de sillazo y vuelva el jugador
        
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
