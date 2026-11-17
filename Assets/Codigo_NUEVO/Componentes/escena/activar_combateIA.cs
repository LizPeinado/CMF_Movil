using System.Collections;
using UnityEngine;

public class ActivarCombateIA : MonoBehaviour
{
    public IA_Enemigo ia_enemigo;

    void Start()
    {
        StartCoroutine(iniciar_combate());
    }

    IEnumerator iniciar_combate()
    {
        yield return null;

        if(ia_enemigo != null)
        {
            ia_enemigo.activar_ia();
            Debug.Log("IA DEL ENEMIGO ACTIVADA");
        }
        else
        {
            Debug.LogError("NO SE ASIGNO LA IA DEL ENEMIGO");
        }
    }
}