using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PantallaMorido : MonoBehaviour
{
    public GameObject pantalla_a_mostrar;
    public Image ganar_o_perder;
    public bool oponente_es_jugador;
    public Image sprite_ganador;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(oponente_es_jugador == true)
        {
            RevisarSprites(PlayerPrefs.GetInt("personaje_seleccionado"));
            ganar_o_perder.sprite = Resources.Load<Sprite>("VICTORIA");
        }
        else
        {
            RevisarSprites(1);
            ganar_o_perder.sprite = Resources.Load<Sprite>("DERROTA");
        }
        StartCoroutine(pantallas_espera());  
    }

    private IEnumerator pantallas_espera(){
        yield return new WaitForSeconds(3.5f);
        pantalla_a_mostrar.SetActive(true);
    }

    void RevisarSprites(int id_personaje)
    {
        switch(id_personaje)
        {
            case 0:
                sprite_ganador.sprite = Resources.Load<Sprite>("Dragon");
            break;
            case 1:
                sprite_ganador.sprite = Resources.Load<Sprite>("Chimazu");
            break;
            case 2:
                sprite_ganador.sprite = Resources.Load<Sprite>("Zune");
            break;
            case 3:
                sprite_ganador.sprite = Resources.Load<Sprite>("Celestial");
            break;
            case 4:
                sprite_ganador.sprite = Resources.Load<Sprite>("Costal_brutal");
            break;
            default:
                sprite_ganador.sprite = Resources.Load<Sprite>("Dragon");
            break; 
        }
    }

}
