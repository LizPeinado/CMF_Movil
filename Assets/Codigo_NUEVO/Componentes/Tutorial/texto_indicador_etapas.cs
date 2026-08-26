using UnityEngine;
using TMPro;

public class TextoIndicadorEtapas : MonoBehaviour
{
    private TMP_Text cajita_texto_etapa;
    public ControladorEtapas controlador_etapas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cajita_texto_etapa = GetComponent<TMP_Text>();
        controlador_etapas = FindFirstObjectByType<ControladorEtapas>();
    }

    // Update is called once per frame
    void Update(){
        actualizar_estado(controlador_etapas.etapa_actual.ToString());
    }

    public void actualizar_estado(string texto_ha_actualizar){
        cajita_texto_etapa.text = texto_ha_actualizar;
    }
}
