using UnityEngine;

public enum EtapasTutorial{
    avanzar_retroceder,
    saltar_agacharse,
    ataques,
    defender,
    quitar_defensa,
    usar_item,
    ataque_especial,
    combate_final
}
public class ControladorEtapas : MonoBehaviour{
    public EtapasTutorial etapa_actual = EtapasTutorial.avanzar_retroceder;

    public void Start(){
        aplicar_etapa();
    }

    public void cambiar_etapa(EtapasTutorial nueva_etapa)
    {
        etapa_actual = nueva_etapa;
        Debug.Log("ETAPA DEL TUTORIAL: " + etapa_actual);
        aplicar_etapa();
    }


    void aplicar_etapa()
    {
        switch(etapa_actual)
        {
            case EtapasTutorial.avanzar_retroceder:
                etapa_avanzar_retroceder();
            break;

            case EtapasTutorial.saltar_agacharse:
                etapa_saltar_agacharse();
            break;

            case EtapasTutorial.ataques:
                etapa_ataques();
            break;

            case EtapasTutorial.defender:
                etapa_defender();
            break;

            case EtapasTutorial.quitar_defensa:
                etapa_quitadefensa();
            break;

            case EtapasTutorial.usar_item:
                etapa_usar_item();
            break;

            case EtapasTutorial.ataque_especial:
                etapa_ataque_especial();
            break;

            case EtapasTutorial.combate_final:
                etapa_final();
            break;
        }
    }

    void etapa_avanzar_retroceder(){
        Debug.Log("ETAPA 1: AVANZAR Y RETROCEDER");
       // cajita_de_etapas.text = $"Etapa 1: Avanzale pa enfrente y pa tras (Moby Dick) {etapa_actual}";
    }
    void etapa_saltar_agacharse(){
        Debug.Log("ETAPA 2: SALTAR Y AGACHARSE");
        //cajita_de_etapas.text = $"Etapa 2: Saltale y agachele";
    }
    void etapa_ataques(){
        Debug.Log("ETAPA 3: ATAQUES");
        //cajita_de_etapas.text = $"Etapa 3: Atacale al costal";
    }
    void etapa_defender(){
        Debug.Log("ETAPA 4: DEFENDER");
        //cajita_de_etapas.text = $"Etapa 4: Defiendete del costal (aguas)";
    }

    void etapa_quitadefensa(){
        Debug.Log("ETAPA 5: QUITAR DEFENSA");
    }

    void etapa_usar_item(){
        Debug.Log("ETAPA 6: USAR ITEM");
    }

    void etapa_ataque_especial()
    {
        Debug.Log("ETAPA 7: ATAQUE ESPECIAL");
    }

    void etapa_final()
    {
        Debug.Log("ETAPA FINAL: COMBATE A MUERTE CON CUCHILLOS");
    }

}
