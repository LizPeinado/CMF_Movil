using UnityEngine;
using UnityEngine.UI;

public class BarraSalud : MonitorSalud
{
    private Slider barra;

    void Start()
{
    base.Inicializar();

    barra = GetComponentInChildren<Slider>();

    if(barra != null)
    {
        Debug.Log("=================================");
        Debug.Log("BARRA: " + gameObject.name);
        Debug.Log("SLIDER ENCONTRADO: " + barra.gameObject.name);
        Debug.Log("VALOR INICIAL: " + barra.value);
        Debug.Log("=================================");
    }
}

    /*public override void actualizacion_salud(int cantidad_salud_nueva){
        barra.value = cantidad_salud_nueva;
        Debug.Log($"[BarraSalud] Salud: {cantidad_salud_nueva}");

    }*/
    public override void actualizacion_salud(int cantidad_salud_nueva)
    {
        barra.value = cantidad_salud_nueva;

        Debug.Log("=================================");
        Debug.Log("[BarraSalud] CAMBIO DE BARRA");
        Debug.Log("[BarraSalud] Barra: " + gameObject.name);
        Debug.Log("[BarraSalud] Salud recibida: " + cantidad_salud_nueva);
        Debug.Log("=================================");
    }
}