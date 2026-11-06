using UnityEngine;

public class RecogibleComponente : MonoBehaviour, InteractuableComportamiento
{
    [SerializeField]
    private TipoItem tipo_actual;

    public TipoItem tipo
    {
        get { return tipo_actual; }
        set { tipo_actual = value; }
    }

    public string nombre { get; set; }

    public string nombre_del_objeto;

    [Header("Modelo de la accion")]
    public GameObject modelo_golpazo;

    void Start()
    {
        nombre = nombre_del_objeto;
    }

    public void accion_consumible()
    {
        Debug.Log("OBJETO CONSUMIDO: " + nombre);
    }

    public void accion_golpazo(GameObject jugador)
    {
        if(tipo != TipoItem.golpazo)
        {
            return;
        }

        if(modelo_golpazo == null)
        {
            Debug.LogError("NO SE ASIGNO EL MODELO DE: " + nombre);
            return;
        }

        GameObject modelo = Instantiate(
            modelo_golpazo,
            jugador.transform.position,
            jugador.transform.rotation
        );

        ControladorAnimacionObjeto controlador =
            modelo.GetComponent<ControladorAnimacionObjeto>();

        if(controlador != null)
        {
            controlador.iniciar(jugador);
        }
        else
        {
            //Debug.LogError("FALTA ControladorAnimacionObjeto EN EL PREFAB");
            Destroy(modelo);
        }
    }

    public void colocar_en(Transform ubicacion)
    {
        transform.SetParent(ubicacion);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void soltar()
    {
        transform.SetParent(null);
    }
}
