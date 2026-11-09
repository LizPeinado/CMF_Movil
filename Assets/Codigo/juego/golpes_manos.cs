
using System.Collections.Generic;
using UnityEngine;

public class GolpesManos : MonoBehaviour
{
    public bool ataque_activo = false;
    public int daño = 100;

    private List<GameObject> a_quien_le_pego;

    void Start()
    {
        a_quien_le_pego = new List<GameObject>();
    }

    void hacer_daño(GameObject a_quien)
    {
        if(!ataque_activo) return;

        GameObject personaje = a_quien.transform.root.gameObject;

        if(a_quien_le_pego.Contains(personaje)) return;

        HitBox hitbox_golpeada = a_quien.GetComponent<HitBox>();

        if(hitbox_golpeada == null) return;

        Sistema_salud salud_de_a_quien_golpeo =
            hitbox_golpeada.salud_del_padre;

        if(salud_de_a_quien_golpeo == null) return;

        ControladorDefensa defensa =
            personaje.GetComponent<ControladorDefensa>();

        // Comprobar si el personaje está defendiendo
        if(defensa != null && defensa.esta_defendiendo)
        {
            bool golpe_alto =
                hitbox_golpeada.tipo_hitbox == TipoHitBox.cabeza;

            bool golpe_bajo =
                hitbox_golpeada.tipo_hitbox == TipoHitBox.baja;

            // Defensa de pie: solamente la baja la rompe
            if(!defensa.defensa_agachada && golpe_bajo)
            {
                defensa.quitar_defensa();

                ControladorAturdimiento aturdimiento =
                    personaje.GetComponent<ControladorAturdimiento>();

                if(aturdimiento != null)
                {
                    Debug.Log("DEFENSA DE PIE ROTA POR GOLPE BAJO");
                    aturdimiento.romper_defensa();
                }
            }
            // Defensa agachada: solamente la alta la rompe
            else if(defensa.defensa_agachada && golpe_alto)
            {
                defensa.quitar_defensa();

                ControladorAturdimiento aturdimiento =
                    personaje.GetComponent<ControladorAturdimiento>();

                if(aturdimiento != null)
                {
                    Debug.Log("DEFENSA AGACHADA ROTA POR GOLPE ALTO");
                    aturdimiento.romper_defensa();
                }
            }
            else
            {
                Debug.Log("GOLPE BLOQUEADO");
            }

            a_quien_le_pego.Add(personaje);
            Invoke(nameof(limpiar_lista_golpes), 0.2f);
            return;
        }

        // Si no está defendiendo, recibe daño
        salud_de_a_quien_golpeo.restar_salud(daño);
        hitbox_golpeada.empujar();

        a_quien_le_pego.Add(personaje);
        Invoke(nameof(limpiar_lista_golpes), 0.2f);
    }

    void OnTriggerStay(Collider golpear)
    {
        hacer_daño(golpear.gameObject);
    }

    void OnTriggerExit(Collider golpear)
    {
        a_quien_le_pego.Remove(golpear.transform.root.gameObject);
    }

    void limpiar_lista_golpes()
    {
        a_quien_le_pego.Clear();
    }
}


