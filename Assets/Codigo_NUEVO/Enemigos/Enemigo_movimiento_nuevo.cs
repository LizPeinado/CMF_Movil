using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemigoMovimientoNuevo : MonoBehaviour
{
    private Rigidbody rigid_body;
    private NavMeshAgent control_movimiento;

    public GameObject a_quien_seguir;

    public bool puede_moverse = false;
    public bool retrocediendo = false;
    public bool esta_saltando = false;
    public float fuerza_salto = 700f;

    void Start()
    {
        rigid_body = GetComponent<Rigidbody>();
        control_movimiento = GetComponent<NavMeshAgent>();

        if(a_quien_seguir == null)
        {
            JugadorMovimiento jugador = FindFirstObjectByType<JugadorMovimiento>();

            if(jugador != null)
            {
                a_quien_seguir = jugador.gameObject;
            }
        }
    }

    void Update()
    {
        if(!puede_moverse)
        {
            control_movimiento.destination = transform.position;
            return;
        }

        if(a_quien_seguir != null)
        {
            if(retrocediendo)
            {
                Vector3 posicion_retroceso = transform.position - transform.forward * 3f;
                control_movimiento.destination = posicion_retroceso;
            }
            else
            {
                control_movimiento.destination = a_quien_seguir.transform.position;
            }
        }
        else
        {
            control_movimiento.destination = transform.position;
        }
    }

    public void saltar()
    {
        if(esta_saltando)
        {
            return;
        } 

        Ray rayo_hacia_el_suelo = new Ray(transform.position, Vector3.down);
        RaycastHit chocamos_con;

        if(Physics.Raycast(rayo_hacia_el_suelo, out chocamos_con, 1.1f))
        {
            if(chocamos_con.collider.CompareTag("suelo"))
            {
                control_movimiento.enabled = false;
                rigid_body.AddForce(Vector3.up * fuerza_salto);
                esta_saltando = true;

                Debug.Log("ENEMIGO SALTO");
            }
        }
    }

    void OnCollisionEnter(Collision colision)
    {
        if(colision.gameObject.CompareTag("suelo"))
        {
            esta_saltando = false;
            control_movimiento.enabled = true;
        }
    }
}
