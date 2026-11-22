using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class configuracion : MonoBehaviour
{
    [SerializeField] Slider slider_volumen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!PlayerPrefs.HasKey("volumenMusica"))
        {
            PlayerPrefs.SetFloat("VolumenMusica", 1);
            Cargar();
        }
        else
        {
            Cargar();
        }
    }

    public void CambiarVolumen()
    {
        AudioListener.volume = slider_volumen.value;
        Guardar();
    }

    public void Cargar()
    {
        slider_volumen.value = PlayerPrefs.GetFloat("volumenMusica");
    }

    public void Guardar()
    {
        PlayerPrefs.SetFloat("volumenMusica", slider_volumen.value);
    }

}
