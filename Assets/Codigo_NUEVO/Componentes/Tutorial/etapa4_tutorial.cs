using UnityEngine;

public class Etapa4Tutorial : MonoBehaviour
{
    private ControladorEtapas controlador_etapas;
    private Cerebro_enemigo cerebro_enemigo;

    private bool etapa_iniciada = false;

    void Start()
    {
        controlador_etapas = FindFirstObjectByType<ControladorEtapas>();
        cerebro_enemigo = FindFirstObjectByType<Cerebro_enemigo>();
    }

    void Update()
    {
        if(controlador_etapas == null)
        {
            return;
        }

        if(controlador_etapas.etapa_actual != EtapasTutorial.defender)
        {
            return;
        }

        if(!etapa_iniciada)
        {
            iniciar_etapa();
        }
    }

    void iniciar_etapa()
    {
        etapa_iniciada = true;

        Debug.Log("ETAPA 4: DEFENDER");

        if(cerebro_enemigo != null)
        {
            cerebro_enemigo.cambiar_estado(EstadosEnemigo.solo_ataque);
        }
    }
}
