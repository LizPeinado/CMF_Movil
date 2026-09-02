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
public class ControladorEtapas : MonoBehaviour
{
    public EtapasTutorial etapa_actual = EtapasTutorial.avanzar_retroceder;

    private void Start()
    {
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
                etapa_quitar_defensa();
            break;
            case EtapasTutorial.usar_item:
                etapa_usar_item();
            break;
            case EtapasTutorial.ataque_especial:
                etapa_ataque_especial();
            break;
            case EtapasTutorial.combate_final:
                etapa_combate_final();
            break;
        }
    }

    void etapa_avanzar_retroceder()
    {
        Debug.Log("ETAPA 1: AVANZAR Y RETROCEDER");
    }
    void etapa_saltar_agacharse()
    {
        Debug.Log("ETAPA 2: SALTAR Y AGACHARSE");
    }
    void etapa_ataques()
    {
        Debug.Log("ETAPA 3: ATAQUES");
    }
    void etapa_defender()
    {
        Debug.Log("ETAPA 4: DEFENDER");
    }
    void etapa_quitar_defensa()
    {
        Debug.Log("ETAPA 5: QUITAR LA DEFENSA");
    }
    void etapa_usar_item()
    {
        Debug.Log("ETAPA 6: USAR LOS ITEMS");
    }
    void etapa_ataque_especial()
    {
        Debug.Log("ETAPA 7: USAR ESPECIAL");
    }
    void etapa_combate_final()
    {
        Debug.Log("ETAPA 8: COMBATE FINAL");
    }

}
