using UnityEngine;
using UnityEngine.UI;

public class renders : MonoBehaviour
{

    private Image imagen;
    private Sprite img_Dragon, img_Chimazu, img_Zune, img_Celestial, img_Costal;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imagen = GetComponent<Image>();
        img_Dragon = Resources.Load<Sprite>("Dragon");
        img_Chimazu = Resources.Load<Sprite>("Chimazu");
        img_Zune = Resources.Load<Sprite>("Zune");
        img_Celestial = Resources.Load<Sprite>("Celestial");
        img_Costal = Resources.Load<Sprite>("Costal_brutal");
    }

    // Update is called once per frame
    void Update()
    {
        switch (PlayerPrefs.GetInt("personaje_seleccionado"))
        {
            case 0:
                imagen.sprite = img_Dragon;
            break;
            case 1:
                imagen.sprite = img_Chimazu;
            break;
            case 2:
                imagen.sprite = img_Zune;
            break;
            case 3:
                imagen.sprite = img_Celestial;
            break;
            case 4:
                imagen.sprite = img_Costal;
            break;
            default:
                imagen.sprite = img_Dragon;
            break; 
        }
    }
}
