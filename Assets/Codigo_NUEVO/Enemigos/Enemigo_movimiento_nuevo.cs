using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemigoMovimientoNuevo : MonoBehaviour
{
    private Rigidbody rigid_body;
    private NavMeshAgent control_movimiento;

    public GameObject[] quienes_se_pueden_seguir;
    private GameObject a_quien_seguir;

    public bool puede_moverse = false;
    public bool retrocediendo = false;
    public bool esta_saltando = false;
    public bool esta_agachado = false;
    public bool estado_cubriendose = false;
    public float fuerza_salto = 700f;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigid_body = GetComponent<Rigidbody>();
        control_movimiento = GetComponent<NavMeshAgent>();

        switch (PlayerPrefs.GetInt("personaje_seleccionado"))
        {
            case 0:
                a_quien_seguir = quienes_se_pueden_seguir[0];
            break;
            case 2:
                a_quien_seguir = quienes_se_pueden_seguir[1];
            break;
            case 3:
                a_quien_seguir = quienes_se_pueden_seguir[2];
            break;
            default:
                a_quien_seguir = quienes_se_pueden_seguir[0];
            break;
        }

        if(a_quien_seguir == null)
        {
            PersonajesHitbox jugador = FindFirstObjectByType<PersonajesHitbox>();

            if(jugador != null)
            {
                a_quien_seguir = jugador.gameObject;
            }
        }
    }

    void Update()
    {
        if(!control_movimiento.enabled || !control_movimiento.isOnNavMesh)
        {
            animator.SetBool("Caminar", false);
            return;
        }

        if(!puede_moverse || esta_saltando || esta_agachado)
        {
            control_movimiento.destination = transform.position;
            animator.SetBool("Caminar", false);
            return;
        }

        if(a_quien_seguir != null)
        {
            if(retrocediendo)
            {
                Vector3 posicion_retroceso = transform.position - transform.forward * 3f;
                control_movimiento.destination = posicion_retroceso;
                animator.SetBool("Caminar", false);
            }
            else
            {
                control_movimiento.destination = a_quien_seguir.transform.position;
                animator.SetBool("Caminar", true);
            }
        }
        else
        {
            control_movimiento.destination = transform.position;
            animator.SetBool("Caminar", false);
        }
    }

    public void saltar()
    {
        if(esta_saltando)
        {
            Debug.Log("Ya está saltando");
            return;
        }

        Debug.Log("INTENTANDO SALTAR");

        Ray rayo_hacia_el_suelo = new Ray(transform.position, Vector3.down);
        RaycastHit chocamos_con;

        if(Physics.Raycast(rayo_hacia_el_suelo, out chocamos_con, 1.1f))
        {
            Debug.Log("OBJETO DETECTADO: " + chocamos_con.collider.name);
            Debug.Log("ETIQUETA: " + chocamos_con.collider.tag);

            if(chocamos_con.collider.CompareTag("suelo"))
            {
                Debug.Log("SUELO DETECTADO, COMIENZA EL SALTO");

                esta_saltando = true;

                control_movimiento.enabled = false;

                rigid_body.linearVelocity = Vector3.zero;

                rigid_body.AddForce(Vector3.up * fuerza_salto);

                animator.SetBool("Salta", true);
            }
        }
        else
        {
            Debug.Log("EL RAYCAST NO DETECTO NADA");
        }
    }

    void OnCollisionEnter(Collision colision)
    {
        if(colision.gameObject.CompareTag("suelo") && esta_saltando)
        {
            esta_saltando = false;

            animator.SetBool("Salta", false);

            rigid_body.linearVelocity = Vector3.zero;

            NavMeshHit posicion_navmesh;

            if(NavMesh.SamplePosition(transform.position, out posicion_navmesh, 2f, NavMesh.AllAreas))
            {
                control_movimiento.enabled = true;
                control_movimiento.Warp(posicion_navmesh.position);
            }
        }
    }
}