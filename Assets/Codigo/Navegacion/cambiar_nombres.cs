using UnityEngine;
using UnityEngine.UI;

public class cambiar_nombres : MonoBehaviour
{

    private Image imagen_nombres;
    private Sprite nombre_dragon, nombre_chimazu, nombre_zune, nombre_celestial, nombre_costal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imagen_nombres = GetComponent<Image>();
        nombre_dragon = Resources.Load<Sprite>("Nmb_Dragon");
        nombre_chimazu = Resources.Load<Sprite>("Nmb_Chimazu");
        nombre_zune = Resources.Load<Sprite>("Nmb_Zune");
        nombre_celestial = Resources.Load<Sprite>("Nmb_Celestial");
        nombre_costal = Resources.Load<Sprite>("Nmb_Costal");
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(PlayerPrefs.GetInt("personaje_seleccionado"))
        {
            case 0:
                imagen_nombres.sprite = nombre_dragon;
            break;

            case 1:
                imagen_nombres.sprite = nombre_chimazu;
            break;

            case 2:
                imagen_nombres.sprite = nombre_zune;
            break;

            case 3:
                imagen_nombres.sprite = nombre_celestial;
            break;

            case 4:
                imagen_nombres.sprite = nombre_costal;
            break;
        }
    }
}
