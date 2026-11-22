using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class seleccion_personaje : MonoBehaviour
{
    public GameObject[] personajes;
    public TMP_Text TextoVerificador;
    public int personaje_seleccionado = 0;
    public bool EmpezarJuego = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.GetInt("empezar_juego", 0);
        PlayerPrefs.GetInt("personaje_seleccionado", personaje_seleccionado);
        verificar_personaje();
    }

    void Awake()
    {
        if(PlayerPrefs.GetInt("empezar_juego") == 1)
        {
            CargarPersonaje(PlayerPrefs.GetInt("personaje_seleccionado"));
        }
    }


    public void SeleccionarPersonaje(int seleccion){
        personaje_seleccionado = seleccion;
        PlayerPrefs.SetInt("personaje_seleccionado", personaje_seleccionado);
        //PlayerPrefs.Save();
    }


    public void verificar_personaje()
    {
        switch (PlayerPrefs.GetInt("personaje_seleccionado"))
        {
            case 0:
                TextoVerificador.text = "Se ha seleccionado a Blue Dragon";
                
            break;
            case 1:
                TextoVerificador.text = "Se ha seleccionado a Chimazu";
            break;
            case 2:
                TextoVerificador.text = "Se ha seleccionado a Zune";
            break;
            case 3:
                TextoVerificador.text = "Se ha seleccionado a Celestial";
            break;
            case 4:
                TextoVerificador.text = "Se ha seleccionado al costal";
            break;
        }
    }

    public void CargarPersonaje(int id_personaje)
    {
        switch(id_personaje)
        {
            case 0:
                personajes[0].SetActive(true);
            break;

            case 1:
                personajes[1].SetActive(true);
            break;

            case 2:
                personajes[2].SetActive(true);
            break;

            case 3:
                personajes[3].SetActive(true);
            break;

            case 4:
                personajes[4].SetActive(true);
            break;
        }
    }
}
