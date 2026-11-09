
using UnityEngine;

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

            Debug.Log("ETAPA 8: COMBATE FINAL");

            ia_enemigo.activar_ia();
        }
    }
}
