using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;
using System.Collections;

public class BotonTutorial : MonoBehaviour
{
/*
    public GameObject canvas_dialogos;
    private TMP_Text caja_de_dialogos;
    private ControladorEtapas controlador_de_etapas;
    private List<EtapasTutorial> lista_de_etapas_que_pasaron = new List<EtapasTutorial>();
    private int boton_siguiente_pushado = 0;
    private int veces_que_cambio_el_estado = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        caja_de_dialogos = GetComponent<TMP_Text>();
        controlador_de_etapas.cambiar_etapa(EtapasTutorial.esperando);
        agregar_estado_al_arreglo(EtapasTutorial.esperando);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void agregar_estado_al_arreglo(EtapasTutorial ultimo_estado_conocido){
        lista_de_etapas_que_pasaron.Add(ultimo_estado_conocido);
    }

    /*void estados_del_dialogo(){
        if(lista_de_etapas_que_pasaron[1] == EtapasTutorial.esperando){
            dialogo_avanzar_retroceder(boton_siguiente_pushado);
        }
    }

    void dialogo_avanzar_retroceder(int controlador_de_texto){

        switch(controlador_de_texto){
            case 0:
                caja_de_dialogos.text = "Vamos a ver si esta jalada jala";
            break;
            case 1:
                caja_de_dialogos.text = "Pusha 'D' para avanzar y 'A' para retroceder, toma en cuenta que si cambias de direccion, te moveras en las mismas direcciones";
            break;
            case 2:
                controlador_de_etapas.cambiar_etapa(EtapasTutorial.avanzar_retroceder);
                Time.timeScale = 1f;
            break;
        }
    }
*/
    
}
