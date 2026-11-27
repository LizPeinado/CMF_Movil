
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Etapa8Tutorial : MonoBehaviour
{
    public ControladorEtapas controlador;
    public IA_Enemigo ia_enemigo;

    private bool etapa_iniciada = false;

    void Update()
    {
        if(controlador.etapa_actual == EtapasTutorial.combate_final &&
           !controlador.esta_en_pausa &&
           !etapa_iniciada)
        {
            etapa_iniciada = true;
            dialogos_etapa(controlador.caja_dialogos, controlador.imagen_dialogos);

            Debug.Log("ETAPA 8: COMBATE FINAL");

            ia_enemigo.activar_ia();
        }
    }

    void dialogos_etapa(TMP_Text cajita, Image imagen)
    {
        cajita.text = "Ahora intenta ganarle al costal... Buena suerte, jaja...";
        imagen.sprite = Resources.Load<Sprite>("Alexis_1");
    }
}
